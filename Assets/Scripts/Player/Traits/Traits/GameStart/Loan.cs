using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/TakeALoan")]
public class Loan : Trait
{
    public override void ApplyTrait()
    {
        var character = CharacterManager.Instance;
        character.Inventory.Gold += 100;
        character.Inventory.UpdateGold();
    }
}
