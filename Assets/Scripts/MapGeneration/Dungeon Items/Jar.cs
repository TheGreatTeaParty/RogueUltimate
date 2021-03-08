using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar : MonoBehaviour, IDamaged
{
    [SerializeField] Transform coin;
    private SpriteRenderer _sprite;
    private ParticleSystem _particle;
    private AudioSource _audioSource;
    private int _gainedGold;
    private int _goldSpawnChance;

    //Material
    private MaterialPropertyBlock _collideMaterial;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _particle = GetComponent<ParticleSystem>();
        _audioSource = GetComponent<AudioSource>();
        _gainedGold = Random.Range(1, 11);
        _goldSpawnChance = Random.Range(0, 100);

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
        //Creates Gold
        if (_goldSpawnChance < 5)
                CreateGold();
        yield return new WaitForSeconds(0.31f);
        Destroy(gameObject);
    }

    public bool TakeDamage(float phyDamage, float magDamage, bool crit)
    {
        return TakeDamage(phyDamage, magDamage);
    }

    private void CreateGold()
    {
        Transform gold = Instantiate(coin, transform.position, Quaternion.identity);
        gold.GetComponent<Gold>().GoldAmount = _gainedGold;
    }

}
