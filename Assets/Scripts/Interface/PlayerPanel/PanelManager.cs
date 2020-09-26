using System.Collections.Generic;
using UnityEngine;


public class PanelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> panels;
    [SerializeField] private GameObject tooltip;
    [SerializeField] private GameObject playerPanel;

    private void Start()
    {
        KeepOnScene.Instance.GetComponentInChildren<PlayerButtonCallBack>().onStateChanged += OpenClose;
    }

    private void OpenClose()
    {
        //Return Joystick to 0 position;
        if (InterfaceManager.Instance.fixedJoystick != null)
            InterfaceManager.Instance.fixedJoystick.ResetInput();

        foreach (var panel in panels)
            if (panel != null)
            {
                bool isActive = panel.activeSelf;
                panel.SetActive(!isActive);
            }

        if (tooltip != null && playerPanel != null)
            if (!playerPanel.activeSelf)
                tooltip.SetActive(false);
        

    }
    
    
}
