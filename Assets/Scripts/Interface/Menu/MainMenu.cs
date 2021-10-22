using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using TMPro;
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
    private bool _firstTime = false;

    private string AccountPath;

    private void Start()
    {
       // SaveManager.DeleteAccount();
        GPGSManger.Initialize();
        string path = Application.persistentDataPath + "/player.dat";
        AccountPath = Application.persistentDataPath + "/account.dat";

        if (File.Exists(path) && File.Exists(AccountPath))
        {
            resumeButton.interactable = true;
            _canStartNewGame = false;
        }

        GPGSManger.Auth(success =>
        {
            if (success && !File.Exists(AccountPath))
            {
                GPGSManger.ReadSaveData(GPGSManger.DEFAULT_SAVE_NAME, (status, data) =>
                {
                    //Load the existing data;
                    if (status == GooglePlayGames.BasicApi.SavedGame.SavedGameRequestStatus.Success && data.Length > 0)
                    {
                        //Load data:
                        AccountData acc_data = SaveManager.LoadCloudData(data);
                        AccountManager.Instance.LoadData(acc_data);
                        SaveManager.SaveAccount();
                    }
                    else    //First Start: 
                    {
                        _firstTime = true;
                    }

                });
            }
        });
    }

    public void NewGame()
    {
        var data = SaveManager.LoadAccount();
        if(data!= null)
            AccountManager.Instance.LoadData(data);

        if (_canStartNewGame)
        {
            SaveManager.DeletePlayer();
            if(!_firstTime)
                LevelManager.Instance.LoadScene("StartTavern");
            else
                LevelManager.Instance.LoadScene("StartTavern"); //TODO tutorial scene here:
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

        if (pl_data != null)
        {
            LevelManager.Instance.LoadScene("Tavern");
            foreach (GameObject pref in characterPrefabs)
            {
                StringBuilder sb = new StringBuilder(pref.name);
                sb.Append("(Clone)");

                if (sb.ToString() == pl_data.gameObjectName)
                {
                    Instantiate(pref, Vector3.zero, Quaternion.identity);
                    var Interaface = Instantiate(playerInterface);

                    Interaface.GetComponentInChildren<CharacterManager>().LoadPlayerData(pl_data);
                    break;
                }

            }
        }
        else
        {
            LevelManager.Instance.LoadScene("StartTavern");
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
