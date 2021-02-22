using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Turret : MonoBehaviour
{
    [SerializeField] private float cooldown;
    private float _cooldown;

    [SerializeField] private GameObject projectile;
    [SerializeField]private Animator animator;
    private static readonly int IsCharging = Animator.StringToHash("IsCharging");

    private void Awake()
    {
        _cooldown = cooldown;
    }

    private void Update()
    {
        if (_cooldown >= 0)
        {
            _cooldown -= Time.deltaTime;
            animator.SetBool(IsCharging, false);
        }
        else
        {
            animator.SetBool(IsCharging, true);
            Fire();
            _cooldown = cooldown;
        }
    }

    private void Fire()
    {
        Instantiate(projectile, transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z - 90f));
    }
}
