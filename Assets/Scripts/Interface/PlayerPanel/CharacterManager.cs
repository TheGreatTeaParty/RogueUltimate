using System.Collections.Generic;
using System.Data;
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
    
    [SerializeField] private int gold = 10;
    [Space]
    [SerializeField] private Inventory inventory;
    [SerializeField] private Equipment equipment;
    [SerializeField] private PlayerStat stats;
    [SerializeField] private Image draggableItem;
    private ItemSlot _draggedSlot;

    public Equipment Equipment => equipment;
    
    
    public delegate void OnEquipmentChanged(EquipmentItem newItem, EquipmentItem oldItem);
    public OnEquipmentChanged onEquipmentChanged;
    
    
    public void Start()
    {
        stats = PlayerOnScene.Instance.GetComponent<PlayerStat>();
        
        onEquipmentChanged += UpdateStatsOnEquipmentChanged;

        
        inventory.OnClickEvent += ShowTooltip;
        inventory.OnBeginDragEvent += BeginDrag;
        inventory.OnDragEvent += Drag;
        inventory.OnEndDragEvent += EndDrag;
        inventory.OnDropEvent += Drop;
        
        equipment.OnClickEvent += ShowTooltip;
        equipment.OnBeginDragEvent += BeginDrag;
        equipment.OnDragEvent += Drag;
        equipment.OnEndDragEvent += EndDrag;
        equipment.OnDropEvent += Drop;
        
        
        // Load items on save
        if (SaveManager.LoadPlayer() == null) return;
        
        var data = SaveManager.LoadPlayer();
        gold = data.gold;
        foreach (var id in data.inventoryData)
        {
           /* if (id != 0) 
                AddItemToInventory(ItemsDatabase.Instance.GetItemByID(id));
            */
        }
        
    }

    public bool AddItemToInventory(Item item)
    {
        if (!inventory.IsFull())
        {
            inventory.AddItem(item);
            return true;
        }

        return false;
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
    }

    public void UpdateStatsOnEquipmentChanged(EquipmentItem newItem, EquipmentItem oldItem)
    {
        if (newItem != null) newItem.Equip(stats);
        if (oldItem != null) oldItem.Unequip(stats);
        stats.onChangeCallback?.Invoke();
    }

    private void ShowTooltip(ItemSlot itemSlot)
    {
        
    }

    private void HideTooltip(ItemSlot itemSlot)
    {
        
    }
    
    public void ChangeGold(int value)
    {
        gold += value;
    }

    public void SetGold(int value)
    {
        gold = value;
    }

    public int GetGold()
    {
        return gold;
    }

    private void BeginDrag(ItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
        {
            _draggedSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.Sprite;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
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
        if (dropItemSlot.CanReceiveItem(_draggedSlot.Item) && _draggedSlot.CanReceiveItem(dropItemSlot.Item))
        {
            EquipmentItem dragItem = _draggedSlot.Item as EquipmentItem;
            EquipmentItem dropItem = dropItemSlot.Item as EquipmentItem;

            // Next two if-statements also can swap equipment
            // Put on/change slot equipment 
            if (dropItemSlot is EquipmentSlot)
            {
                onEquipmentChanged.Invoke(dragItem, dropItem != null ? dropItem : null);
                AudioManager.Instance.Play("Equip");
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
            }
            
            //stats.onChangeCallback.Invoke();;

            Item draggedItem = _draggedSlot.Item;
            _draggedSlot.Item = dropItemSlot.Item;
            dropItemSlot.Item = draggedItem;
        }
    }

}
