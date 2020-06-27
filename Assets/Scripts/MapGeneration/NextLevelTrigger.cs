using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    [SerializeField] private LevelManager.Scenes next_level;
    [SerializeField] private Animator transition;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            LoadNextLevel();
        
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevelAnimation());
    }

    IEnumerator LoadLevelAnimation()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);
        
        LevelManager.LoadScene(next_level);

    }

}
