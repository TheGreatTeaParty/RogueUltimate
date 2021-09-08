using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;


public class StatUI : MonoBehaviour
{
   private PlayerStat _playerStat;

   [Space] 
   public TMP_Text physicalDamagePoints;
   public TMP_Text magicDamagePoints;
   public TMP_Text physicalProtectionPoints;
   public TMP_Text magicProtectionPoints;

   [Space]
   public TMP_Text AttackSpeed;
   public TMP_Text CastSpeed;
   public TMP_Text MovementSpeed;
   [Space]
   public TMP_Text Level;
   public TMP_Text Name;

    private IEnumerator Start()
   {
      yield return new WaitForSeconds(0.0001f);
      
      _playerStat = CharacterManager.Instance.Stats;
      _playerStat.onChangeCallback += UpdateUI;
     
      UpdateUI();
   }

    private void UpdateUI()
    {
        // Protection & damage
        physicalDamagePoints.SetText(_playerStat.PhysicalDamage.Value.ToString());
        physicalProtectionPoints.SetText(_playerStat.PhysicalProtection.Value.ToString());
        magicDamagePoints.SetText(_playerStat.MagicDamage.Value.ToString());
        magicProtectionPoints.SetText(_playerStat.MagicProtection.Value.ToString());

        //Stats:
        AttackSpeed.SetText(_playerStat.AttackSpeed.Value.ToString());
        CastSpeed.SetText(_playerStat.CastSpeed.Value.ToString());
        MovementSpeed.SetText(_playerStat.playerMovement.GetCurentMovementSpeed().ToString());

        //Points:
        Level.SetText(_playerStat.Level.ToString());
        Name.SetText(_playerStat.CharacterName);
    }
   
}