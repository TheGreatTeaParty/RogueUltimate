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
        diePanel.SetActive(true);
        StartCoroutine(Waiter());
        Time.timeScale = 0f;
    }

    public void DieButton()
    {
        Destroy(PlayerStat.Instance.gameObject);
        SaveSystem.DeletePlayer();
        diePanel.SetActive(false);
        LevelManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }

    public void ResurrectButton()
    {
        PlayerStat.Instance.ModifyHealth(PlayerStat.Instance.maxHealth);
        PlayerStat.Instance.ModifyMana(PlayerStat.Instance.maxMana);
        PlayerStat.Instance.ModifyStamina(PlayerStat.Instance.maxStamina);
        PlayerStat.Instance.gameObject.layer = 12;
        PlayerStat.Instance.gameObject.tag = "Player";
        diePanel.SetActive(false);
        Time.timeScale = 1f;
        InterfaceOnScene.Instance.Show();
        Debug.Log("There will be an adv : ");
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(1f);
    }

}
