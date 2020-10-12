﻿using System.Collections;
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


    protected override void Attack()
    {
        Transform arrow = Instantiate(arrowPrefab, transform.position + ShootDir, Quaternion.identity);

        arrow.GetComponent<FlyingObject>().SetData(archerStat.physicalDamage.Value, archerStat.magicDamage.Value,ShootDir);
        _attack = false;

    }

}
