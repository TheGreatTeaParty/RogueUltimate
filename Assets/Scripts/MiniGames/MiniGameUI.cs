﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MiniGameUI : MonoBehaviour
{
    public GameObject miniGameWindow;
    public Image[] fieldImages;
    public Sprite[] coinSprites;
    public TextMeshProUGUI amountUI;
    public TextMeshProUGUI turnUI;
    public Button buttonExit;
    public Button buttonThrow;

    public void OpenMiniGame()
    {
        InterfaceManager.Instance.HideFaceElements();
        InterfaceManager.Instance.HideTavernUI();
        InterfaceManager.Instance.QuickSlots.gameObject.SetActive(false);
        miniGameWindow.gameObject.SetActive(true);
    }

    public void CloseMiniGame()
    {
        miniGameWindow.gameObject.SetActive(false);
        InterfaceManager.Instance.ShowFaceElements();
        InterfaceManager.Instance.ShowTavernUI();
        InterfaceManager.Instance.QuickSlots.gameObject.SetActive(true);

    }

    public void Defeat()
    {
        CloseMiniGame();
         // swhows lose window
    }

    public void Victory()
    {
        CloseMiniGame();
         // shows victory window
    }

    public void ChangeButtonText(string text)
    {
        buttonThrow.GetComponentInChildren<TextMeshProUGUI>().SetText(text);
    }
}