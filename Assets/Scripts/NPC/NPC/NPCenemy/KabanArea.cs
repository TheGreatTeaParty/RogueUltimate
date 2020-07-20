using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KabanArea : MonoBehaviour
{
    int PhysicalDamage;
    int MagicalDamage;

    private void Start()
    {
        PhysicalDamage = GetComponentInParent<EnemyStat>().physicalDamage.GetValue();
        MagicalDamage = GetComponentInParent<EnemyStat>().magicDamage.GetValue();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<IDamaged>().TakeDamage(PhysicalDamage, MagicalDamage);
        }
    }
}
