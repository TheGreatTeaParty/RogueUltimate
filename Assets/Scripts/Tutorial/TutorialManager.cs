using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;

    private void Start()
    {
        var dialogue = Resources.Load<Dialogue>("Dialogues/Tutorial");
        dialogueUI.StartDialogue(dialogue);
    }
}
