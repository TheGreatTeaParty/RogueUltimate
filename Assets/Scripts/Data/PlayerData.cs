using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public float currentHP, maxHP; //Health
    public float currentMP, maxMP; //Mana
    public float currentSP, maxSP; //Stamina

    public int xp, level, statPoints, gold;

    public float[] position;
    public string gameObjectName;

    public int[,] inventoryData; // [i,j], where i - item id, j - ammount
    public int[] equipmentData;
    public int[,] quickSlotsData; // [i,j], where i - item id, j - ammount
    public int[] traitsData = new int[3];
    public float[] statsData = new float[3];
    
    public PlayerData()
    {
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

        inventoryData = new int[inventory.Inventory.ItemSlots.Length, 2];
        for (int i = 0; i < inventory.Inventory.ItemSlots.Length; i++)
            if (inventory.Inventory.ItemSlots[i].Item)
            {
                inventoryData[i, 0] = inventory.Inventory.ItemSlots[i].Item.ID;
                inventoryData[i, 1] = inventory.Inventory.ItemSlots[i].Amount;
            }

        quickSlotsData = new int[inventory.Inventory.QuickSlots.Length, 2];
        for (int i = 0; i < inventory.Inventory.QuickSlots.Length; i++)
            if (inventory.Inventory.QuickSlots[i].Item)
            {
                quickSlotsData[i, 0] = inventory.Inventory.QuickSlots[i].Item.ID;
                quickSlotsData[i, 1] = inventory.Inventory.QuickSlots[i].Amount;
            }

        Equipment equipment = CharacterManager.Instance.Equipment;
        equipmentData = new int[equipment.equipmentSlots.Length];
        for (int i = 0; i < equipment.equipmentSlots.Length; i++)
            // Null check because of currentEquipment structure: array, not list
            if (equipment.equipmentSlots[i].Item != null)
                equipmentData[i] = equipment.equipmentSlots[i].Item.ID;

        // TODO: save skills
            
        

        
        var transformPosition = CharacterManager.Instance.Stats.transform.position;
        position = new float[3];
        position[0] = transformPosition.x;
        position[1] = transformPosition.y;
        position[2] = transformPosition.z;
    }

}
