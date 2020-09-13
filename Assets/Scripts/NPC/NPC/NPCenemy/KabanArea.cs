using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KabanArea : MonoBehaviour
{
    int PhysicalDamage;
    int MagicalDamage;
    Kaban kaban;
    
    // Cache
    private Animator _parentAnimator;

    
    private void Start()
    {
        kaban = GetComponentInParent<Kaban>();
        PhysicalDamage = GetComponentInParent<EnemyStat>().physicalDamage.GetValue();
        MagicalDamage = GetComponentInParent<EnemyStat>().magicDamage.GetValue();
        _parentAnimator = GetComponentInParent<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<IDamaged>().TakeDamage(PhysicalDamage, MagicalDamage);
            _parentAnimator.SetTrigger("Crash");
            _parentAnimator.SetBool("HitPlayer",true);
            kaban.SetHit(true);
        }

        else if(collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("Door"))
        {
            _parentAnimator.SetTrigger("Crash");
            _parentAnimator.SetBool("HitPlayer", false);
            kaban.SetHit(false);
        }
    }
}
