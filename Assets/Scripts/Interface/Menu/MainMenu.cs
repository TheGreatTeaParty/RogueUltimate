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

    private bool _cutSceneAllowed = false;
    private bool _canStartNewGame = true;
    
    private void Start()
    {
        string path = Application.persistentDataPath + "/account.dat";
        if (File.Exists(path)) {
            resumeButton.interactable = true;
            _canStartNewGame = false;
        }
    }

    public void NewGame()
    {
        if (_canStartNewGame)
        {
            SaveManager.DeletePlayer();
            SaveManager.DeleteAccount();
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
        PlayerData pl_data = SaveManager.LoadPlayer();
        AccountData acc_data = SaveManager.LoadAccount();
        AccountManager.Instance.LoadData(acc_data);
        LevelManager.Instance.LoadScene(acc_data.scene);

        if (pl_data != null)
        {
            foreach (GameObject pref in characterPrefabs)
            {
                StringBuilder sb = new StringBuilder(pref.name);
                sb.Append("(Clone)");

                Debug.Log(pref.name + "(Clone)");
                if (sb.ToString() == pl_data.gameObjectName)
                {
                    Instantiate(pref, new Vector3(pl_data.position[0], pl_data.position[1], pl_data.position[2]), Quaternion.identity);
                    var Interaface = Instantiate(playerInterface);

                    Interaface.GetComponentInChildren<CharacterManager>().LoadPlayerData(pl_data);
                    break;
                }

            }
        }
    }
    public void ExitGame()
    {
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
