using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class NavigatorButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image image;
    public WindowType windowType;
    
    private Color unselectedColor = new Color(255, 255, 255, 150);
    private Color selectedColor = new Color(255, 255, 255, 255);
    
    
    public event Action<WindowType, NavigatorButton> onWindowChanged; 
    
    
    private void Start()
    {
        image.color = unselectedColor;
        Debug.Log("Start(), " + image.color);
    }

    public void Highlight(bool state)
    {
        if (state)
        {
            gameObject.transform.localScale.Set(1.1f, 1.1f, 1f);
            image.color = selectedColor;
            Debug.Log("True state");
        }
        else
        {
            gameObject.transform.localScale.Set(1f, 1f, 1f);
            image.color = unselectedColor;
            Debug.Log("False state");
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        onWindowChanged?.Invoke(windowType, this);
    }
    
}