using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectFX : MonoBehaviour
{
    public GameObject target;
    public SpriteRenderer targetSprite;
    public Effect effect;

    private SpriteRenderer _effectSprite;

    private void Start()
    {
        _effectSprite = GetComponent<SpriteRenderer>();
        effect.OnDelete += DeleteEffect;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position;
        _effectSprite.sortingOrder = targetSprite.sortingOrder + 1;
    }

    void DeleteEffect()
    {
        Destroy(gameObject);
    }
}
