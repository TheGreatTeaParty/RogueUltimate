using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPosition : MonoBehaviour
{
    private bool isAvailable = true;
    private EnemyAI curentEnemy = null;
    
    public void CheckPosition()
    {
        if (curentEnemy == null)
            isAvailable = true;
    }

    public bool IsAvailable()
    {
        return isAvailable;
    }
    public GameObject SetPosition(EnemyAI enemy)
    {
        if (isAvailable)
        {
            curentEnemy = enemy;
            isAvailable = false;
            return gameObject;
        }
        else
            return null;
    }
}
