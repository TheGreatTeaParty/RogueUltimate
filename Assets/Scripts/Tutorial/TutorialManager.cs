using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private CutsceneDialogueManager cutsceneDialogueManager;

    private void Start()
    {
        StartDialogue();
    }

    private void StartDialogue()
    {
        var dialogue = Resources.Load<Dialogue>("Dialogues/Tutorial");
        cutsceneDialogueManager.StartDialogue(dialogue);
    }
}
