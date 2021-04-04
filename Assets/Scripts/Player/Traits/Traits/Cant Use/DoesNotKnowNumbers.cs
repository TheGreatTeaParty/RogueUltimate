using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/DoesNotKnowNumbers")]
public class DoesNotKnowNumbers : Trait,ITraitReqired
{
    public bool ISAppropriate(EquipmentItem equipmentItem)
    {
        if (equipmentItem)
        {
            if (equipmentItem.Echo() == AttackType.Magic)
            {
                return false;
            }
            return true;
        }
        return true;
    }

    public override void ApplyTrait()
    {
        CharacterManager.Instance.Stats.EquipmentTraitReq = this;
    }
}
