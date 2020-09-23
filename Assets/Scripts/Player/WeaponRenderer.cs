using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRenderer : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _weaponSprite;
    private bool _rangeEquiped;
    private Vector2 _direction;
    private Sprite[] WeaponAttackAnim;

    
    private void Start()
    {
        _playerMovement = KeepOnScene.Instance.GetComponent<PlayerMovement>();
        _playerSprite = KeepOnScene.Instance.GetComponent<SpriteRenderer>();
        _weaponSprite = GetComponent<SpriteRenderer>();
        EquipmentManager.Instance.onEquipmentChanged += OnWeaponChanged;
    }
    
    private void Update()
    {
        _direction = _playerMovement.GetDirection();

        if (_direction.y > 0.01f && (_direction.x < 0.65f && _direction.x > - 0.65f))
            _weaponSprite.sortingOrder = _playerSprite.sortingOrder - 1;
        else
            _weaponSprite.sortingOrder = _playerSprite.sortingOrder + 1;
    }
    private void OnWeaponChanged(EquipmentItem _new, EquipmentItem _old)
    {
        if (_new)
        {
            if (_new.Echo() != WeaponType.Melee)
            {
                _rangeEquiped = true;
                WeaponAttackAnim = _new.Animation;
            }
        }
        else
        {
            _rangeEquiped = false;
            WeaponAttackAnim = null;
        }
    }
    private void LateUpdate()
    {
        if (_rangeEquiped)
        {
            string index = "";
            if (_weaponSprite.sprite.name[_weaponSprite.sprite.name.Length - 2] != '_')
            {
                if (_weaponSprite.sprite.name[_weaponSprite.sprite.name.Length - 3] != '_')
                    index += _weaponSprite.sprite.name[_weaponSprite.sprite.name.Length - 3];
                index += _weaponSprite.sprite.name[_weaponSprite.sprite.name.Length - 2];
            }
            index += _weaponSprite.sprite.name[_weaponSprite.sprite.name.Length - 1];

            if (WeaponAttackAnim.Length == 0)
                Debug.LogWarning("Missing Animation Sprites");

            else
            {
                if (int.TryParse(index, out int j))
                {
                    if (j >= WeaponAttackAnim.Length)
                    {
                        Debug.LogWarning($"Sprite with Index: {j} does not exist in Animation Sprites!");
                        _weaponSprite.sprite = null;
                    }
                    else
                    {   //Need to be added +4;
                        _weaponSprite.sprite = WeaponAttackAnim[j];
                    }
                }
            }
        }
    }
}
