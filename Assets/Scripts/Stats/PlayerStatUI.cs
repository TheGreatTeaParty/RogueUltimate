using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStatUI : MonoBehaviour
{
    private PlayerStat _playerStat;

    [SerializeField] private TextMeshProUGUI levelPointsText;
    [SerializeField] private TextMeshProUGUI experiencePointsText;
    [Space]
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
        PlayerStat.Instance.onChangeCallback += UpdateUI;
        UpdateUI();
    }


    private void UpdateUI()
    {
        levelPointsText.SetText(_playerStat.level.ToString());
        experiencePointsText.SetText(_playerStat.GetXP().ToString());
        healthPointsText.SetText("" + _playerStat.currentHealth + " / " + _playerStat.maxHealth);
        physicalDamagePointsText.SetText(_playerStat.physicalDamage.GetValue().ToString());
        physicalProtectionPointsText.SetText(_playerStat.physicalProtection.GetValue().ToString());
        magicDamagePointsText.SetText(_playerStat.magicDamage.GetValue().ToString());
        magicProtectionPointsText.SetText(_playerStat.magicProtection.GetValue().ToString());
    }

}