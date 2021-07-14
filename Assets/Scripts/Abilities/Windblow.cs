using UnityEngine;


[CreateAssetMenu(menuName = "Abilities/Windblow")] 
public class Windblow : ActiveAbility
{
    public float attackRange;
    
    
    public override void Activate()
    {
        base.Activate();

        var player = PlayerOnScene.Instance;
        var position = player.gameObject.transform.position;
        
        if (player.playerAttack.CurrentAttackCD <= 0)
        {
            _enemyMask = LayerMask.GetMask("Enemy");
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(position, attackRange, _enemyMask);

            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                var vector = enemiesToDamage[i].gameObject.transform.position - player.transform.position;
                enemiesToDamage[i].GetComponent<EnemyStat>().
                    TakeDamage(1f, 1f, vector.normalized, 100000);
                Debug.Log(vector.normalized.ToString());
            }

            //Send mesage to isAttack animation handler that we use Melee Weapon
            player.playerAttack.onAttacked?.Invoke(AttackType.Melee);
        }
    }
    
}