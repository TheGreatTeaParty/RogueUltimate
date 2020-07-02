using System.Linq;
using UnityEngine;


public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager Instance;

    void Awake()
    {
        if (Instance != null)
            return;
        
        Instance = this;
    }
    #endregion

    public EquipmentItem[] currentEquipment;
    //Deligate for updating stats
    public delegate void OnEquipmentChanged(EquipmentItem newItem, EquipmentItem oldItem);
    public delegate void OnEquipmentCallback();

    //Deligate for updating UI
    public OnEquipmentChanged onEquipmentChanged;
    public OnEquipmentCallback onEquipmentCallback;

    
    private void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipmentType)).Length;
        currentEquipment = new EquipmentItem[numSlots];
    }

    
    public void Equip(EquipmentItem newItem)
    {
        int slotIndex = (int)newItem.equipmentType;
        EquipmentItem oldItem = null;

        //If we already have equipment save in into old item and added into inventory
        if (currentEquipment[slotIndex]!= null)
        {
            oldItem = currentEquipment[slotIndex];
            InventoryManager.Instance.AddItemToInventory(oldItem);
        }

        currentEquipment[slotIndex] = newItem;

        onEquipmentChanged?.Invoke(newItem, oldItem);
        onEquipmentCallback?.Invoke();
    }

    
    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            EquipmentItem oldItem = currentEquipment[slotIndex];
            InventoryManager.Instance.AddItemToInventory(oldItem);
            currentEquipment[slotIndex] = null;

            onEquipmentChanged?.Invoke(null, oldItem);
            onEquipmentCallback?.Invoke();
        }
    }


    public void DropFromEquipment(EquipmentItem equipmentItem)
    {
        var position = KeepOnScene.instance.transform.position;
        Vector3 newPosition = new Vector3(position.x + 1f, position.y, position.z);
        ItemScene.SpawnItemScene(newPosition, currentEquipment[(int)equipmentItem.equipmentType]);
        
        currentEquipment[(int)equipmentItem.equipmentType] = null;

        onEquipmentChanged?.Invoke(null, equipmentItem);
        onEquipmentCallback?.Invoke();
    }

    
    public bool Request(EquipmentItem equipmentItem)
    {
        foreach (var item in currentEquipment.ToList()) // ToList() prevents InvalidOperationException
        {
            if (equipmentItem == item)
                return true;
        }
        
        return false;
    }
    
}
