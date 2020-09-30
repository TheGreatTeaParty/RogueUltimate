using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KabanArea : MonoBehaviour
{
    private float _physicalDamage;
    private float _magicDamage;
    private Kaban _kaban;
    
    // Cache
    private Animator _parentAnimator;

    
    private void Start()
    {
        _kaban = GetComponentInParent<Kaban>();
        _physicalDamage = GetComponentInParent<EnemyStat>().physicalDamage.Value;
        _magicDamage = GetComponentInParent<EnemyStat>().magicDamage.Value;
        _parentAnimator = GetComponentInParent<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<IDamaged>().TakeDamage(_physicalDamage, _magicDamage);
            _parentAnimator.SetTrigger("Crash");
            _parentAnimator.SetBool("HitPlayer",true);
            _kaban.SetHit(true);
        }

        else if(collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("Door"))
        {
            _parentAnimator.SetTrigger("Crash");
            _parentAnimator.SetBool("HitPlayer", false);
            _kaban.SetHit(false);
        }
    }
}
