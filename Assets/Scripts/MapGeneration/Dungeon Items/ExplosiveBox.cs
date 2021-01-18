using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBox : MonoBehaviour, IDamaged
{
    public float Radius = 2.5f;
    private SpriteRenderer _sprite;
    private ParticleSystem _particle;
    [SerializeField] ParticleSystem _sparks;

    private LayerMask layerMask;

    private bool _isTriggered = false;
    private float _timeLeft = 2f;
    private float _blinkTime = 0;
    private bool _courutineStarted = false;
    //Material
    private MaterialPropertyBlock _collideMaterial;

    void Start()
    {
        layerMask = LayerMask.GetMask("Enemy", "Player");
        _sprite = GetComponent<SpriteRenderer>();
        _particle = GetComponent<ParticleSystem>();

        //Material:
        _collideMaterial = new MaterialPropertyBlock();
        _sprite.GetPropertyBlock(_collideMaterial);
    }

    private void Update()
    {
        if(_isTriggered)
        {
            _timeLeft -= Time.deltaTime;

            if(_blinkTime > 0.5)
            {
                StartCoroutine(Blink());
                _blinkTime = 0;
            }
            _blinkTime += Time.deltaTime;
        }
        if (_timeLeft < 0 && !_courutineStarted)
            StartCoroutine(WaitAndDestroy());
    }
    public void TakeDamage(float phyDamage, float magDamage)
    {
        if (_isTriggered && !_courutineStarted)
            StartCoroutine(WaitAndDestroy());
        else
        {
            _isTriggered = true;
            StartCoroutine(Blink());
        }
    }

    private IEnumerator WaitAndDestroy()
    {
        _courutineStarted = true;

        _sprite.sprite = null;
        _particle.Play();
        _sparks.Play();
        Explode();
        ScreenShakeController.Instance.StartShake(0.1f, 1f);
        yield return new WaitForSeconds(1.25f);
        Destroy(gameObject);
    }

    private IEnumerator Blink()
    {
        if (_sprite)
        {
            _collideMaterial.SetFloat("Damaged", 1f);
            _sprite.SetPropertyBlock(_collideMaterial);

            yield return new WaitForSeconds(0.2f);

            _collideMaterial.SetFloat("Damaged", 0f);
            _sprite.SetPropertyBlock(_collideMaterial);
        }
    }

    private void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Radius, layerMask);

        for(int i = 0; i < colliders.Length; i++)
        {
            IDamaged damaged = colliders[i].GetComponent<IDamaged>();
            if (damaged!= null)
            {
                damaged.TakeDamage(50f, 0f);
            }
        }
    }
}
