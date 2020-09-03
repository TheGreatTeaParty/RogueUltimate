using System.Collections.Generic;
using UnityEngine;


public class PanelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> panels;
    [SerializeField] private GameObject tooltip;
    [SerializeField] private GameObject playerPanel;

    private void Start()
    {
        KeepOnScene.instance.GetComponentInChildren<PlayerButtonCallBack>().onStateChanged += OpenClose;
    }

    private void OpenClose()
    {
        //Return Joystick to 0 position;
        if(InterfaceOnScene.Instance.GetComponentInChildren<FixedJoystick>()!= null)
            InterfaceOnScene.Instance.GetComponentInChildren<FixedJoystick>().ResetInput();

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
