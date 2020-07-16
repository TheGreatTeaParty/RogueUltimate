using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int HP;
    public int MP;
    public int SP;
    public float[] position;

    public PlayerData(PlayerStat player_stat)
    {
        HP = PlayerStat.Instance.currentHealth;
        MP = PlayerStat.Instance.currentMana;
        SP = PlayerStat.Instance.currentStamina;

        position = new float[3];
        position[0] = PlayerStat.Instance.transform.position.x;
        position[1] = PlayerStat.Instance.transform.position.y;
        position[2] = PlayerStat.Instance.transform.position.z;
    }

}
