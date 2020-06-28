using System;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerStat : CharacterStat, IDamaged
{
    #region Singleton

    public static PlayerStat Instance;

    private void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
    }

    #endregion
    
    private float _regenerationCoolDown;
    [SerializeField] private float regenerationSpeed;

    public int maxMana = 100;
    public int maxStamina = 100;
    
    public int currentMana;
    public int currentStamina;


    private void Start()
    {
        _regenerationCoolDown = 0;
        regenerationSpeed = 1;
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        currentMana = maxMana;
        
        EquipmentManager.Instance.onEquipmentChanged += OnEquipmentChanged;
    }


    private void Update()
    {
        _regenerationCoolDown += Time.deltaTime * regenerationSpeed;
        if (_regenerationCoolDown > 1)
        {
            ModifyStamina(5);
            _regenerationCoolDown = 0;
        }
    }

    //Receive message of changing equipment, so change player modifiers
    private void OnEquipmentChanged(EquipmentItem newEquipmentItem, EquipmentItem oldEquipmentItem)
    {
        if (newEquipmentItem != null)
        {
            physicalProtection.AddModifier(newEquipmentItem.PhysicalArmorModifier);
            magicProtection.AddModifier(newEquipmentItem.MagicalArmorModifier);

            physicalDamage.AddModifier(newEquipmentItem.PhysiscalDamageModifier);
            magicDamage.AddModifier(newEquipmentItem.MagicalDamageModifier);
        }

        if (oldEquipmentItem != null)
        {
            physicalProtection.RemoveModifier(oldEquipmentItem.PhysicalArmorModifier);
            magicProtection.RemoveModifier(oldEquipmentItem.MagicalArmorModifier);

            physicalDamage.RemoveModifier(oldEquipmentItem.PhysiscalDamageModifier);
            magicDamage.RemoveModifier(oldEquipmentItem.MagicalDamageModifier);
        }
    }

    public bool ModifyHealth(int value)
    {
        if (currentHealth + value < 0)
            return false;
        
        currentHealth += value;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        return true;
    }
    
    public bool ModifyStamina(int value)
    {
        if (currentStamina + value < 0)
            return false;
     
        currentStamina += value;
        if (currentStamina > maxStamina)
            currentStamina = maxStamina;

        return true;
    }
    
    public bool ModifyMana(int value)
    {
        if (currentMana + value < 0)
            return false;
        
        currentStamina += value;
        if (currentMana > maxMana)
            currentMana = maxMana;
        
        return true;
    }

    public void RegenerateStamina()
    {
        currentStamina += Mathf.RoundToInt(Time.deltaTime);
    }
    
}
