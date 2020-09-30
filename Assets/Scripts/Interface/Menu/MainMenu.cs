using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    private GameObject [] characterPrefabs;
    [SerializeField]
    private GameObject playerInterface;
    [SerializeField]
    private Button resumeButton;

    
    private void Start()
    {
        string path = Application.persistentDataPath + "/player.dat";
        if (File.Exists(path)) resumeButton.interactable = true;
    }

    public void NewGame()
    {
        SaveManager.DeletePlayer();
        LevelManager.LoadScene("StartTavern");
    }

    public void ResumeGame()
    {
        PlayerData data = SaveManager.LoadPlayer();
        LevelManager.LoadScene(data.scene);

        foreach(GameObject pref in characterPrefabs)
        {
            StringBuilder sb = new StringBuilder(pref.name);
            sb.Append("(Clone)");

            Debug.Log(pref.name + "(Clone)");
            if (sb.ToString() == data.gameObjectName)
            {
                Instantiate(pref, new Vector3(data.position[0], data.position[1], data.position[2]), Quaternion.identity);
                Instantiate(playerInterface);
                PlayerStat.Instance.CurrentHealth = data.currentHP;
                PlayerStat.Instance.CurrentMana = data.currentMP;
                PlayerStat.Instance.CurrentStamina = data.currentSP;
                PlayerStat.Instance.MaxHealth = data.maxHP;
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
