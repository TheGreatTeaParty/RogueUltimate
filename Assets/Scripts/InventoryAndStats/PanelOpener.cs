using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    public GameObject panel;

    public void OpenClose()
    {
        if (panel != null)
        {
            bool isActive = panel.activeSelf;
            panel.SetActive(!isActive);
        }
    }
    
}
