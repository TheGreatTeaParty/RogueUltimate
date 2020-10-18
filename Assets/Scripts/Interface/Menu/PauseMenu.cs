using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Animator animator;
    

    public void PauseButton()
    {
        Time.timeScale = 0f;
        animator.SetFloat("Kind of animation", 0f);
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
        Time.timeScale = 1f;
        Debug.Log("Quitting game...");

        Destroy(PlayerOnScene.Instance.gameObject);
        // null to CharacterManager.Instance.Stats ?
        Destroy(InterfaceManager.Instance.gameObject);
        Destroy(AudioManager.Instance.gameObject);
        Destroy(PlayerCamera.Instance.gameObject);

        SceneManager.LoadScene("Menu");
    }

}

