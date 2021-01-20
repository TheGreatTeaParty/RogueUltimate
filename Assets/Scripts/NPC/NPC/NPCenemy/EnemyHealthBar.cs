using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealthBar : MonoBehaviour
{

    private const float DAMAGED_HEALTH_SHRINK_TIMER_MAX = 0.8f;

    private float damagedHealthShrinkTimer;
    private EnemyStat _enemyStat;
    [SerializeField] private TextMeshPro enemyName;
    [SerializeField] private TextMeshPro shadow;
    [SerializeField] private TextMeshPro level;
    [SerializeField] private Slider sliderHealth;
    [SerializeField] private Slider _damagedSlider;
    [SerializeField] private GameObject UI;

    
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
        TurnOffAllElements();
    }

    private void Update()
    {
        if (UI.activeSelf)
        {
            damagedHealthShrinkTimer -= Time.deltaTime;
            if (damagedHealthShrinkTimer < 0)
            {
                if (sliderHealth.value < _damagedSlider.value)
                {
                    float shrinkSpeed = 100f;
                    _damagedSlider.value -= shrinkSpeed * Time.deltaTime;
                }
            }
        }
    }
    private void ChangeHealth(float damage)
    {
        if (!UI.activeSelf)
            TurnOnAllElements();
        damagedHealthShrinkTimer = DAMAGED_HEALTH_SHRINK_TIMER_MAX;
        sliderHealth.value = _enemyStat.CurrentHealth;
        
    }

    private void DeleteBar()
    {
        Destroy(gameObject);
    }

    private void TurnOnAllElements()
    {
        UI.SetActive(true);
    }
    private void TurnOffAllElements()
    {
        UI.SetActive(false);
    }
}
