using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : AI
{
    public Transform arrowPrefab;

    private EnemyStat archerStat;
    private Vector3 ShootDir;
    public override void Start()
    {
        base.Start();
        archerStat = GetComponent<EnemyStat>();
    }

    public override void Update()
    {
        base.Update();
        ShootDir = Vector3.Normalize(target.transform.position - transform.position);
    }

   
    public override void Attack()
    {
        Transform arrow = Instantiate(arrowPrefab, transform.position + ShootDir, Quaternion.identity);

        arrow.GetComponent<FlyingObject>().SetData(archerStat.physicalDamage.GetValue(),archerStat.magicDamage.GetValue(),ShootDir);
    }
    
    public override void Die()
    {
        base.Die();
        PlayerStat.Instance.GainXP(100);
    }
    
}
