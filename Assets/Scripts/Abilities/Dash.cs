using UnityEngine;
using System.Collections;


[CreateAssetMenu(menuName = "Abilities/Dash")] 
public class Dash : ActiveAbility
{
    public float bounce;
    
    
    public override void Activate()
    {
        base.Activate();
            
        var player = PlayerOnScene.Instance;
        var trail = player.playerMovement.trailRenderer;

        if (player.playerAttack.CurrentAttackCD <= 0)
        {
            player.rb.AddForce(player.playerMovement.GetDirection() * bounce);
            Instantiate(trail, player.GetComponent<Transform>());

            _enemyMask = LayerMask.GetMask("Enemy");
            Collider2D[] enemiesToDamage = Physics2D.
                OverlapCircleAll(player.gameObject.transform.position, 2, _enemyMask);

            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                var vector = enemiesToDamage[i].gameObject.transform.position - player.transform.position;
                enemiesToDamage[i].GetComponent<EnemyStat>().
                    TakeDamage(3, 0f, vector.normalized, 100000);
            }
        }
/*
        collider.enabled = false;
        trail.gameObject.SetActive(false);*/
            
    }
        
    
}