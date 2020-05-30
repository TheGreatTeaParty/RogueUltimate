using UnityEngine;


public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    void Awake()
    {
        if (instance != null)
            return;
        
        instance = this;
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
        if(currentEquipment[slotIndex]!= null)
        {
            oldItem = currentEquipment[slotIndex];
            Inventory.instance.Add(oldItem);
        }

        currentEquipment[slotIndex] = newItem;

        if (onEquipmentChanged != null)
            onEquipmentChanged.Invoke(newItem, oldItem);

        if (onEquipmentCallback != null)
            onEquipmentCallback.Invoke();
    }

    
    public void UnEquip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            EquipmentItem oldItem = currentEquipment[slotIndex];

            Inventory.instance.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
                onEquipmentChanged.Invoke(null, oldItem);

            if (onEquipmentCallback != null)
                onEquipmentCallback.Invoke();
        }
    }
    
    
}