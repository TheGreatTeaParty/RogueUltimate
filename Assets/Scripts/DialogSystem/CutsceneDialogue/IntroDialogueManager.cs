using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroDialogueManager : MonoBehaviour, IDialogueManager
{
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;
    [SerializeField] private Image background;

    private Canvas UI;

    // Desperate times call for desperate measures.
    // Arrays that store the local position of DialogueUI elements.
    [SerializeField] private Vector2[] leftLayoutPositions;
    [SerializeField] private Vector2[] rightLayoutPositions;
    
    public static event Action DialogueEnded;
    
    private int _lineIndex = 0;
    private Dialogue _currentDialogue;

    private void Awake()
    {
        UI = FindObjectOfType<InterfaceManager>().GetComponent<Canvas>();
    }

    private void Start()
    {
        TextTyper.TyperStop += StopSound;
    }

    /// <summary>
    /// Hide UI and show dialogue window.
    /// </summary>
    /// <param name="dialogue">Dialogue that need to be shown.</param>
    public void StartDialogue(Dialogue dialogue)
    {
        // UI.enabled = false;
        _currentDialogue = dialogue;
        ChangeDialogueUIState(true);
        CutsceneManager.StartNextTimeline();
        NextLine();
    }
    
    private Coroutine _coroutine;
    private TextTyper _typer;
    public void NextLine()
    {
        if (_lineIndex >= _currentDialogue.Lines.Length)
        {
            ChangeDialogueUIState(false);
            // UI.enabled = true;
            DialogueEnded?.Invoke();
            Destroy(gameObject);
        }

        try
        {
            StopCoroutine(_coroutine);
        }
        catch
        {
            // It's first line.
        }

        if (_lineIndex < _currentDialogue.Lines.Length)
        {
            ChangeDialogueValues();
            PlaySound();
            _coroutine = StartTyper(0.03f);
        }
    }

    public void HandleButton()
    {
        try
        {
            if (_typer.IsActive)
            {
                SkipLine();
            }
            else
            {
                NextLine();
            }
        }
        catch
        {
            Debug.Log("There is no active dialogue.");
        }
    }

    private void SkipLine()
    {
        StopCoroutine(_coroutine);
        _typer.IsActive = false;
        text.maxVisibleCharacters = 9999;
        StopSound();
    }

    private Coroutine StartTyper(float speed)
    {
        _typer = text.GetComponentInParent<TextTyper>();
        return StartCoroutine(_typer.Type(speed));
    }

    private void ChangeDialogueValues()
    {
        characterName.text = _currentDialogue.Lines[_lineIndex].CharacterName;
        text.text = _currentDialogue.Lines[_lineIndex].DialogueLine;
        image.sprite = _currentDialogue.Lines[_lineIndex].Icon;
        ChangeLayout(_currentDialogue.Lines[_lineIndex].IconPosition);
        _lineIndex++;
    }
    
    private void PlaySound()
    {
        PlayerOnScene.Instance.dialogueAudioSource.loop = true;
        PlayerOnScene.Instance.dialogueAudioSource.clip = _currentDialogue.Lines[_lineIndex - 1].ScrollingSound;
        PlayerOnScene.Instance.dialogueAudioSource.Play();
    }

    private void StopSound()
    {
        PlayerOnScene.Instance.dialogueAudioSource.loop = false;
        PlayerOnScene.Instance.dialogueAudioSource.Stop();
    }
    
    public void ChangeDialogueUIState(bool state)
    {
        characterName.enabled = state;
        text.enabled = state;
        image.enabled = state;
        background.enabled = state;
    }

    public void ChangeLayout(Line.Position position)
    {
        switch (position)
        {
            case Line.Position.Left:
                characterName.rectTransform.localPosition = leftLayoutPositions[0];
                text.rectTransform.localPosition = leftLayoutPositions[1];
                text.alignment = TextAlignmentOptions.Left;
                image.rectTransform.localPosition = leftLayoutPositions[2]; 
                break;
                
            case Line.Position.Right:
                characterName.rectTransform.localPosition = rightLayoutPositions[0];
                text.rectTransform.localPosition = rightLayoutPositions[1];
                text.alignment = TextAlignmentOptions.Right;
                image.rectTransform.localPosition = rightLayoutPositions[2];
                break;
        }
    }
}
