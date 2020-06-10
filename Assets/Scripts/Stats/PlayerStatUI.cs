using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStatUI : MonoBehaviour
{
    private PlayerStat _playerStat;

    [SerializeField] private TextMeshProUGUI healthPointsText;
    [Space]
    [SerializeField] private TextMeshProUGUI physicalDamagePointsText;
    [SerializeField] private TextMeshProUGUI physicalProtectionPointsText;
    [SerializeField] private TextMeshProUGUI magicDamagePointsText;
    [SerializeField] private TextMeshProUGUI magicProtectionPointsText;


    private void Start()
    {
        _playerStat = PlayerStat.Instance;
        
        EquipmentManager.Instance.onEquipmentCallback += UpdateUI;   
        UpdateUI();
    }


    private void UpdateUI()
    {
        healthPointsText.SetText("" + _playerStat.CurrentHealth + " / " + _playerStat.maxHealth);
        physicalDamagePointsText.SetText("" + _playerStat.physicalDamage.GetValue());
        physicalProtectionPointsText.SetText("" + _playerStat.physicalProtection.GetValue());
        magicDamagePointsText.SetText("" + _playerStat.magicDamage.GetValue());
        magicProtectionPointsText.SetText("" + _playerStat.magicProtection.GetValue());
    }

}