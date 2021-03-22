using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitHolder
{
    public List<Trait> Traits;

    public TraitHolder()
    {
        Traits = new List<Trait>(3);
    }

    public void AddTrait(Trait trait)
    {
        Traits.Add(trait);
        trait.ApplyTrait();
    }

    public void RemoveTrait(Trait trait)
    {
        Traits.Remove(trait);
    }
}
