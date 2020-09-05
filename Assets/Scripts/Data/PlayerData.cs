using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public int currentHP, maxHP; //Health
    public int currentMP, maxMP; //Mana
    public int currentSP, maxSP; //Stamina

    public int xp, level;
    public int strength, dexterity, intelligence;
    
    
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
        gameObjectName = PlayerStat.Instance.gameObject.name;
     
        
        var stats = PlayerStat.Instance;

        currentHP = stats.GetCurrentHealth();
        currentMP = stats.GetCurrentMana();
        currentSP = stats.GetCurrentStamina();
        
        maxHP = stats.GetMaxHealth();
        maxMP = stats.GetMaxMana();
        maxSP = stats.GetMaxStamina();

        level = stats.level;
        xp = stats.GetXP();

        strength = stats.strength;
        dexterity = stats.agility;
        intelligence = stats.intelligence;

        
        var inventory = InventoryManager.Instance;
        gold = inventory.GetGold();
        for (int i = 0; i < inventory.items.Count; i++)
            inventoryData[i] = inventory.items[i].ID;

        
        var equipment = EquipmentManager.Instance;
        for (int i = 0; i < equipment.currentEquipment.Length; i++)
            // Null check because of currentEquipment structure: array, not list
            if (equipment.currentEquipment[i] != null)
            {
                equipmentData[i] = equipment.currentEquipment[i].ID;
                Debug.Log("Equipment saved");
            }
        
        
        var quickSlots = QuickSlotsManager.Instance;
        for (int i = 0; i < quickSlots.items.Count; i++)
            quickSlotsData[i] = quickSlots.items[i].ID;

        
        var transformPosition = PlayerStat.Instance.transform.position;
        position = new float[3];
        position[0] = transformPosition.x;
        position[1] = transformPosition.y;
        position[2] = transformPosition.z;
    }

}
