using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickSlotOnScene : MonoBehaviour, IPointerClickHandler
{
    private Image _image;
    private TMP_Text _amountText;
    
    public event Action<QuickSlotOnScene> OnClickEvent;


    protected virtual void Awake()
    {
        var images = gameObject.GetComponentsInChildren<Image>();
        var texts = gameObject.GetComponentsInChildren<TMP_Text>();
        
        for (int i = 0; i < images.Length; i++)
            if (images[i].gameObject.transform.parent.GetInstanceID() != GetInstanceID())
                _image = images[i];
        
        for (int i = 0; i < texts.Length; i++)
            if (texts[i].gameObject.transform.parent.GetInstanceID() != GetInstanceID())
                _amountText = texts[i];
        
        _image.color = Color.clear;
        _amountText.SetText("");
    }

    public void SetItem(QuickSlot quickSlot)
    {
        if (quickSlot.Item != null)
        {
            _image.sprite = quickSlot.Item.Sprite;
            _image.color = Color.white;
            _amountText.SetText(quickSlot.Amount.ToString());
            return;
        }

        _image.sprite = null;
        _image.color = Color.clear;
        _amountText.SetText("");
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickEvent?.Invoke(this);
    }
    
}