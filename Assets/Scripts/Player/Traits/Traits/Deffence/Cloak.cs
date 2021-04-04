using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/Defence/Cloak")]

public class Cloak : Trait
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
            if (_new.EquipmentType == EquipmentType.Armor)
            {
                switch (_new.EqipmnetArmorType)
                {
                    case EquipmentItem.ArmorType.Magical:
                        {
                            CharacterManager.Instance.Stats.MagicProtection.AddModifier(new StatModifier(0.1f, StatModifierType.PercentAdd, this));
                            break;
                        }

                    default:
                        break;
                }
            }
        }
        //If we drop the Armor, clear the Percentage mofifier
        else if (_old.EquipmentType == EquipmentType.Armor && _new == null)
        {
            switch (_old.EqipmnetArmorType)
            {
                case EquipmentItem.ArmorType.Magical:
                    {
                        CharacterManager.Instance.Stats.MagicProtection.RemoveAllModifiersFromSource(this);
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
