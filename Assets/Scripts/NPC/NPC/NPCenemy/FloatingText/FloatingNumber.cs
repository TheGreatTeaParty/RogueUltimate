﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingNumber : MonoBehaviour
{
    [SerializeField] private Transform textPb;
    private int _damage;


    void Start()
    {
        GetComponent<EnemyStat>().onReceivedDamage += ReceiveDamage;
    }


    private void ReceiveDamage(int damage)
    {
        _damage = damage;
        textPb.GetComponent<TextMeshPro>().text = damage.ToString();
        textPb.GetComponent<TextMeshPro>().color = new Color32(255, 168, 0, 255);
        Instantiate(textPb, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);

    }
    
    
    public int GetDamage()
    {
        return _damage;
    }
    
    
}