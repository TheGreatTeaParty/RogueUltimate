using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;


public class MainMenu : MonoBehaviour {

    [SerializeField]
    private GameObject [] characterPrefabs;
    [SerializeField]
    private GameObject playerInterface;
    [SerializeField]
    private Button resumeButton;

    
    private void Start()
    {
        string path = Application.persistentDataPath + "/player.gay";
        if (File.Exists(path)) resumeButton.interactable = true;
    }

    public void NewGame()
    {
        LevelManager.LoadScene("StartTavern");
        SaveSystem.DeletePlayer();
    }

    public void ResumeGame()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        LevelManager.LoadScene(data.scene);

        Debug.Log("U are trying to load scene " + data.scene);
        Debug.Log("gameObject's name is " + data.gameObjectName);
        Debug.Log("gameObject's HP = " + data.currentHP);
         
        foreach(GameObject pref in characterPrefabs)
        {
            Debug.Log(pref.name + "(Clone)");
            if (pref.name + "(Clone)" == data.gameObjectName)
            {
                Instantiate(pref, new Vector3(data.position[0], data.position[1], data.position[2]), Quaternion.identity);
                Instantiate(playerInterface);
                PlayerStat.Instance.currentHealth = data.currentHP;
                PlayerStat.Instance.currentMana = data.currentMP;
                PlayerStat.Instance.currentStamina = data.currentSP;
                PlayerStat.Instance.maxHealth = data.maxHP;
                PlayerStat.Instance.maxMana = data.maxMP;
                PlayerStat.Instance.maxStamina = data.maxSP;

                break;
            }
  
        }
    }   

    public void ExitGame()
    {
        Debug.Log("Trying to exit game doesn't work in Editor");
        Application.Quit();
    }

}
