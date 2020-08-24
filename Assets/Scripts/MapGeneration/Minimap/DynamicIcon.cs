using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicIcon : MonoBehaviour
{
    [SerializeField] bool TurnOff = true;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        if (TurnOff)
        {
            sprite.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!sprite.enabled)
                sprite.enabled = true;
            sprite.color = new Color(0.6f, 0.6f, 0.6f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sprite.color = new Color(0.3f, 0.3f, 0.3f);
        }
    }
}
