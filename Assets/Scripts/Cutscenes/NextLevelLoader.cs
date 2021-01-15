using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextLevelLoader : MonoBehaviour
{
    [SerializeField]
    private Animator transition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Doll"))
        {
            LoadLevel();
        }
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadAnim());
        SceneManager.LoadScene("StartTavern");
    }

    private IEnumerator LoadAnim()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
    }

}
