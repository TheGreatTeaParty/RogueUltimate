using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector[] timelineSequence;

    private int timelineCounter = 0;

    public static event Action ChangeTimeline;

    private void Start()
    {
        //IntroDialogueManager.DialogueEnded += ChangeTimelineHandler;
        ChangeTimeline += ChangeTimelineHandler;
    }

    public static void StartNextTimeline()
    {
        ChangeTimeline?.Invoke();
    }
    
    public void ChangeTimelineHandler()
    {
        if (timelineCounter + 1 < timelineSequence.Length)
        {
            timelineSequence[timelineCounter].Stop();
            timelineCounter++;
            timelineSequence[timelineCounter].Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
