using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimDisplay : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TurnOnIcon()
    {
        spriteRenderer.enabled = true;
    }

    public void TurnOFFIcon()
    {
        spriteRenderer.enabled = false;
    }

    public bool GetIconState()
    {
        return spriteRenderer.enabled;
    }
}
