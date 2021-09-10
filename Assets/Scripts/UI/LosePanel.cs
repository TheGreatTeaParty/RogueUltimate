using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    public TextMeshProUGUI KillValue;
    public TextMeshProUGUI KillAmmount;
    public Button button;

    private Animator animator;
    private CharacterManager characterManager;
    private AudioManager audioManager;
    [SerializeField]
    private GameObject Hint;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.Instance;
        animator = GetComponent<Animator>();
        characterManager = CharacterManager.Instance;
        button.interactable = false;
        audioManager.Play("Lose");
    }

    private void DisplayKills()
    {
        animator.SetTrigger("KillAmmount");
        KillAmmount.text = characterManager.Stats.Kills.ToString();
        StartCoroutine(WaitAndCalculate());
    }

    private IEnumerator WaitAndCalculate()
    {
        yield return new WaitForSeconds(0.5f);
        audioManager.Play("Renown");
        animator.SetTrigger("KillCount");
        KillValue.text = (characterManager.Stats.Kills * 10).ToString();
        button.interactable = true;
    }
    public void DisplayHint()
    {
        Hint.SetActive(true);
    }
    public void Close()
    {
        SaveManager.DeletePlayer();
        Destroy(InterfaceManager.Instance.gameObject);
        Destroy(CharacterManager.Instance.Stats.gameObject);
        Destroy(PlayerCamera.Instance.gameObject);
        Destroy(TargetCircle.Instance.gameObject);
        LevelManager.Instance.LoadScene("StartTavern");
    }
    public void ClosePanel()
    {
        AccountManager accountManager = AccountManager.Instance;
        if (accountManager.KeeperLevel == 1 && accountManager.MasterLevel == 1 &&
            accountManager.SwithLevel == 1)
            DisplayHint();
        else
        {
            Close();
        }
    }
}
