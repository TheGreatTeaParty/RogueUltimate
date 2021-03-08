using System.Globalization;
using TMPro;
using UnityEngine;


public class StatUI : MonoBehaviour
{
   private PlayerStat _playerStat;
   private StatButton[] _statButtons;
  
   public Transform attributes;
   [Space] 
   public TMP_Text maxWillPoints;
   public TMP_Text maxHealthPoints;
   public TMP_Text maxStaminaPoints;
   public TMP_Text maxManaPoints;
   [Space] 
   public TMP_Text physicalDamagePoints;
   public TMP_Text magicDamagePoints;
   public TMP_Text physicalProtectionPoints;
   public TMP_Text magicProtectionPoints;
   [Space]
   public TMP_Text critDamage;
   public TMP_Text dmgResistance;
   [Space]
   public TMP_Text magicalcritDamage;
   public TMP_Text effectResistance;
   [Space]
    public TMP_Text critChance;
    public TMP_Text dodgeChance;
   [Space]
    public TMP_Text points;


    private void Start()
   {
      _playerStat = CharacterManager.Instance.Stats;
      _playerStat.onChangeCallback += UpdateUI;
      
      _statButtons = attributes.GetComponentsInChildren<StatButton>();
      for (int i = 0; i < _statButtons.Length; i++)
         _statButtons[i].onClickEvent += _playerStat.AddAttributePoint;
      
      UpdateUI();
   }

    private void UpdateUI()
    {
        // HP, SP, MP
        maxHealthPoints.SetText(_playerStat.Strength.MaxHealth.Value.ToString(CultureInfo.CurrentUICulture));
        maxStaminaPoints.SetText(_playerStat.Agility.MaxStamina.Value.ToString(CultureInfo.CurrentUICulture));
        maxManaPoints.SetText(_playerStat.Intelligence.MaxMana.Value.ToString(CultureInfo.CurrentUICulture));
        //maxWillPoints.SetText(_playerStat.MaxWill.ToString(CultureInfo.CurrentUICulture));

        // Protection & damage
        physicalDamagePoints.SetText(_playerStat.PhysicalDamage.Value.ToString(CultureInfo.CurrentUICulture));
        physicalProtectionPoints.SetText(_playerStat.PhysicalProtection.Value.ToString(CultureInfo.CurrentUICulture));
        magicDamagePoints.SetText(_playerStat.MagicDamage.Value.ToString(CultureInfo.CurrentUICulture));
        magicProtectionPoints.SetText(_playerStat.MagicProtection.Value.ToString(CultureInfo.CurrentUICulture));

        //Stats:
        critDamage.SetText(_playerStat.Strength.CritDamage.Value.ToString(CultureInfo.CurrentUICulture));
        dmgResistance.SetText(_playerStat.Strength.PoisonResistance.Value.ToString(CultureInfo.CurrentUICulture));
        magicalcritDamage.SetText(_playerStat.Intelligence.MagicalCrit.Value.ToString(CultureInfo.CurrentUICulture));
        effectResistance.SetText(_playerStat.Intelligence.FireResistance.Value.ToString(CultureInfo.CurrentUICulture));
        critChance.SetText(_playerStat.Agility.CritChance.Value.ToString(CultureInfo.CurrentUICulture));
        dodgeChance.SetText(_playerStat.Agility.dodgeChance.Value.ToString(CultureInfo.CurrentUICulture));

        //Points:
        points.SetText(_playerStat.StatPoints.ToString(CultureInfo.CurrentUICulture));
    }
   
}