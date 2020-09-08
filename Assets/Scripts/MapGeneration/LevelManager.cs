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
    
    public static void LoadScene(Scenes scenes,Vector3 position = new Vector3())
    {
        SceneManager.LoadScene(scenes.ToString());

        //Tries to find player and move him to the 0 position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null )
            player.transform.position = position;
        
    }
    
    public static void LoadScene(String scenes, Vector3 position = new Vector3())
    {
        SceneManager.LoadScene(scenes);

        //Tries to find player and move him to the 0 position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) 
            player.transform.position = position;

    }
}
