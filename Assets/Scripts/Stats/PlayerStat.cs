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
    [SerializeField] private float regenerationSpeed;
    [SerializeField] private Animator animator;

    public int maxMana = 100;
    public int maxStamina = 100;
    
    public int currentMana;
    public int currentStamina;

    
    public delegate void OnChangeCallback();
    public OnChangeCallback onChangeCallback;
    
    
    private void Start()
    {
        _regenerationCoolDown = 0;
        regenerationSpeed = 1;
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        currentMana = maxMana;

        

        //Set max health
        InterfaceOnScene.instance.GetComponentInChildren<HealthBar>().SetMaxValue(maxHealth);
        InterfaceOnScene.instance.GetComponentInChildren<StaminaBar>().SetMaxValue(maxStamina);
        InterfaceOnScene.instance.GetComponentInChildren<ManaBar>().SetMaxValue(maxMana);

        if (EquipmentManager.Instance !=null)
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

        onChangeCallback?.Invoke();
        return true;
    }
    
    public bool ModifyStamina(int value)
    {
        if (currentStamina + value < 0)
            return false;
     
        currentStamina += value;
        if (currentStamina > maxStamina)
            currentStamina = maxStamina;

        onChangeCallback?.Invoke();
        return true;
    }
    
    public bool ModifyMana(int value)
    {
        if (currentMana + value < 0)
            return false;
        
        currentMana += value;
        if (currentMana > maxMana)
            currentMana = maxMana;

        onChangeCallback?.Invoke();
        return true;
    }

    public void RegenerateStamina()
    {
        currentStamina += Mathf.RoundToInt(Time.deltaTime);
        onChangeCallback?.Invoke();
    }

    public override void TakeDamage(int _physicalDamage, int _magicDamage)
    {
        base.TakeDamage(_physicalDamage, _magicDamage);
        animator.SetTrigger("Taking Dmg");
        
        
        
    }

    public override void Die()
    {
        //Sets player's tag to playerIsDead
        animator.SetTrigger("Die");
        //Opens Window with a decision |Adverb to continue| or |Humility|
        //transform.position = new Vector2(100, 100);
        //Destroys or set Active(faulse)
        //Deleate all player's data
        
        InterfaceOnScene.instance.gameObject.SetActive(false);
        //Destroy(gameObject);
        //Destroy(InterfaceOnScene.instance.gameObject);
        //SceneManager.LoadScene("Menu");

    }
    
}
