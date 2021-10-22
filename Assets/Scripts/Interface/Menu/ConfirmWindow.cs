using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmWindow : MonoBehaviour {

    public void StartNewGame()
    {
        SaveManager.DeletePlayer();
        LevelManager.Instance.LoadScene("StartTavern");
    }
}
