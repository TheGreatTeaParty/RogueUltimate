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
        var UI = InterfaceManager.Instance;
        
        UI.HideAll();
        diePanel.SetActive(true);
        StartCoroutine(Waiter());
        Time.timeScale = 0f;
    }

    public void DieButton()
    {
        var UI = InterfaceManager.Instance;
        
        SaveManager.DeletePlayer();
        UI.HideAll();
        diePanel.SetActive(false);
        
        Destroy(InterfaceManager.Instance.gameObject);
        Destroy(AudioManager.Instance.gameObject);
        Destroy(PlayerStat.Instance.gameObject);
        Destroy(PlayerCamera.Instance.gameObject);
        
        LevelManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }

    public void ResurrectButton()
    {
        var UI = InterfaceManager.Instance;
        
        PlayerStat.Instance.ModifyHealth(PlayerStat.Instance.MaxHealth);
        PlayerStat.Instance.ModifyMana(PlayerStat.Instance.MaxMana);
        PlayerStat.Instance.ModifyStamina(PlayerStat.Instance.MaxStamina);
        
        PlayerStat.Instance.gameObject.layer = 12;
        PlayerStat.Instance.gameObject.tag = "Player";
        
        diePanel.SetActive(false);
        UI.ShowFaceElements();
        Time.timeScale = 1f;
        InterfaceManager.Instance.ShowFaceElements();
        Debug.Log("There will be an adv : ");
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(1f);
    }

}
