using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    [SerializeField] private LevelManager.Scenes next_level;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            LevelManager.LoadScene(next_level);
        }
    }

}
