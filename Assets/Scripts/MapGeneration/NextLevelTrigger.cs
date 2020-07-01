using System;
using System.Collections;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    [SerializeField] private LevelManager.Scenes nextLevel;
    [SerializeField] private Animator transition;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            LoadNextLevel();
    }

    
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevelAnimation());
    }

    
    private IEnumerator LoadLevelAnimation()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        
        LevelManager.LoadScene(nextLevel);
    }

}
