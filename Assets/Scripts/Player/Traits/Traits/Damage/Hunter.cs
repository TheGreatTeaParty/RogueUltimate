using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/Hunter")]
public class Hunter : Trait
{
    public override void ApplyTrait()
    {
        CharacterManager.Instance.onEquipmentChanged += ApplyEffect;
    }

    private void ApplyEffect(EquipmentItem _new, EquipmentItem _old)
    {
        //If we have a new weapon:
        if (_new)
        {
            if (_new.EquipmentType == EquipmentType.Weapon)
            {
                switch (_new.Echo())
                {
                    case AttackType.Range:
                        {
                            RangeWeapon range = _new as RangeWeapon;
                            if (range.WeaponType == RangeWeapon.RangeType.bow)  //ApplyEffect
                            {
                                CharacterManager.Instance.Stats.PhysicalDamage.AddModifier(new StatModifier(0.5f, StatModifierType.PercentAdd, this));
                            }
                            break;
                        }

                    default:
                        break;
                }
            }
        }
        //If we drop the weapon, clear the animation controller
        else if (_old.EquipmentType == EquipmentType.Weapon && _new == null)
        {
            switch (_old.Echo())
            {
                case AttackType.Range:
                    {
                        RangeWeapon range = _new as RangeWeapon;
                        if (range.WeaponType == RangeWeapon.RangeType.bow)  //ApplyEffect
                        {
                            CharacterManager.Instance.Stats.PhysicalDamage.RemoveAllModifiersFromSource(this);
                        }
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
