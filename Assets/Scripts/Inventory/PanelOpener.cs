using System.Collections.Generic;
using UnityEngine;


public class PanelOpener : MonoBehaviour
{ 
    [SerializeField] private List<GameObject> panels;
    
    
    public void OpenClose()
    {
        foreach (var panel in panels)
            if (panel != null)
            {
                bool isActive = panel.activeSelf;
                panel.SetActive(!isActive);
            }
    }
    
}
