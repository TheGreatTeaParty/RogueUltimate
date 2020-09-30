using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonAttack : MonoBehaviour
{
    // Cache
    private PlayerAttack _playerAttack;


    private void Start()
    {    
        // Cache
        _playerAttack = PlayerOnScene.Instance.playerAttack;
    }

    public void AttackPlayer()
    {
        //Call player damage function
        _playerAttack.Attack();
    }
    
}
