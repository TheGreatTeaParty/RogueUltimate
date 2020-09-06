using UnityEngine;


public class EquipmentUI : MonoBehaviour
{
    [SerializeField] private EquipmentSlot[] equipmentSlots;
    private EquipmentManager equipmentManager;
    public Transform itemsParent;   

    
    private void Start()
    {
        equipmentManager = EquipmentManager.Instance;
        equipmentManager.onEquipmentCallback += UpdateUI;
        equipmentSlots = itemsParent.GetComponentsInChildren<EquipmentSlot>();
        UpdateUI();
    }
    
    private void UpdateUI()
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentManager.currentEquipment[i] != null) equipmentSlots[i].AddItemToEquipmentSlot(equipmentManager.currentEquipment[i]);
            else equipmentSlots[i].RemoveItemFromEquipmentSlot();
        }
    }

}
