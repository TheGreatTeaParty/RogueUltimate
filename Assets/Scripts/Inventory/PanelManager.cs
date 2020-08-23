using System.Collections.Generic;
using UnityEngine;


public class PanelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> panels;
    [SerializeField] private GameObject tooltip;
    [SerializeField] private GameObject playerPanel;
    

    public void OpenClose()
    {
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
