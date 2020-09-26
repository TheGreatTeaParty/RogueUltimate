using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class NavigatorButton : MonoBehaviour, IPointerClickHandler
{
    private Image _image;
    public WindowType window;

    
    private void Start()
    {
        _image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InterfaceManager.Instance.playerPanelManager.ChangeWindow(window);
    }
    
}