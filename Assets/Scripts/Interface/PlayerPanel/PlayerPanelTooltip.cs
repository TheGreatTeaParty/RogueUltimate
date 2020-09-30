using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerPanelTooltip : MonoBehaviour
{
    #region Singleton
    public static PlayerPanelTooltip Instance;
    private void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
    }
    #endregion
    
}
