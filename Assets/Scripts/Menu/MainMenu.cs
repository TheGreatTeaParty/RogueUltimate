using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {


    public void NewGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResumeGame(){
        //There will be a load sys, but now i don't know how we'll do it :))
    }

    public void Settings(){

    }

    public void ExitGame(){
        Debug.Log("Trying to exit game doesn't work in Editor");
        Application.Quit();
    }

}
