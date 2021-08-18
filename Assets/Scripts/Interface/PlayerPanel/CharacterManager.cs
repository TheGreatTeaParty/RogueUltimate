using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterManager : MonoBehaviour
{
    #region Singleton
    
    public static CharacterManager Instance;
    
    void Awake()
    {
        if (Instance != null)
            return;
        
        Instance = this;
    }
    
    #endregion
    
    
    private PlayerStat _stats;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Equipment equipment;
    [SerializeField] private Image draggableItem;
    [SerializeField] private GameObject tooltipPrefab;

    // Optional
    private ItemSlot _draggedSlot;
    private List<Tooltip> _openedTooltips = new List<Tooltip>();

    public Inventory Inventory => inventory;
    public Equipment Equipment => equipment;
    public PlayerStat Stats => _stats;


    public event Action<EquipmentItem, EquipmentItem> onEquipmentChanged; 
    
    
    public void Start()
    {
        
        onEquipmentChanged += UpdateStatsOnEquipmentChanged;
        
        inventory.OnClickEvent += AddTooltip;
        inventory.OnBeginDragEvent += BeginDrag;
        inventory.OnDragEvent += Drag;
        inventory.OnEndDragEvent += EndDrag;
        inventory.OnDropEvent += Drop;
        inventory.OnQuickDropEvent += QuickDrop;
        
        equipment.OnClickEvent += AddTooltip;
        equipment.OnBeginDragEvent += BeginDrag;
        equipment.OnDragEvent += Drag;
        equipment.OnEndDragEvent += EndDrag;
        equipment.OnDropEvent += Drop;
    }

    // Inventory & equipment
    public void Equip(ItemSlot itemSlot)
    {

        Debug.Log("Equip !");
        EquipmentItem equipmentItem = itemSlot.Item as EquipmentItem;
        if (equipmentItem != null)
            Equip(equipmentItem);
    }
    
    private void Equip(EquipmentItem item)
    {
        if (inventory.RemoveItem(item))
        {
            EquipmentItem previousItem;
            if (equipment.AddItem(item, out previousItem))
            {
                if (previousItem != null)
                {
                    inventory.AddItem(previousItem);
                    previousItem.Unequip(_stats);
                    _stats.onChangeCallback.Invoke();
                    onEquipmentChanged?.Invoke(item, previousItem);
                }

                AudioManager.Instance.Play("Equip");
                item.Equip(_stats);
                _stats.onChangeCallback.Invoke();
                onEquipmentChanged?.Invoke(item, null);
            }
            else
            {
                inventory.AddItem(item);
            }
        }
    }

    private void EquipOnLoad(EquipmentItem item)
    {
        EquipmentItem previousItem;
        if (equipment.AddItem(item, out previousItem))
        {
            item.Equip(_stats);
            _stats.onChangeCallback.Invoke();
            onEquipmentChanged?.Invoke(item, null);
        }
    }
    
    public void Unequip(ItemSlot itemSlot)
    {
        EquipmentItem equipmentItem = itemSlot.Item as EquipmentItem;
        if (equipmentItem != null)
        {
            Unequip(equipmentItem);
            itemSlot.Item = null;
            itemSlot.Amount = 0;
        }
    }
    
    private void Unequip(EquipmentItem item)
    {
        if (!inventory.IsFull() && equipment.RemoveItem(item))
        {
            item.Unequip(_stats);
            inventory.AddItem(item);
            AudioManager.Instance.Play("Unequip");
            _stats.onChangeCallback.Invoke();
            onEquipmentChanged?.Invoke(null, item);
        }
    }

    public void DropFromInventory(ItemSlot itemSlot)
    {
        //Call spawn function on the player's position
        var position = PlayerOnScene.Instance.transform.position;
        Vector3 newPosition = new Vector3(position.x + 1f, position.y, 0f);
        Collider2D checkWall = Physics2D.OverlapCircle(newPosition, 0.25f, LayerMask.GetMask("Wall"));
        ItemScene.SpawnItemScene(checkWall == null ? newPosition : new Vector3(position.x - 1f, position.y, 0f), itemSlot.Item);

        if (itemSlot is EquipmentSlot)
        {
            onEquipmentChanged?.Invoke(null, itemSlot.Item as EquipmentItem);
            equipment.RemoveItem(itemSlot.Item as EquipmentItem);
            _stats.onChangeCallback.Invoke();
        }
        else
            inventory.RemoveItem(itemSlot.Item);
    }

    public void AddTooltip(ItemSlot itemSlot)
    {
        if (itemSlot.TooltipIsOpened) return;
        
        Tooltip tooltip = 
            Instantiate(tooltipPrefab, itemSlot.transform.position, Quaternion.identity, transform).GetComponent<Tooltip>();
        tooltip.Init(itemSlot);
        _openedTooltips.Add(tooltip);
    }

    public void RemoveTooltip(Tooltip tooltip = null)
    {
        if (tooltip)
        {
            _openedTooltips.Remove(tooltip);
            return;
        }
            
        // Remove all existing tooltips
        while (_openedTooltips.Count > 0)
            _openedTooltips[0].CloseSelf();
    }

    private void BeginDrag(ItemSlot itemSlot)
    {
        if (itemSlot.Item == null) return;
        
        _draggedSlot = itemSlot;
        draggableItem.sprite = itemSlot.Item.Sprite;
        draggableItem.transform.position = Input.mousePosition;
        draggableItem.enabled = true;
        BeginHighLight(itemSlot);
    }
    
    private void Drag(ItemSlot itemSlot)
    {
        if (draggableItem.enabled)
            draggableItem.transform.position = Input.mousePosition;
    }
    
    private void EndDrag(ItemSlot itemSlot)
    {
        _draggedSlot = null;
        draggableItem.enabled = false;
        EndHighLight();
    }
    
    private void Drop(ItemSlot dropItemSlot)
    {
        if (dropItemSlot == null || _draggedSlot == null) return;

        if (dropItemSlot.CanAddStack(_draggedSlot.Item))
            AddStack(dropItemSlot);
        else if (dropItemSlot.CanReceiveItem(_draggedSlot.Item) && _draggedSlot.CanReceiveItem(dropItemSlot.Item))
            SwapItems(dropItemSlot);
    }

    private void QuickDrop(QuickSlot dropQuickSlot)
    {
        if (_draggedSlot.Item is UsableItem)
        {
            Drop(dropQuickSlot);
            
            var dragSlot = _draggedSlot as QuickSlot;
            if (dragSlot != null) 
                dragSlot.quickSlotOnScene.SetItem(dragSlot);
        }
    }

    private void SwapItems(ItemSlot dropItemSlot)
    {
        EquipmentItem dragItem = _draggedSlot.Item as EquipmentItem;
        EquipmentItem dropItem = dropItemSlot.Item as EquipmentItem;

        // Next two if-statements also can swap equipment
        // Put on/change slot equipment 
        if (dropItemSlot is EquipmentSlot)
        {
            onEquipmentChanged.Invoke(dragItem, dropItem != null ? dropItem : null);

            AudioManager.Instance.Play("Equip");
            _stats.onChangeCallback?.Invoke();
        }

        // Put off/change slot of equipment 
        if (_draggedSlot is EquipmentSlot)
        {
            if (dropItem != null)
            {
                onEquipmentChanged.Invoke(dropItem, dragItem);
                AudioManager.Instance.Play("Equip");
            }
            else
            {
                onEquipmentChanged.Invoke(null, dragItem);
                AudioManager.Instance.Play("Unequip");
            }

            _stats.onChangeCallback?.Invoke();
        }

        Item draggedItem = _draggedSlot.Item;
        int draggedItemAmount = _draggedSlot.Amount;

        _draggedSlot.Item = dropItemSlot.Item;
        _draggedSlot.SetTier(_draggedSlot.Item);
        _draggedSlot.Amount = dropItemSlot.Amount;

        dropItemSlot.Item = draggedItem;
        dropItemSlot.SetTier(dropItemSlot.Item);
        dropItemSlot.Amount = draggedItemAmount;
    }

    private void AddStack(ItemSlot dropItemSlot)
    {
        int allowedCount = dropItemSlot.Item.StackMaxSize - dropItemSlot.Amount;
        int stackCountToAdd = Mathf.Min(allowedCount, _draggedSlot.Amount);

        dropItemSlot.Amount += stackCountToAdd;
        _draggedSlot.Amount -= stackCountToAdd;
    }
    
    
    // Stats
    public void UpdateStatsOnEquipmentChanged(EquipmentItem newItem, EquipmentItem oldItem)
    {
        if (newItem != null) newItem.Equip(_stats);
        if (oldItem != null) oldItem.Unequip(_stats);
        _stats.onChangeCallback?.Invoke();
    }
    
    public void SetStats(PlayerStat playerStat)
    {
        _stats = playerStat;
    }

    // HighLight
    private void BeginHighLight(ItemSlot itemSlot)
    {
        foreach (EquipmentSlot slot in equipment.equipmentSlots)
        {
            if (slot.CanReceiveItem(itemSlot.Item))
                slot.HighLight();
        }
    }

    private void EndHighLight()
    {
        foreach (EquipmentSlot slot in equipment.equipmentSlots)
        {
            slot.EndHighLight();
        }
    }

    public void LoadPlayerData(PlayerData data)
    {
        PlayerOnScene.Instance.SetStats();

        //Load STATS
        _stats.AddAttributePoint(StatType.Physique, data.statsData[0]);
        _stats.AddAttributePoint(StatType.Reaction, data.statsData[1]);
        _stats.AddAttributePoint(StatType.Mind, data.statsData[2]);

        //LOAD LEVEL / XP / Points Left / GOLD
        _stats.currentHealth = data.currentHP;
        _stats.CurrentMana = data.currentMP;
        _stats.CurrentStamina = data.currentSP;
        _stats.Level = data.level;
        _stats.XP = data.xp;
        _stats.StatPoints = data.statPoints;
        _stats.SkillPoints = data.abilityPoints;
        inventory.Gold = data.gold;

        //TRAITS:
        TraitsDatabase traitsDB = TraitsDatabase.Instance;
        _stats.PlayerTraits = new TraitHolder();
        _stats.PlayerTraits.AddTrait(traitsDB.GetTraitByID(data.traitsData[0]));
        _stats.PlayerTraits.AddTrait(traitsDB.GetTraitByID(data.traitsData[1]));
        _stats.PlayerTraits.AddTrait(traitsDB.GetTraitByID(data.traitsData[2]));

        ItemsDatabase itemsDB = ItemsDatabase.Instance;
        //CONTRACTS:
        _stats.PlayerContracts = new ContractHolder();
        for (int i = 0; i < data.contractsData.GetLength(0); ++i)
        {
            if (itemsDB.GetContractByID(data.contractsData[i, 0]))
            {
                _stats.PlayerContracts.Add(Instantiate(itemsDB.GetContractByID(data.contractsData[i, 0])));
                _stats.PlayerContracts.contracts[i]._currentScore = data.contractsData[i, 1];
            }
        }

        StartCoroutine(WaitAndLoadEquipment(data, itemsDB));
    }

    IEnumerator WaitAndLoadEquipment(PlayerData data, ItemsDatabase itemsDB)
    {
        yield return new WaitForSeconds(0.1f);
        
        //INVENTORY:
        for (int i = 0; i < data.inventoryData.GetLength(0); i++)
        {
            Item item = itemsDB.GetItemByID(data.inventoryData[i, 0]);
            if (item)
            {
                for(int j = 0; j < data.inventoryData[i, 1]; ++j)
                    inventory.AddItem(item);
            }
        }

        //EQUIPMENT:
        for (int i = 0; i < data.equipmentData.Length; i++)
        {
            EquipmentItem equipment = itemsDB.GetItemByID(data.equipmentData[i]) as EquipmentItem;
            if (equipment)
                EquipOnLoad(equipment);
        }

        //Quick Slots:
        for (int i = 0; i < data.quickSlotsData.GetLength(0); i++)
        {
            Item item = itemsDB.GetItemByID(data.quickSlotsData[i, 0]);
            if (item)
            {
                for(int j = 0; j < data.quickSlotsData[i, 1]; ++j)
                    inventory.AddQuickSlotItemOnLoad(item);
            }
        }

        //Skills:
        AbilityManager abilityManager = AbilityManager.Instance;
        abilityManager.CreateAbilityList();
        for (int i = 0; i < data.abillities.Length; i++)
        {
            Ability ability = itemsDB.GetAbilityByID(data.abillities[i]);
            if (ability)
            {
                abilityManager.AddSavedAbility(ability);
            }
        }

        //QuickSkills:
        for(int i = 0; i < data.quickSlotAbilities.Length; ++i)
        {
            Ability ability = itemsDB.GetAbilityByID(data.quickSlotAbilities[i]);
            if (ability)
            {
                abilityManager.AddQuickSlotAbility(ability, i);
            }
        }
    }
}
