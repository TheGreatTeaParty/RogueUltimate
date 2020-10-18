using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealthBar : MonoBehaviour
{
    private EnemyStat _enemyStat;
    [SerializeField] private TextMeshPro enemyName;
    [SerializeField] private TextMeshPro shadow;
    [SerializeField] private TextMeshPro level;
    [SerializeField] private Slider sliderHealth;

    
    void Start()
    {
        _enemyStat = GetComponentInParent<EnemyStat>();
        _enemyStat.onReceivedDamage += ChangeHealth;
        _enemyStat.onDie += DeleteBar;
        sliderHealth.maxValue = _enemyStat.MaxHealth;
        sliderHealth.value = sliderHealth.maxValue;
        enemyName.text = _enemyStat.CharacterName;
        shadow.text = enemyName.text;
        level.text = _enemyStat.Level.ToString();
    }

    private void ChangeHealth(float damage)
    {
        sliderHealth.value = _enemyStat.CurrentHealth;
    }

    private void DeleteBar()
    {
        Destroy(gameObject);
    }
    
}
