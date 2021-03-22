using UnityEngine;

public class Archer : EnemyAI
{
    private Vector3 _shootDir;
    public Transform arrowPrefab;


    protected override void Update()
    {
        base.Update();
        _shootDir = Vector3.Normalize(target.transform.position - transform.position);
    }
    
    protected override void Attack()
    {
        Transform arrow = Instantiate(arrowPrefab, transform.position + _shootDir, Quaternion.identity);

        arrow.GetComponent<FlyingObject>().SetData(stats.PhysicalDamage.Value, stats.MagicDamage.Value,_shootDir,false);
        isAttack = false;
    }

}
