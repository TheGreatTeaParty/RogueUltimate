using System;
using System.Collections;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    [SerializeField] private LevelManager.Scenes nextLevel;
    [SerializeField] private Animator transition;
    [SerializeField] private Type type;

    public enum Type
    {
        portal = 0,
        stairs,
    }

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

        if(type == Type.portal)
        {
            AudioManager.Instance.Play("Teleport");
        }
        else
        {

        }

        yield return new WaitForSeconds(1f);
        
        LevelManager.LoadScene(nextLevel);
    }

    public void SetNextLevel(LevelManager.Scenes next)
    {
        nextLevel = next;
    }
}
