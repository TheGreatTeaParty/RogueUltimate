using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Traits/Woodcutter")]
public class Woodcutter : Trait
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
                    case AttackType.Melee:
                        {
                            MeleeWeapon mele = _new as MeleeWeapon;
                            if (mele.WeaponType == MeleeWeapon.MeleType.TwoHanded)
                                CharacterManager.Instance.Stats.PhysicalDamage.AddModifier(new StatModifier(0.5f, StatModifierType.PercentAdd, this));
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
                case AttackType.Melee:
                    {
                        MeleeWeapon mele = _new as MeleeWeapon;
                        if (mele.WeaponType == MeleeWeapon.MeleType.TwoHanded)
                            CharacterManager.Instance.Stats.PhysicalDamage.RemoveAllModifiersFromSource(this);
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
