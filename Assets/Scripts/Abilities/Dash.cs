using UnityEngine;


[CreateAssetMenu(menuName = "Abilities/Dash")] 
public class Dash : ActiveAbility
{
    public float bounce;
    
    
    public override void Activate()
    {
        var player = PlayerOnScene.Instance;

        if (player.playerAttack.CurrentAttackCD <= 0)
            player.rb.AddForce(Vector3.right * bounce);
    }
    
}