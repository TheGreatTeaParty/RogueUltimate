using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/NotAccapted")]
public class WasNotAccepted : Trait
{
    public override void ApplyTrait()
    {
        //Skip the generation and Restart! 
    }
}
