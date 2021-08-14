using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class NavigatorButton : MonoBehaviour, IPointerClickHandler
{
    private Image _image;
    private Color _unselectedColor = new Color(0.8f, 0.8f, 0.8f);
    private Color _selectedColor = new Color(1f, 1f, 1f);
    [SerializeField]
    private Image outline;
    [SerializeField]
    private Animator animator;

    public WindowType windowType;
    
    public event Action<WindowType, NavigatorButton> onWindowChanged; 
    
    
    private void Start()
    {
        if(outline)
            outline.enabled = false;
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
        TurnOff();
        onWindowChanged?.Invoke(windowType, this);
        //Call test sound:
        AudioManager.Instance.Play("UIclick");
    }

    public void TurnOn()
    {
        outline.enabled = true;
    }

    private void TurnOff()
    {
        if (outline&&animator)
        {
            outline.enabled = false;
            animator.SetBool("Outline", false);
        }
    }
    
    public void CheckAnimator()
    {
        if (outline)
        {
            if (outline.enabled)
                if(animator.isActiveAndEnabled)
                    animator.SetBool("Outline", true);
        }
    }
}