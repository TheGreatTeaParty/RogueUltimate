using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTalkTrigger : MonoBehaviour
{
    private bool _isActive = false;
    [SerializeField] private TutorialGuy _tutorialGuy;
   // [SerializeField] private Dialogue dialogue;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_isActive)
        {
            _isActive = true;
            StartCoroutine(_tutorialGuy.SlowDissolve(true));
           // TutorialManager.Instance.StartDialogue(dialogue);
        }
    }
}
