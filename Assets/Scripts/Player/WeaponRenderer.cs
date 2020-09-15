using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRenderer : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private Renderer _playerSprite;
    private Renderer _weaponSprite;
    private Vector2 _direction;

    
    private void Start()
    {
        _playerMovement = KeepOnScene.Instance.GetComponent<PlayerMovement>();
        _playerSprite = KeepOnScene.Instance.GetComponent<Renderer>();
        _weaponSprite = GetComponent<Renderer>();
    }
    
    private void Update()
    {
        _direction = _playerMovement.GetDirection();

        if (_direction.y > 0.01f && (_direction.x < 0.65f && _direction.x > - 0.65f))
            _weaponSprite.sortingOrder = _playerSprite.sortingOrder - 1;
        else
            _weaponSprite.sortingOrder = _playerSprite.sortingOrder + 1;
    }
    
}
