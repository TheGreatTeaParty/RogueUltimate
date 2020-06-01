using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonAttack : MonoBehaviour
{

    public void attackPlayer()
    {
        //Call player damage function
        KeepOnScene.instance.GetComponent<PlayerAttack>().Attack();
    }
    

}
