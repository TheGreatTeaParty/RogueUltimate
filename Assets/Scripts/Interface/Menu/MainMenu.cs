using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    private GameObject [] characterPrefabs;
    [SerializeField]
    private GameObject playerInterface;
    [SerializeField]
    private Button resumeButton;
    [SerializeField]
    private GameObject confirmWindow;
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject LoadingScreen;

    private bool _cutSceneAllowed = false;
    private bool _canStartNewGame = true;
    
    private void Start()
    {
        string path = Application.persistentDataPath + "/player.dat";
        if (File.Exists(path)) {
            resumeButton.interactable = true;
            _canStartNewGame = false;
        }
    }

    List<AsyncOperation> asyncOperations = new List<AsyncOperation>();

    public void NewGame()
    {
        if (_canStartNewGame)
        {
            SaveManager.DeletePlayer();
            if (_cutSceneAllowed)
                LevelManager.Instance.LoadScene("CutScene");
            else
                LevelManager.Instance.LoadScene("StartTavern");

        }
        else
        {
            confirmWindow.SetActive(true);
            mainMenu.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        PlayerData data = SaveManager.LoadPlayer();
        LevelManager.Instance.LoadScene(data.scene);

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

    public void AllowCutscene()
    {
        _cutSceneAllowed = true;
    }

    public void OpenBook()
    {
        mainMenu.SetActive(true);
    }
    
}
