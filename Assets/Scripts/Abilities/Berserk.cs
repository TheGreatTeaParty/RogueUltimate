using System.Collections;
using UnityEngine;


[CreateAssetMenu(menuName = "Abilities/Berserk")] 
public class Berserk : ActiveAbility
{
    public float time; 
    
    
    public override void Activate()
    {
        
    }

    private IEnumerator BerserkMode(float time)
    {
        
        yield return new WaitForSeconds(time);
        
        
    }
    
}