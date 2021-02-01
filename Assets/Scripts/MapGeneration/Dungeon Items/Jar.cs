using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar : MonoBehaviour, IDamaged
{
    private SpriteRenderer _sprite;
    private ParticleSystem _particle;
    private AudioSource _audioSource;

    //Material
    private MaterialPropertyBlock _collideMaterial;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _particle = GetComponent<ParticleSystem>();
        _audioSource = GetComponent<AudioSource>();

        //Material:
        _collideMaterial = new MaterialPropertyBlock();
        _sprite.GetPropertyBlock(_collideMaterial);
    }

    public bool TakeDamage(float phyDamage, float magDamage)
    {
        _collideMaterial.SetFloat("Damaged", 1f);
        _sprite.SetPropertyBlock(_collideMaterial);
        StartCoroutine(WaitAndDestroy());
        return true;
    }

    private IEnumerator WaitAndDestroy()
    {
        _sprite.sprite = null;
        _particle.Play();
        _audioSource.Play();
        yield return new WaitForSeconds(0.31f);
        Destroy(gameObject);
    }
}
