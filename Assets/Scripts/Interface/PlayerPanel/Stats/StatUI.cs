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
      maxHealthPoints.SetText(_playerStat.MaxHealth.ToString(CultureInfo.CurrentUICulture));
      maxStaminaPoints.SetText(_playerStat.MaxStamina.ToString(CultureInfo.CurrentUICulture));
      maxManaPoints.SetText(_playerStat.MaxMana.ToString(CultureInfo.CurrentUICulture));
      maxWillPoints.SetText(_playerStat.MaxWill.ToString(CultureInfo.CurrentUICulture));

      // Protection & damage
      physicalDamagePoints.SetText(_playerStat.PhysicalDamage.Value.ToString(CultureInfo.CurrentUICulture));
      physicalProtectionPoints.SetText(_playerStat.PhysicalProtection.Value.ToString(CultureInfo.CurrentUICulture));
      magicDamagePoints.SetText(_playerStat.MagicDamage.Value.ToString(CultureInfo.CurrentUICulture));
      magicProtectionPoints.SetText(_playerStat.MagicProtection.Value.ToString(CultureInfo.CurrentUICulture));
      
   }
   
}