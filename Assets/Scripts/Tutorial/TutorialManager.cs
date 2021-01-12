using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private IntroDialogueManager introDialogueManager;
    
    public void StartDialogue(Dialogue dialogue)
    {
        introDialogueManager.StartDialogue(dialogue);
    }
}
