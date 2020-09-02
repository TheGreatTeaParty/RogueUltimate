using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DiePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject diePanel;
    [SerializeField]
    private Animator pauseAnimation;


    public void PlayerDie()
    {
        var UI = InterfaceOnScene.Instance;
        
        UI.HideAll();
        diePanel.SetActive(true);
        StartCoroutine(Waiter());
        Time.timeScale = 0f;
    }

    public void DieButton()
    {
        var UI = InterfaceOnScene.Instance;
        
        SaveSystem.DeletePlayer();
        UI.HideAll();
        diePanel.SetActive(false);
        LevelManager.LoadScene("Menu");
        Time.timeScale = 1f;

        Destroy(InterfaceOnScene.Instance.gameObject);
        Destroy(AudioManager.Instance.gameObject);
        Destroy(PlayerStat.Instance.gameObject);
    }

    public void ResurrectButton()
    {
        var UI = InterfaceOnScene.Instance;
        
        PlayerStat.Instance.ModifyHealth(PlayerStat.Instance.maxHealth);
        PlayerStat.Instance.ModifyMana(PlayerStat.Instance.maxMana);
        PlayerStat.Instance.ModifyStamina(PlayerStat.Instance.maxStamina);
        PlayerStat.Instance.gameObject.layer = 12;
        PlayerStat.Instance.gameObject.tag = "Player";
        diePanel.SetActive(false);
        UI.ShowMainElements();
        Time.timeScale = 1f;
        InterfaceOnScene.Instance.ShowMainElements();
        Debug.Log("There will be an adv : ");
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(1f);
    }

}
