using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public int HP;
    public int MP;
    public int SP;
    public float[] position;
    public string scene;
    public string gameObjectName;
    

    public PlayerData()
    {
        HP = PlayerStat.Instance.currentHealth;
        MP = PlayerStat.Instance.currentMana;
        SP = PlayerStat.Instance.currentStamina;

        scene = SceneManager.GetActiveScene().name;
        gameObjectName = PlayerStat.Instance.gameObject.name;

        position = new float[3];
        position[0] = PlayerStat.Instance.transform.position.x;
        position[1] = PlayerStat.Instance.transform.position.y;
        position[2] = PlayerStat.Instance.transform.position.z;
    }

}
