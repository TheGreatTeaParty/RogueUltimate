using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar : MonoBehaviour, IDamaged
{
    private SpriteRenderer _sprite;
    private ParticleSystem _particle;

    //Material
    private MaterialPropertyBlock _collideMaterial;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _particle = GetComponent<ParticleSystem>();

        //Material:
        _collideMaterial = new MaterialPropertyBlock();
        _sprite.GetPropertyBlock(_collideMaterial);
    }

    public void TakeDamage(float phyDamage, float magDamage)
    {
        _collideMaterial.SetFloat("Damaged", 1f);
        _sprite.SetPropertyBlock(_collideMaterial);
        StartCoroutine(WaitAndDestroy());
    }

    private IEnumerator WaitAndDestroy()
    {
        _sprite.sprite = null;
        _particle.Play();
        yield return new WaitForSeconds(0.31f);
        Destroy(gameObject);
    }
}
