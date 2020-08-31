using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KabanArea : MonoBehaviour
{
    int PhysicalDamage;
    int MagicalDamage;
    Kaban kaban;

    private void Start()
    {
        kaban = GetComponentInParent<Kaban>();
        PhysicalDamage = GetComponentInParent<EnemyStat>().physicalDamage.GetValue();
        MagicalDamage = GetComponentInParent<EnemyStat>().magicDamage.GetValue();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<IDamaged>().TakeDamage(PhysicalDamage, MagicalDamage);
            GetComponentInParent<Animator>().SetTrigger("Crash");
            GetComponentInParent<Animator>().SetBool("HitPlayer",true);
            kaban.SetHit(true);
        }

        else if(collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("Door"))
        {
            GetComponentInParent<Animator>().SetTrigger("Crash");
            GetComponentInParent<Animator>().SetBool("HitPlayer", false);
            kaban.SetHit(false);
        }
    }
}
