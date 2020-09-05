using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public static class LevelManager
{
    public enum Scenes
    {
        Hub = 0,
        Tavern,
        DB1,
        DB2,
        DB3,
    }
    
    public static void LoadScene(Scenes scenes)
    {
        SceneManager.LoadScene(scenes.ToString());

        //Tries to find player and move him to the 0 position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
            player.transform.position = new Vector3(0f, 0f, 0f);
        
    }
    
    public static void LoadScene(String scenes)
    {
        SceneManager.LoadScene(scenes);

        //Tries to find player and move him to the 0 position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) 
            player.transform.position = new Vector3(0f, 0f, 0f);

    }

}
