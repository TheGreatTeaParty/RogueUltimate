using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossFightPortal : MonoBehaviour
{
    [SerializeField] private GameObject healthbar;
    [SerializeField] private Slider bosshealthSlider;
    [SerializeField] private GameObject portal;

    #region Singleton
    public static BossFightPortal Instance;
    void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
    }
    #endregion

    public void SetBossHealth(float health)
    {
        bosshealthSlider.value = health;
    }

    
    public void TurnThePortal()
    {
        portal.SetActive(true);
    }

    public void HealthBar(bool value)
    {
        healthbar.SetActive(value);
    }
    
}
