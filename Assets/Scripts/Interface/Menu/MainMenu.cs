﻿using UnityEngine;
using UnityEngine.UI;
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
