using UnityEngine;
using UnityEngine.UI;

public class BossFightPortal : MonoBehaviour
{
    public GameObject healthbar;
    [SerializeField] private Slider bosshealthSlider;
    [SerializeField] private GameObject portal;
    [SerializeField] private GameObject portalEmpty;
    #region Singleton
    public static BossFightPortal Instance;
    void Start()
    {
        if (Instance != null)
            return;

        Instance = this;
        if(healthbar)
            healthbar.SetActive(false);
    }
    #endregion

    public void SetBossHealth(float health)
    {
        if(bosshealthSlider)
            bosshealthSlider.value = health;
    }

    
    public void TurnThePortal()
    {
        portal.SetActive(true);
        portalEmpty.SetActive(false);
    }

    public void HealthBar(bool value)
    {
        if(healthbar)
            healthbar.SetActive(value);
    }
    
}
