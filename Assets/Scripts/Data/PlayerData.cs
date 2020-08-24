using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public int currentHP, maxHP; //Health
    public int currentMP, maxMP; //Mana
    public int currentSP, maxSP; //Stamina
    public float[] position;
    public string scene;
    public string gameObjectName;
    

    public PlayerData()
    {
        currentHP = PlayerStat.Instance.GetCurrentHealth();
        currentMP = PlayerStat.Instance.GetCurrentMana();
        currentSP = PlayerStat.Instance.GetCurrentStamina();
        maxHP = PlayerStat.Instance.GetCurrentHealth();
        maxMP = PlayerStat.Instance.maxMana;
        maxSP = PlayerStat.Instance.maxStamina;

        scene = SceneManager.GetActiveScene().name;
        gameObjectName = PlayerStat.Instance.gameObject.name;

        position = new float[3];
        position[0] = PlayerStat.Instance.transform.position.x;
        position[1] = PlayerStat.Instance.transform.position.y;
        position[2] = PlayerStat.Instance.transform.position.z;
    }

}
