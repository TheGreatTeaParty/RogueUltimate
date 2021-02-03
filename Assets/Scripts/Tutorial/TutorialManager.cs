using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;

public class TutorialManager : MonoBehaviour
{
    #region Singleton

    public static TutorialManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.Log("Second instance of " + this.ToString() + "was created!");
            Destroy(gameObject);
        }
    }
    

    #endregion
    
    private IntroDialogueManager introDialogueManager;
    [SerializeField] private GameObject dialogueInterface;
    [SerializeField] private Canvas mainUI;

    private void Start()
    {
        mainUI = InterfaceManager.Instance.GetComponent<Canvas>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        var interfaceInstance = Instantiate(dialogueInterface);
        introDialogueManager = interfaceInstance.GetComponent<IntroDialogueManager>();
        introDialogueManager.StartDialogue(dialogue);
    }

    public void ChangeUIState(bool state)
    {
        mainUI.enabled = state;
    }
}
