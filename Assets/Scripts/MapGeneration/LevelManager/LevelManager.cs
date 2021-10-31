using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Firebase.Analytics;

public class LevelManager : MonoBehaviour
{
    public enum Scenes
    {
        Hub = 0,
        Tavern,
        DB1,
        DB2,
        DB3,
        StartTavern,
    }


    public static LevelManager Instance;
    public GameObject LoadScreen;
    public int LoadsToAds = 4;
    private int _levelLoaded = 0;

    private AsyncOperation asyncOperation;
    [SerializeField]
    private MobAdsSimple mobAds;
    private Vector3 _playerPos;

    void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void LoadScene(Scenes scenes, Vector3 position = new Vector3())
    {
        LoadScreen.gameObject.SetActive(true);
        if(scenes != Scenes.Tavern && scenes != Scenes.StartTavern)
        {
            SaveManager.AccountAutoSave();
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart,
                new Parameter(FirebaseAnalytics.ParameterLevelName, "dungeon1"));
        }
        else if(scenes == Scenes.Tavern || scenes == Scenes.StartTavern)
        {
            _levelLoaded++;
        }
        asyncOperation = SceneManager.LoadSceneAsync(scenes.ToString());
        StartCoroutine(GetSceneLoadProgress(scenes));

        _playerPos = position;

        if (PlayerOnScene.Instance)
            PlayerOnScene.Instance.ShowPlayer();

        if (InterfaceManager.Instance)
            InterfaceManager.Instance.ShowFaceElements();
    }

    public void LoadScene(String scenes, Vector3 position = new Vector3())
    {
        LoadScreen.gameObject.SetActive(true);
        if (scenes != "Tavern" && scenes != "StartTavern")
        {
            SaveManager.AccountAutoSave();
        }
        else if (scenes == "Tavern" || scenes == "StartTavern")
        {
            _levelLoaded++;
        }

        asyncOperation = SceneManager.LoadSceneAsync(scenes);
        StartCoroutine(GetSceneLoadProgress(scenes));

        _playerPos = position;

        if (PlayerOnScene.Instance)
            PlayerOnScene.Instance.ShowPlayer();

        if (InterfaceManager.Instance)
            InterfaceManager.Instance.ShowFaceElements();
    }

    private IEnumerator GetSceneLoadProgress()
    {
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
        LoadScreen.gameObject.SetActive(false);

    }

    private IEnumerator GetSceneLoadProgress(Scenes scene)
    {
        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        LoadScreen.gameObject.SetActive(false);

        //Save data every time Tavern is loaded:
        if (scene == Scenes.StartTavern)
            SaveManager.AccountAutoSave();

        else if (scene == Scenes.Tavern)
        {
            SaveManager.AccountAutoSave();
            InterfaceManager.Instance.ShowTavernUI();
        }
        else
        {
            InterfaceManager.Instance.HideTavernUI();
        }
    }

    private IEnumerator GetSceneLoadProgress(string scene)
    {
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
        //Tries to find player and move him to the 0 position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
            player.transform.position = _playerPos;

        LoadScreen.gameObject.SetActive(false);

        //Save data every time Tavern is loaded:
        if (scene == "StartTavern")
        {
            SaveManager.AccountAutoSave();
        }

        else if (scene == "Tavern")
        {
            SaveManager.AccountAutoSave();
            InterfaceManager.Instance.ShowTavernUI();
        }
        else
        {
            if(InterfaceManager.Instance)
                InterfaceManager.Instance.HideTavernUI();
        }
        if (_levelLoaded >= LoadsToAds)
        {
            _levelLoaded = 0;
            mobAds.ShowAd();
        }
    }

}
