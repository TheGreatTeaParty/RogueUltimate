using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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
    
    
    [SerializeField] private Inventory inventory;
    [SerializeField] private Equipment equipment;
    [SerializeField] private PlayerStat stats;
    [SerializeField] private Image draggableItem;
    [SerializeField] private GameObject tooltipPrefab;

    // Optional
    private ItemSlot _draggedSlot;
    private List<Tooltip> _openedTooltips = new List<Tooltip>();

    public Inventory Inventory => inventory;
    public Equipment Equipment => equipment;
    
    
    public delegate void OnEquipmentChanged(EquipmentItem newItem, EquipmentItem oldItem);
    public OnEquipmentChanged onEquipmentChanged;
    
    
    public void Start()
    {
        stats = PlayerOnScene.Instance.GetComponent<PlayerStat>();
        
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
        
        
        // Load items on save
        if (SaveManager.LoadPlayer() == null) return;
        
        var data = SaveManager.LoadPlayer();
        foreach (var id in data.inventoryData)
        {
           /* if (id != 0) 
                AddItemToInventory(ItemsDatabase.Instance.GetItemByID(id));
            */
        }
        
    }

    public void Equip(ItemSlot itemSlot)
    {
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
                    previousItem.Unequip(stats);
                    stats.onChangeCallback.Invoke();
                }

                AudioManager.Instance.Play("Equip");
                item.Equip(stats);
                stats.onChangeCallback.Invoke();
            }
            else
            {
                inventory.AddItem(item);
            }
        }
    }
    
    public void Unequip(ItemSlot itemSlot)
    {
        EquipmentItem equipmentItem = itemSlot.Item as EquipmentItem;
        if (equipmentItem != null)
            Unequip(equipmentItem);
    }
    
    private void Unequip(EquipmentItem item)
    {
        if (!inventory.IsFull() && equipment.RemoveItem(item))
        {
            inventory.AddItem(item);
            AudioManager.Instance.Play("Unequip");
        }
    }

    public void DropFromInventory(Item item)
    {
        //Call spawn function on the player's position
        var position = PlayerOnScene.Instance.transform.position;
        Vector3 newPosition = new Vector3(position.x + 1f, position.y, 0f);
        Collider2D checkWall = Physics2D.OverlapCircle(newPosition, 0.25f, LayerMask.GetMask("Wall"));
        ItemScene.SpawnItemScene(checkWall == null ? newPosition : new Vector3(position.x - 1f, position.y, 0f), item);

        inventory.RemoveItem(item);
    }

    public void UpdateStatsOnEquipmentChanged(EquipmentItem newItem, EquipmentItem oldItem)
    {
        if (newItem != null) newItem.Equip(stats);
        if (oldItem != null) oldItem.Unequip(stats);
        stats.onChangeCallback?.Invoke();
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
            stats.onChangeCallback?.Invoke();
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

            stats.onChangeCallback.Invoke();
        }

        Item draggedItem = _draggedSlot.Item;
        int draggedItemAmount = _draggedSlot.Amount;

        _draggedSlot.Item = dropItemSlot.Item;
        _draggedSlot.Amount = dropItemSlot.Amount;

        dropItemSlot.Item = draggedItem;
        dropItemSlot.Amount = draggedItemAmount;
    }

    private void AddStack(ItemSlot dropItemSlot)
    {
        int allowedCount = dropItemSlot.Item.StackMaxSize - dropItemSlot.Amount;
        int stackCountToAdd = Mathf.Min(allowedCount, _draggedSlot.Amount);

        dropItemSlot.Amount += stackCountToAdd;
        _draggedSlot.Amount -= stackCountToAdd;
    }
    
}
