using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class AbilitySlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Ability _ability;
    protected Image _image;

    private readonly Color _normalColor = Color.white;
    private readonly Color _shadowColor = new Color(0.8f, 0.8f, 0.8f, 0.8f);
    private readonly Color _disabledColor = Color.clear;
    
    public Ability Ability
    {
        get => _ability;
        set
        {
            _ability = value;

            if (_ability == null)
            {
                _image.sprite = null;
                _image.color = _disabledColor;
            }
            else
            {
                _image.sprite = _ability.Sprite;
                _image.color = _normalColor;
            }
        }
        
    }
    
    
    public event Action<AbilitySlot> OnClickEvent;
    public event Action<AbilitySlot> OnBeginDragEvent;
    public event Action<AbilitySlot> OnDragEvent;
    public event Action<AbilitySlot> OnEndDragEvent;
    public event Action<AbilitySlot> OnDropEvent;


    protected void Awake()
    {
        var images = gameObject.GetComponentsInChildren<Image>();
        
        for (int i = 0; i < images.Length; i++)
            if (images[i].gameObject.transform.parent.GetInstanceID() != GetInstanceID())
                _image = images[i];

        if (_ability != null)
        {
            _image.sprite = _ability.Sprite;
            _image.color = _normalColor;
        }
    }

    private void OnValidate()
    {
        var images = gameObject.GetComponentsInChildren<Image>();
        
        for (int i = 0; i < images.Length; i++)
            if (images[i].gameObject.transform.parent.GetInstanceID() != GetInstanceID())
                _image = images[i];
        
        if (_ability != null)
        {
            _image.sprite = _ability.Sprite;
            _image.color = _normalColor;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_ability != null)
            OnClickEvent?.Invoke(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_ability == null) return;
        
        _image.color = _shadowColor;
        OnBeginDragEvent?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {        
        OnDragEvent?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragEvent?.Invoke(this);
        if (_ability != null)
            _image.color = _normalColor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnDropEvent?.Invoke(this);
    }
    
}