using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealthBar : MonoBehaviour
{

    private const float DAMAGED_HEALTH_SHRINK_TIMER_MAX = 0.8f;

    private float damagedHealthShrinkTimer;
    private EnemyStat _enemyStat;
    [SerializeField] private TextMeshProUGUI enemyName;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private Slider sliderHealth;
    [SerializeField] private Slider _damagedSlider;
    [SerializeField] private GameObject UI;
    [Space]
    [SerializeField] private GameObject _phDeffense;
    [SerializeField] private TextMeshProUGUI _phValue;
    [SerializeField] private GameObject _mpDeffense;
    [SerializeField] private TextMeshProUGUI _mpValue;

    void Start()
    {
        _enemyStat = GetComponentInParent<EnemyStat>();
        _enemyStat.onReceivedDamage += ChangeHealth;
        _enemyStat.onDie += DeleteBar;
        sliderHealth.maxValue = _enemyStat.MaxHealth;
        sliderHealth.value = sliderHealth.maxValue;
        enemyName.text = _enemyStat.CharacterName;
        level.text = _enemyStat.Level.ToString();

        CheckDeffense();

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
    private void CheckDeffense()
    {
        if (_phDeffense)
        {
            if (_enemyStat.PhysicalProtection.Value != 0)
            {
                _phDeffense.SetActive(true);
                _phValue.SetText(_enemyStat.PhysicalProtection.Value.ToString());
            }
        }
        if (_mpDeffense)
        {
            if (_enemyStat.MagicProtection.Value != 0)
            {
                _mpDeffense.SetActive(true);
                _mpValue.SetText(_enemyStat.PhysicalProtection.Value.ToString());
            }
        }
    }
    private void ChangeHealth(float damage,bool crit)
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
