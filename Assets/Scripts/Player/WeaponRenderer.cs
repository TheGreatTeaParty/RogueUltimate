﻿using UnityEngine;

public class WeaponRenderer : MonoBehaviour
{
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _weaponSprite;
    private Animator _weaponAnimator;
    private Sprite[] WeaponAttackAnim;
    private Sprite[] WaeaponIdleAnim;
    private int _prevIndex;
    private PlayerStat _playerStat;
    private bool _isAttack = false;

    public int PrevIndex => _prevIndex;
    private void Start()
    {
        CharacterManager.Instance.onEquipmentChanged += OnWeaponChanged;
        _weaponAnimator = GetComponent<Animator>();
        _playerSprite = PlayerOnScene.Instance.GetComponent<SpriteRenderer>();
        _weaponSprite = GetComponent<SpriteRenderer>();

        _playerStat = PlayerOnScene.Instance.GetComponent<PlayerStat>();
        PlayerOnScene.Instance.playerAttack.onAttacked += OnAttacked;
        PlayerOnScene.Instance.playerAttack.EndAttack += EndAttack;
        _weaponSprite.sortingOrder = _playerSprite.sortingOrder + 2;

    }
    
    private void OnWeaponChanged(EquipmentItem _new, EquipmentItem _old)
    {
        if (_new && _new.EquipmentType == EquipmentType.Weapon)
        {
            if (_new.Animation.Length != 0)
            {
                WaeaponIdleAnim = _new.Animation;
            }
            else
            {
                WaeaponIdleAnim = null;
            }
        }
    }

    private void LateUpdate()
    {
        if (!_isAttack && WaeaponIdleAnim != null)
        {
            string index = "";
            if (_weaponSprite.sprite.name[_weaponSprite.sprite.name.Length - 2] != '_')
            {
                if (_weaponSprite.sprite.name[_weaponSprite.sprite.name.Length - 3] != '_')
                    index += _weaponSprite.sprite.name[_weaponSprite.sprite.name.Length - 3];
                index += _weaponSprite.sprite.name[_weaponSprite.sprite.name.Length - 2];
            }
            index += _weaponSprite.sprite.name[_weaponSprite.sprite.name.Length - 1];

            if (int.TryParse(index, out int j))
            {
                if (j >= WaeaponIdleAnim.Length)
                {
                    Debug.Log($"Sprite with Index: {j} does not exist in Animation Sprites!");
                   // _weaponSprite.sprite = null;
                }
                else
                {   //Need to be added;
                    _weaponSprite.sprite = WaeaponIdleAnim[j];

                }
            }

        }
    }

    private void OnAttacked(AttackType attackType)
    {
        if(attackType == AttackType.None)
        {
            return;
        }

        else if(attackType == AttackType.Melee)
        {
            _weaponAnimator.SetInteger("Set", GenerateAttackSet());
        }
        ChangeAnimationSpeed(attackType);

        _weaponAnimator.SetBool("Attack",true);
        _isAttack = true;
    }

    private void EndAttack(AttackType attackType)
    {
        if (attackType != AttackType.None)
        {
            _weaponAnimator.SetBool("Attack", false);
            _isAttack = false;
        }
    }

    private int GenerateAttackSet()
    {
        if (_prevIndex == 0)
        {
            _prevIndex = 1;
            return _prevIndex;
        }
        else
        {
            _prevIndex = 0;
            return _prevIndex;
        }
    }

    private void ChangeAnimationSpeed(AttackType attackType)
    {
        if (attackType == AttackType.Magic)
            _weaponAnimator.speed = (0.8f / _playerStat.CastSpeed.Value);       //0.5 base attack animation duration in seconds
        else
        {
            _weaponAnimator.speed = (0.8f / _playerStat.AttackSpeed.Value);
        }
    }
}
