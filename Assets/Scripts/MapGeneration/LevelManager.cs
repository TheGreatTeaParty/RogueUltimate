using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManager
{
    public enum Scenes
    {
        Hub = 0,
        DB1,
        DB2,
        DB3,
    }
    public static void LoadScene(Scenes scenes)
    {
        SceneManager.LoadScene(scenes.ToString());
    }
}
