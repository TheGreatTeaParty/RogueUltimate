using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealthBar : MonoBehaviour
{
    private EnemyStat enemyStat;
    [SerializeField] private TextMeshPro Name;
    [SerializeField] private TextMeshPro Shadow;
    [SerializeField] private TextMeshPro LvL;
    [SerializeField] private Slider SliderHealth;

    // Start is called before the first frame update
    void Start()
    {
        enemyStat = GetComponentInParent<EnemyStat>();
        enemyStat.onReceivedDamage += ChangeHealth;
        SliderHealth.maxValue = enemyStat.GetMaxHealth();
        SliderHealth.value = SliderHealth.maxValue;
        Name.text = enemyStat.GetName();
        Shadow.text = Name.text;
        //Set Level
    }

    void ChangeHealth(int damage)
    {
        SliderHealth.value = enemyStat.GetCurrentHealth();
    }
}
