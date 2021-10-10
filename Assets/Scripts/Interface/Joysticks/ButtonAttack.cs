using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;


public class ButtonAttack : MonoBehaviour, IUpdateSelectedHandler, IPointerDownHandler, IPointerUpHandler
{
    // Cache
    private PlayerAttack _playerAttack;
    private bool _isPressed;

    private void Start()
    {    
        // Cache
        _playerAttack = PlayerOnScene.Instance.playerAttack;
    }

    public void OnUpdateSelected(BaseEventData eventData)
    {
        if (_isPressed)
            _playerAttack.Attack();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
    }
}
