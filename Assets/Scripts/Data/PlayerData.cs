using UnityEngine;
using UnityEngine.SceneManagement;


struct InventoryData
{
    private int ID;
    private int Amount;
    
}


[System.Serializable]
public class PlayerData
{
    public float currentHP, maxHP; //Health
    public float currentMP, maxMP; //Mana
    public float currentSP, maxSP; //Stamina

    public int xp, level;

    public float[] position;
    public string scene;
    public string gameObjectName;

    public int[] inventoryData = new int[8];
    public int[] equipmentData = new int[4];
    public int[] quickSlotsData = new int[3];
    public int gold;
    
    public PlayerData()
    {
        scene = SceneManager.GetActiveScene().name;
        gameObjectName = CharacterManager.Instance.Stats.gameObject.name;
     
        
        var stats = CharacterManager.Instance.Stats;

        currentHP = stats.CurrentHealth;
        currentMP = stats.CurrentMana;       
        currentSP = stats.CurrentStamina;

        maxHP = stats.MaxHealth;
        maxMP = stats.MaxMana;
        maxSP = stats.MaxStamina;        
            
        level = stats.Level;
        xp = stats.XP;


        var inventory = CharacterManager.Instance;
        //gold = playerInventory.GetGold();
       /* for (int i = 0; i < playerInventory.items.Count; i++)
            inventoryData[i] = playerInventory.items[i].ID;
*/
        
        Equipment equipment = CharacterManager.Instance.Equipment;
        for (int i = 0; i < equipment.equipmentSlots.Length; i++)
            // Null check because of currentEquipment structure: array, not list
            if (equipment.equipmentSlots[i].Item != null)
            {
                equipmentData[i] = equipment.equipmentSlots[i].Item.ID;
                Debug.Log("Equipment saved");
            }
        

        
        var transformPosition = CharacterManager.Instance.Stats.transform.position;
        position = new float[3];
        position[0] = transformPosition.x;
        position[1] = transformPosition.y;
        position[2] = transformPosition.z;
    }

}
