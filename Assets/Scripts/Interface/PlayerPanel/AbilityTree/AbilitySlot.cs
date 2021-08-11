using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class AbilitySlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Ability _ability;
    [Space]
    public AbilitySlot[] NextAbilities;
    private bool _isLocked = true;
    [SerializeField]
    public bool _isUpgraded = false;
    [SerializeField]
    protected Image _image;
    private Image _outline;


    private readonly Color _normalColor = Color.white;
    private readonly Color _shadowColor = new Color(0.8f, 0.8f, 0.8f, 0.8f);
    private readonly Color _disabledColor = Color.clear;
    private readonly Color _locked = Color.grey;

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

    public bool IsLocked
    {
        get => _isLocked;
        set
        {
            _isLocked = value;

            if (_isLocked == true)
            {
                _image.color = _locked;
            }
            else if(!_isUpgraded)
            {
                _outline.enabled = true;
            }
        }
    }
    
    public event Action<AbilitySlot> OnClickEvent;
    public event Action<AbilitySlot> OnBeginDragEvent;
    public event Action<AbilitySlot> OnDragEvent;
    public event Action<AbilitySlot> OnEndDragEvent;
    public event Action<AbilitySlot> OnDropEvent;


    protected void Start()
    {
        var images = GetComponentsInChildren<Image>();
        _image = images[2];
        _outline = GetComponent<Image>();
        _outline.enabled = false;

        if (_ability != null)
        {
            _image.sprite = _ability.Sprite;
            _image.color = _locked;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_ability != null)
            OnClickEvent?.Invoke(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_ability == null || !_isUpgraded) return;
        
        _image.color = _shadowColor;
        OnBeginDragEvent?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {        
        OnDragEvent?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_ability == null || !_isUpgraded) return;
        OnEndDragEvent?.Invoke(this);
        if (_ability != null)
            _image.color = _normalColor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnDropEvent?.Invoke(this);
    }

    public void Unlock()
    {
        _outline.enabled = true;
    }

    public void UpgradeSkill()
    {
        _isUpgraded = true;
        _image.color = _normalColor;
        _outline.enabled = false;
    }
}