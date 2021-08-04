using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAudioHandle : MonoBehaviour
{
    PlayerAttack playerAttack;
    AudioSource audioSource;

    private AudioClip _startAudio;
    private AudioClip _endAudio;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerAttack = PlayerOnScene.Instance.GetComponent<PlayerAttack>();
        playerAttack.onAttacked += OnAttacked;
        playerAttack.EndAttack += EndAttack;
        CharacterManager.Instance.onEquipmentChanged += OnWeaponChanged;
    }

    private void OnWeaponChanged(EquipmentItem _new, EquipmentItem _old)
    {
        if (_new && _new.EquipmentType == EquipmentType.Weapon)
        {
            switch (_new.Echo())
            {
                case AttackType.Melee:
                    {
                        MeleeWeapon weapon = _new as MeleeWeapon;
                        _startAudio = weapon.StartAttackSound;
                        _endAudio = weapon.EndAttackSound;
                        break;
                    }
                case AttackType.Range:
                    {
                        RangeWeapon weapon = _new as RangeWeapon;
                        _startAudio = weapon.prepareSound;
                        _endAudio = weapon.ReleaseSound;
                        break;
                    }
                case AttackType.Magic:
                    {
                        MagicWeapon weapon = _new as MagicWeapon;
                        _startAudio = weapon.prepareSound;
                        _endAudio = weapon.ReleaseSound;
                        break;
                    }
            }
        }
        else if(!_new && _old && _old.EquipmentType == EquipmentType.Weapon)
        {
            _startAudio = null;
            _endAudio = null;
        }
    }
    private void OnAttacked(AttackType attackType)
    {
        if (_startAudio)
        {
            audioSource.PlayOneShot(_startAudio);
        }
    }

    private void EndAttack(AttackType attackType)
    {
        if (_endAudio)
        {
            audioSource.PlayOneShot(_endAudio);
        }
    }
}
