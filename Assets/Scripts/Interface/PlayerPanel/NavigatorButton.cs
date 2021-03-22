using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class NavigatorButton : MonoBehaviour, IPointerClickHandler
{
    private Image _image;
    private Color _unselectedColor = new Color(0.8f, 0.8f, 0.8f);
    private Color _selectedColor = new Color(1f, 1f, 1f);
    
    public WindowType windowType;
    
    public event Action<WindowType, NavigatorButton> onWindowChanged; 
    
    
    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.color = _unselectedColor;
    }

    public void Highlight(bool state)
    {
        if (state)
        {
            gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
            _image.color = _selectedColor;
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            _image.color = _unselectedColor;
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        onWindowChanged?.Invoke(windowType, this);
        //Call test sound:
        AudioManager.Instance.Play("UIclick");
    }
    
}