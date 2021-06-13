using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

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

    private AsyncOperation asyncOperation;

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
            SaveManager.SavePlayer();
            SaveManager.SaveAccount();
        }

        asyncOperation = SceneManager.LoadSceneAsync(scenes.ToString());
        StartCoroutine(GetSceneLoadProgress(scenes));

        //Tries to find player and move him to the 0 position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
            player.transform.position = position;

        if (PlayerOnScene.Instance)
            PlayerOnScene.Instance.ShowPlayer();

        if (InterfaceManager.Instance)
            InterfaceManager.Instance.ShowFaceElements();
    }

    public void LoadScene(String scenes, Vector3 position = new Vector3())
    {
        LoadScreen.gameObject.SetActive(true);
        if(scenes != "Tavern" && scenes != "StartTavern")
        {
            SaveManager.SavePlayer();
            SaveManager.SaveAccount();
        }

        asyncOperation = SceneManager.LoadSceneAsync(scenes);
        StartCoroutine(GetSceneLoadProgress(scenes));

        //Tries to find player and move him to the 0 position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
            player.transform.position = position;

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
            SaveManager.SaveAccount();

        else if (scene == Scenes.Tavern)
        {
            SaveManager.SavePlayer();
            SaveManager.SaveAccount();
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

        LoadScreen.gameObject.SetActive(false);

        //Save data every time Tavern is loaded:
        if (scene == "StartTavern")
        {
            SaveManager.SaveAccount();
        }

        else if (scene == "Tavern")
        {
            SaveManager.SavePlayer();
            SaveManager.SaveAccount();
            InterfaceManager.Instance.ShowTavernUI();
        }
        else
        {
            InterfaceManager.Instance.HideTavernUI();
        }
        Debug.Log("DATA SAVED!");
    }

}
