using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaban : AI
{
    public float rage_speed;

    private Vector2 finalPos;
    [SerializeField] private GameObject kabanDamageArea;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        if (Vector2.Distance(transform.position, finalPos) < 0.2f)
        {
            kabanDamageArea.GetComponent<CapsuleCollider2D>().enabled = false;
            NPCmovement.SetSpeed(speed);
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
        NPCmovement.StopMoving();
        finalPos = target.transform.position;
        NPCmovement.SetSpeed(rage_speed);
        NPCmovement.MoveTo(finalPos);
        kabanDamageArea.GetComponent<CapsuleCollider2D>().enabled = true;
    }

}
