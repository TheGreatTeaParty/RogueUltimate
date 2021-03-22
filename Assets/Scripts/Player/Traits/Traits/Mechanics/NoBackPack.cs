﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(menuName = "Traits/NoBackPack")]
public class NoBackPack : Trait
{
    public override void ApplyTrait()
    {
        //Change Invemtory size here:
    }
    public override void DeleteTrait()
    {
        CharacterManager.Instance.Stats.Strength.MaxHealth.RemoveAllModifiersFromSource(this);
    }
}