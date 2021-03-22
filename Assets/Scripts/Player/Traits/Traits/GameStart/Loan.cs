using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/TakeALoan")]
public class Loan : Trait
{
    public override void ApplyTrait()
    {
        CharacterManager.Instance.Inventory.Gold += 500;
    }
}
