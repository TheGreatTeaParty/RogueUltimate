using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrap : MonoBehaviour
{
    public Sprite ActiveSprite;
    public Sprite SleepSprite;
    public float TrapDamage = 15f;

    private float SleepTime = 3;
    private float ActiveTime = 1f;
    private float _timeLeft;
    private bool active = false;

    private BoxCollider2D damageArea;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        damageArea = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _timeLeft = SleepTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            if (_timeLeft < 0)
            {
                TurnTrapOn();
            }
        }
        else
        {
            if (_timeLeft < 0)
            {
                TurnTrapOFF();
            }
        }
        _timeLeft -= Time.deltaTime;
    }

    private void TurnTrapOn()
    {
        _timeLeft = ActiveTime;
        active = true;
        damageArea.enabled = true;
        spriteRenderer.sprite = ActiveSprite;
    }

    private void TurnTrapOFF()
    {
        _timeLeft = SleepTime;
        active = false;
        damageArea.enabled = false;
        spriteRenderer.sprite = SleepSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamaged damaged = collision.GetComponent<IDamaged>();
        if(damaged!= null)
        {
            damaged.TakeDamage(TrapDamage, 0);
        }
    }
}