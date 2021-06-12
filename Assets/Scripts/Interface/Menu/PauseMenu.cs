using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Animator animator;
    

    public void PauseButton()
    {
        StartCoroutine(WaitAndPause());
    }

    public void ResumeButton()
    {
        Time.timeScale = 1f;
        animator.SetFloat("Kind of animation", 1f);
    }

    public void SettingsButton()
    {
        Debug.Log("There will be settings");

    }

    public void ExitButton()
    {
        SaveManager.SavePlayer();
        SaveManager.SaveAccount();
        Time.timeScale = 1f;

        Destroy(PlayerOnScene.Instance.gameObject);
        // null to CharacterManager.Instance.Stats ?
        Destroy(InterfaceManager.Instance.gameObject);
        Destroy(AudioManager.Instance.gameObject);
        Destroy(PlayerCamera.Instance.gameObject);

        SceneManager.LoadScene("Menu");
    }

    IEnumerator WaitAndPause()
    {
        animator.SetFloat("Kind of animation", 0f);
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 0f;
    }
}

