using UnityEngine;


public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    #endregion

    public Equipment[] currentEquipment;

    //Deligate for updating stats
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public delegate void OnEquipmentCallback();

    //Deligate for updating UI
    public OnEquipmentChanged onEquipmentChanged;
    public OnEquipmentCallback onEquipmentCallback;

    private void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipmentType)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipmentType;
        Equipment oldItem = null;

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
            Equipment oldItem = currentEquipment[slotIndex];

            Inventory.instance.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
                onEquipmentChanged.Invoke(null, oldItem);

            if (onEquipmentCallback != null)
                onEquipmentCallback.Invoke();
        }
    }
}