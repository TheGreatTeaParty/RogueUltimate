using UnityEngine;


public class InvetoryUI : MonoBehaviour
{
    [SerializeField] private InventorySlot[] inventorySlots;
    private InventoryManager _inventoryManager;
    public Transform itemsParent;

    
    private void Start()
    {
        _inventoryManager = InventoryManager.Instance;
        //Make delegate function from InventoryManager be equal to UpdateUI fun
        _inventoryManager.onItemChangedCallback += UpdateUI;
        inventorySlots = itemsParent.GetComponentsInChildren<InventorySlot>();
        UpdateUI();
    }

    
    private void UpdateUI()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < _inventoryManager.items.Count) inventorySlots[i].AddItemToInventorySlot(_inventoryManager.items[i]);
            else inventorySlots[i].RemoveItemFromInventorySlot();
        }
    }
    
}
