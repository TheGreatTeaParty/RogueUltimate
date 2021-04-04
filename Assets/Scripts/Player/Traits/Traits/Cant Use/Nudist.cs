using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/Nudist")]
public class Nudist : Trait,ITraitReqired
{
    public bool ISAppropriate(EquipmentItem equipmentItem)
    {
        if (equipmentItem)
        {
            if (equipmentItem.Echo() == AttackType.None)
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
