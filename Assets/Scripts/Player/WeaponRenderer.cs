using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRenderer : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _weaponSprite;
    private Animator _weaponAnimator;
    private bool _rangeEquiped;
    private Vector2 _direction;
    private Sprite[] WeaponAttackAnim;

    
    private void Start()
    {
        CharacterManager.Instance.onEquipmentChanged += OnWeaponChanged;
        _weaponAnimator = GetComponent<Animator>();
        _playerMovement = PlayerOnScene.Instance.GetComponent<PlayerMovement>();
        _playerSprite = PlayerOnScene.Instance.GetComponent<SpriteRenderer>();
        _weaponSprite = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        _direction = _playerMovement.GetDirection();

        if (_direction.y > 0.01f && (_direction.x < 0.65f && _direction.x > - 0.65f))
            _weaponSprite.sortingOrder = _playerSprite.sortingOrder - 2;
        else
            _weaponSprite.sortingOrder = _playerSprite.sortingOrder + 2;
    }
    private void OnWeaponChanged(EquipmentItem _new, EquipmentItem _old)
    {
        if (_new)
        {
            if (_new.Echo() != WeaponType.Melee && _new.equipmentType == EquipmentType.Weapon)
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
        if (_rangeEquiped && _weaponAnimator.GetBool("Attack") && _weaponAnimator.GetInteger("Set") < 1)
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
                    {   //Need to be added;
                        _weaponSprite.sprite = WeaponAttackAnim[j];
                        
                    }
                }
            }
        }
    }
}
