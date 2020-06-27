using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaban : AI
{
    public float rage_speed;
    public float timeBeforeTheRage;

    private Rigidbody2D Rb;
    private Vector2 position;
    private Vector3 finalPos;
    private bool _isRage;
    private float saveTime = 3f;

    [SerializeField] private GameObject kabanDamageArea;

    public override void Start()
    {
        base.Start();
        Rb = GetComponent<Rigidbody2D>();
    }

    public override void Update()
    {
        base.Update();

        if (_isRage)
            Rb.MovePosition(Rb.position + position* rage_speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, finalPos) < 0.5f || !_isRage)
        {
            NPCmovement.Enable();
            kabanDamageArea.GetComponent<CapsuleCollider2D>().enabled = false;
            _isRage = false;
        }
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case NPCstate.chasing:
                {
                    StateChasing();
                    break;
                }

            case NPCstate.hanging:
                {
                    StateHanging();
                    break;
                }

            case NPCstate.attacking:
                {
                    StateAttack();
                    break;
                }
        }
    }

    public override void Attack()
    {
        if (!_isRage)
        {
            NPCmovement.Disable();
            StartCoroutine(Waiter());
            finalPos = target.transform.position;
            position = finalPos - transform.position;
            kabanDamageArea.GetComponent<CapsuleCollider2D>().enabled = true;
            _isRage = true;
            StartCoroutine(Check());
        }
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(timeBeforeTheRage);
    }

    IEnumerator Check()
    {
        yield return new WaitForSeconds(saveTime);
        _isRage = false;
    }

}
