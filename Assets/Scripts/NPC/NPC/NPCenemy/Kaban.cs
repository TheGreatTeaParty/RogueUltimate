using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaban : AI
{
    public float rage_speed;
    public float timeBeforeTheRage;

    private Vector2 position;
    private Vector3 finalPos;
    private bool _isRage;
    private float saveTime = 3f;

    private bool _preparing = false;

    [SerializeField] private GameObject kabanDamageArea;

    
    public override void Update()
    {
        base.Update();

        if (_isRage)
            Rb.MovePosition(Rb.position + position* rage_speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, finalPos) < 0.5f || !_isRage)
        {
            StartMoving();
            kabanDamageArea.GetComponent<CapsuleCollider2D>().enabled = false;
            _isRage = false;
        }
    }

    public override void Attack()
    {
        if (!_isRage)
        {
            StopMoving();
            if(!_preparing)
                StartCoroutine(Waiter());
        }
    }
    

    IEnumerator Waiter()
    {
        _preparing = true;
        yield return new WaitForSeconds(timeBeforeTheRage);
        finalPos = target.transform.position;
        position = finalPos - transform.position;
        kabanDamageArea.GetComponent<CapsuleCollider2D>().enabled = true;
        _isRage = true;
        StartCoroutine(Check());
        _preparing = false;
    }

    IEnumerator Check()
    {
        yield return new WaitForSeconds(saveTime);
        _isRage = false;
    }

}
