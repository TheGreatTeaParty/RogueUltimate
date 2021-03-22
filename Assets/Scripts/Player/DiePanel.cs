using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DiePanel : MonoBehaviour
{
    [SerializeField]
    private Animator dieAnimator;
    [SerializeField]
    private GameObject background;
    [SerializeField]
    private GameObject image;


    public void PlayerStartDie()
    {
        var UI = InterfaceManager.Instance;
        UI.HideAll();
        background.SetActive(true);
        image.SetActive(true);

        dieAnimator.SetTrigger("Die");

    }

    private void PlayerEndDie()
    {
        SaveManager.DeletePlayer();
        Destroy(InterfaceManager.Instance.gameObject);
        Destroy(AudioManager.Instance.gameObject);
        Destroy(CharacterManager.Instance.Stats.gameObject);
        Destroy(PlayerCamera.Instance.gameObject);
        SceneManager.LoadScene("Menu");
    }

}
