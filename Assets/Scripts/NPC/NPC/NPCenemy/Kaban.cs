using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaban : AI
{
    public float rage_speed;
    public float StunTime = 2.5f;

    private Vector2 position;
    private Vector3 finalPos;
    private bool _isRage;

    private bool _preparing = false;
    private bool hit = false;
    private bool hit_player = false;

    [SerializeField] private GameObject kabanDamageArea;

    
    public override void Update()
    {
        base.Update();
       
        if (_isRage)
            Rb.MovePosition(Rb.position + position* rage_speed * Time.deltaTime);

        if (hit)
        {
            kabanDamageArea.GetComponent<CapsuleCollider2D>().enabled = false;
            _isRage = false;
            hit = false;
            if (!hit_player)
            {
                StartCoroutine(Stun());
            }

            else
            {
                _attack = false;
            }
        }
    }

    public override void Attack()
    {
       
        if (!_isRage)
        {
            if (!_preparing)
            {
                _preparing = true;
                finalPos = target.transform.position;
                position = (finalPos - transform.position).normalized;
                kabanDamageArea.GetComponent<CapsuleCollider2D>().enabled = true;
                _isRage = true;
                _preparing = false;
            }

        }
    }
    public override Vector2 GetDirection()
    {
        if (_isRage)
        {
            return position;
        }
        else
        {
            return dir;
        }
    }

    public void SetHit(bool _hit_player)
    {
        hit = true;
        hit_player = _hit_player;
    }

    IEnumerator Stun()
    {
        GetComponent<Animator>().SetBool("Stunned", true);
        yield return new WaitForSeconds(StunTime);
        _attack = false;
        GetComponent<Animator>().SetBool("Stunned", false);
    }
}
