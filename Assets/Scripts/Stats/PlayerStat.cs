using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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
    private int _xp;
    private int[] _xpToNextLevel = 
    {
        // multiplied by 3
        220, // 1
        440, // 2
        700, // 3
        980, // 4
        1300, // 5 
        1640, // 6
        2020, // 7
        2460, // 8
        2920, // 9
        3440, // 10
        4000, // 11
        4640, // 12
        5340, // 13
        
    };
    private int _currentMana;
    private int _currentStamina;
    
    // Delete getters for maxvalues ?
    public int maxMana;
    public int maxStamina;
    [Space]
    public int strength;
    public int agility;
    public int intelligence;
    [Space]
    public float regenerationSpeed;
    [Space]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform LevelUpEffect;


    public delegate void OnChangeCallback();
    public OnChangeCallback onChangeCallback;


    private void Start()
    {
        _regenerationCoolDown = 0;
        regenerationSpeed = 1;
        EquipmentManager.Instance.onEquipmentChanged += OnEquipmentChanged;
        
        if (SaveManager.LoadPlayer() != null)
        {
            var data = SaveManager.LoadPlayer();

            level = data.level;
            _xp = data.xp;

            strength = data.strength;
            agility = data.dexterity;
            intelligence = data.intelligence;

            maxHealth = data.maxHP;
            maxStamina = data.maxSP;
            maxMana = data.maxMP;
            
            currentHealth = data.currentHP;
            _currentStamina = data.currentSP;
            _currentMana = data.currentMP;
            
            int modifier = level * 10;
            physicalDamage.AddModifier(modifier);
            magicDamage.AddModifier(modifier);
            physicalProtection.AddModifier(modifier);
            magicProtection.AddModifier(modifier);
            
            return;
        }

        currentHealth = maxHealth;
        _currentStamina = maxStamina;
        _currentMana = maxMana;

        _xp = 0;
        level = 1;
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

    public void GainXP(int gainedXP)
    {
        if (level >= 20) return; // max level
        
        _xp += gainedXP;
        while (_xp > _xpToNextLevel[level - 1]) 
        {
            _xp -= _xpToNextLevel[level - 1];
            LevelUp();
        }
        
        onChangeCallback.Invoke();
    }
    
    private void LevelUp()
    {
        level++;
        
        int modifier = level * 10;
        physicalDamage.AddModifier(modifier);
        magicDamage.AddModifier(modifier);
        physicalProtection.AddModifier(modifier);
        magicProtection.AddModifier(modifier);
        
        //Sound + LevelUpFX
        AudioManager.Instance.Play("LevelUp");
        KeepOnScene.instance.GetComponent<PlayerFX>().SpawnEffect(LevelUpEffect);
    }
    
    public bool ModifyHealth(int value)
    {
        if (currentHealth + value < 0)
            return false;

        currentHealth += value;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        onChangeCallback?.Invoke();
        return true;
    }

    public bool ModifyStamina(int value)
    {
        if (_currentStamina + value < 0)
            return false;

        _currentStamina += value;
        if (_currentStamina > maxStamina)
            _currentStamina = maxStamina;

        onChangeCallback?.Invoke();
        return true;
    }

    public bool ModifyMana(int value)
    {
        if (_currentMana + value < 0)
            return false;

        _currentMana += value;
        if (_currentMana > maxMana)
            _currentMana = maxMana;

        onChangeCallback?.Invoke();
        return true;
    }

    public int GetCurrentMana()
    {
        return _currentMana;
    }

    public int GetCurrentStamina()
    {
        return _currentStamina;
    }

    public int GetMaxStamina()
    {
        return maxStamina;
    }

    public int GetMaxMana()
    {
        return maxMana;
    }
    
    public void SetCurrentMana(int mana)
    {
        _currentMana = mana;
    }
    public void SetCurrentStamina(int stamina)
    {
        _currentStamina = stamina;
    }

    public void SetCurrentHealth(int health)
    {
        currentHealth = health;
    }

    public void RegenerateStamina()
    {
        _currentStamina += Mathf.RoundToInt(Time.deltaTime);
        onChangeCallback?.Invoke();
    }

    public int GetXP()
    {
        return _xp;
    }

    public int GetXPToNextLevel(int level)
    {
        return _xpToNextLevel[level - 1];
    }

    public override void TakeDamage(int _physicalDamage, int _magicDamage)
    {
        base.TakeDamage(_physicalDamage, _magicDamage);
        animator.SetTrigger("Taking Dmg");
        
        
        
    }

    public override void Die()
    {
        animator.SetTrigger("Die");
        PlayerStat.Instance.gameObject.layer = 2;
        PlayerStat.Instance.gameObject.tag = "Untagged";
        InterfaceOnScene.Instance.HideMainElements();
        InterfaceOnScene.Instance.gameObject.GetComponentInChildren<DiePanel>().PlayerDie(); //Opens Window with a decision |Adverb to continue| or |Humility|
        //transform.position = new Vector2(100, 100);
        //Destroy or set Active(faulse) 


        //Destroy(gameObject);
        //Destroy(InterfaceOnScene.Instance.gameObject);
        //SceneManager.LoadScene("Menu");

    }
    
}
