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
    private int _statPoints = 0;
    private float _currentMana;
    private float _currentStamina;
    
    public float maxMana;
    public float maxStamina;
    public float maxWill;
    [Space] 
    public Stat physique;
    public Stat will;
    public Stat mind;
    public Stat reaction;
    [Space]
    public Stat attackSpeed;
    public Stat blockStrength;
    public Stat castSpeed;
    public Stat dodgeChance;
    public Stat critDamage;
    public Stat critChance;
    [Space]
    public float regenerationSpeed;
    [Space]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform LevelUpEffect;

    public float CurrentMana
    {
        get => _currentMana;
        set => _currentMana = value;
    }
    public float CurrentStamina
    {
        get => _currentStamina;
        set => _currentStamina = value;
    }
    public float MaxMana
    {
        get => maxMana;
        set => maxMana = value;
    }
    public float MaxStamina
    {
        get => maxStamina;
        set => maxStamina = value;
    }
    public float MaxWill
    {
        get => maxWill;
        set => maxWill = value;
    }
    public int XP
    {
        get => _xp;
        set => _xp = value;
    }
    public int StatPoints
    {
        get => _statPoints;
        set => _statPoints = value;
    }


    public delegate void OnChangeCallback();
    public OnChangeCallback onChangeCallback;
    
    
    private void Start()
    {
        _regenerationCoolDown = 0;
        regenerationSpeed = 5;

        if (SaveManager.LoadPlayer() != null)
        {
            var data = SaveManager.LoadPlayer();

            level = data.level;
            _xp = data.xp;
            

            maxHealth = data.maxHP;
            maxStamina = data.maxSP;
            maxMana = data.maxMP;
            
            currentHealth = data.currentHP;
            _currentStamina = data.currentSP;
            _currentMana = data.currentMP;

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
        RegenerateStamina();
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
        _statPoints += 1;

        //Sound + LevelUpFX
        AudioManager.Instance.Play("LevelUp");
        PlayerOnScene.Instance.playerFX.SpawnEffect(LevelUpEffect);
    }
    
    public void AddAttributePoint(StatType statType)
    {
        switch (statType)
        {
            case StatType.Will:
            {
                will.AddModifier(new StatModifier(1, StatModifierType.Flat));
                break;
            }
         
            case StatType.Physique:
            {
                physique.AddModifier(new StatModifier(1, StatModifierType.Flat));
                break;
            }
         
            case StatType.Mind:
            {
                mind.AddModifier(new StatModifier(1, StatModifierType.Flat));
                break;
            }
         
            case StatType.Reaction:
            {
                reaction.AddModifier(new StatModifier(1, StatModifierType.Flat));
                break;
            }
        }

        onChangeCallback.Invoke();
    }

    public void AddAttributePoint(StatType statType, int value = 1)
    {
        for (int i = 0; i < value; i++)
            AddAttributePoint(statType);
    }

    public bool ModifyHealth(float value)
    {
        if (currentHealth + value < 0)
            return false;

        currentHealth += value;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        onChangeCallback?.Invoke();
        return true;
    }
    
    public bool ModifyStamina(float value)
    {
        if (_currentStamina + value < 0)
            return false;

        _currentStamina += value;
        if (_currentStamina > maxStamina)
            _currentStamina = maxStamina;

        onChangeCallback?.Invoke();
        return true;
    }
    
    public bool ModifyMana(float value)
    {
        if (_currentMana + value < 0)
            return false;

        _currentMana += value;
        if (_currentMana > maxMana)
            _currentMana = maxMana;

        onChangeCallback?.Invoke();
        return true;
    }
    
    public void RegenerateStamina()
    {
        _regenerationCoolDown += Time.deltaTime * regenerationSpeed;
        if (_regenerationCoolDown > 1)
        {
            ModifyStamina(1);
            _regenerationCoolDown = 0;
        }
    }

    public int GetXPToNextLevel(int level)
    {
        return _xpToNextLevel[level - 1];
    }

    public override void TakeDamage(float physicalDamage, float magicDamage)
    {
        base.TakeDamage(physicalDamage, magicDamage);
        animator.SetTrigger("Taking Dmg");
    }

    public override void Die()
    {
        animator.SetTrigger("Die");
        PlayerStat.Instance.gameObject.layer = 2;
        PlayerStat.Instance.gameObject.tag = "Untagged";
        InterfaceManager.Instance.HideFaceElements();
        InterfaceManager.Instance.gameObject.GetComponentInChildren<DiePanel>().PlayerDie(); //Opens Window with a decision |Adverb to continue| or |Humility|
        //transform.position = new Vector2(100, 100);
        //Destroy or set Active(false) 


        //Destroy(gameObject);
        //Destroy(InterfaceManager.Instance.gameObject);
        //SceneManager.LoadScene("Menu");

    }
    
}
