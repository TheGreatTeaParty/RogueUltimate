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
    private int _currentMana;
    private int _currentStamina;
    [SerializeField] 
    private float regenerationSpeed;
    [SerializeField] 
    private Animator animator;
    [SerializeField]
    private Transform LevelUpEffect;

    public int maxMana = 100;
    public int maxStamina = 100;
    

    public delegate void OnChangeCallback();
    public OnChangeCallback onChangeCallback;


    private void Start()
    {
        _regenerationCoolDown = 0;
        regenerationSpeed = 1;
        currentHealth = maxHealth;
        _currentStamina = maxStamina;
        _currentMana = maxMana;
        
        //Set max health
        InterfaceOnScene.Instance.GetComponentInChildren<HealthBar>().SetMaxValue(maxHealth);
        InterfaceOnScene.Instance.GetComponentInChildren<StaminaBar>().SetMaxValue(maxStamina);
        InterfaceOnScene.Instance.GetComponentInChildren<ManaBar>().SetMaxValue(maxMana);

        if (EquipmentManager.Instance != null)
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

    public void GainXP(int gainedXP)
    {
        _xp += gainedXP;
        switch (level)
        {
            // * 2
            case 1 when _xp >= 220:
            case 2 when _xp >= 440:
            case 3 when _xp >= 700:
            case 4 when _xp >= 980:
            case 5 when _xp >= 1300:
            case 6 when _xp >= 1640:
            case 7 when _xp >= 2020:    
            case 8 when _xp >= 2460:
            case 9 when _xp >= 2920:
            case 10 when _xp > 3440:
            case 11 when _xp > 4000:    
            case 12 when _xp > 4640:
            case 13 when _xp > 5340:
                LevelUp();
                break;
        }
        
        onChangeCallback.Invoke();
    }
    
    private void LevelUp()
    {
        //Sound + LevelUpFX
        AudioManager.Instance.Play("LevelUp");
        KeepOnScene.instance.GetComponent<PlayerFX>().SpawnEffect(LevelUpEffect);

        level++;
        _xp = 0;
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

    public void SetCurrentMana(int mana)
    {
        _currentMana = mana;
    }
    public void SetCurrentStamina(int stamina)
    {
        _currentStamina = stamina;
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
        InterfaceOnScene.Instance.Hide();
        InterfaceOnScene.Instance.gameObject.GetComponentInChildren<DiePanel>().PlayerDie(); //Opens Window with a decision |Adverb to continue| or |Humility|
        //transform.position = new Vector2(100, 100);
        //Destroy or set Active(faulse) 


        //Destroy(gameObject);
        //Destroy(InterfaceOnScene.Instance.gameObject);
        //SceneManager.LoadScene("Menu");

    }
    
}
