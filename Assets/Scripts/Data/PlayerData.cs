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

    public int xp, level, statPoints;

    public float[] position;
    public string scene;
    public string gameObjectName;

    public int[] inventoryData;
    public int[] equipmentData;
    public int[] quickSlotsData = new int[3];
    public int[] traitsData = new int[3];
    public float[] statsData = new float[3];


    public int gold;
    
    public PlayerData()
    {
        scene = SceneManager.GetActiveScene().name;
        gameObjectName = CharacterManager.Instance.Stats.gameObject.name;
     
        
        var stats = CharacterManager.Instance.Stats;

        currentHP = stats.CurrentHealth;
        currentMP = stats.CurrentMana;       
        currentSP = stats.CurrentStamina;

        statsData[0] = stats.Strength.GetBaseValue();
        statsData[1] = stats.Agility.GetBaseValue();
        statsData[2] = stats.Intelligence.GetBaseValue();

        level = stats.Level;
        xp = stats.XP;
        statPoints = stats.StatPoints;

        traitsData[0] = stats.PlayerTraits.Traits[0].ID;
        traitsData[1] = stats.PlayerTraits.Traits[1].ID;
        traitsData[2] = stats.PlayerTraits.Traits[2].ID;


        var inventory = CharacterManager.Instance;
        gold = inventory.Inventory.Gold;

       inventoryData = new int[inventory.Inventory.Items.Count];
       for (int i = 0; i < inventory.Inventory.Items.Count; i++)
            inventoryData[i] = inventory.Inventory.Items[i].ID;
        
        Equipment equipment = CharacterManager.Instance.Equipment;
        equipmentData = new int[equipment.equipmentSlots.Length];
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
