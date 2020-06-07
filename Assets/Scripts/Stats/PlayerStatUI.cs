using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStatUI : MonoBehaviour
{
    private PlayerStat _playerStat;
    
    private Stat physicalDamagePoints;
    [SerializeField] private TextMeshProUGUI physicalDamagePointsText;


    private void Start()
    {
        EquipmentManager.Instance.onEquipmentCallback += UpdateUI;
        
        if (physicalDamagePointsText == null) 
            Debug.Log("damageStat doesn't find itself textMeshPro");
        
        _playerStat = PlayerStat.Instance;
        physicalDamagePoints = _playerStat.physicalDamage;
        
        UpdateUI();
    }


    private void UpdateUI()
    {
        physicalDamagePointsText.SetText("" + physicalDamagePoints.GetValue());
    }

}