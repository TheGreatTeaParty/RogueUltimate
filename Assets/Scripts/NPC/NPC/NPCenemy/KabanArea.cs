using UnityEngine;
using System.Collections;

public class KabanArea : MonoBehaviour
{
    private float _physicalDamage;
    private float _magicDamage;
    private bool IgnoreWalls = false;
    private Kaban _kaban;
    
    // Cache
    private Animator _parentAnimator;

    
    private void Start()
    {
        _kaban = GetComponentInParent<Kaban>();
        _physicalDamage = GetComponentInParent<EnemyStat>().PhysicalDamage.Value;
        _magicDamage = GetComponentInParent<EnemyStat>().MagicDamage.Value;
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

        else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if (!IgnoreWalls)
            {
                _parentAnimator.SetTrigger("Crash");
                _parentAnimator.SetBool("HitPlayer", false);
                _kaban.SetHit(false);
            }
        }

        else if(collision.gameObject.layer == LayerMask.NameToLayer("EnvObjects"))
        {
            collision.GetComponent<IDamaged>().TakeDamage(_physicalDamage, _magicDamage);
            return;
        }
    }
    public void IgnoreWallForASecond()
    {
        StartCoroutine("Wait");
    }

    public IEnumerator Wait()
    {
        IgnoreWalls = true;
        // Start function WaitAndPrint as a coroutine
        yield return new WaitForSeconds(1f);
        IgnoreWalls = false;
    }

}
