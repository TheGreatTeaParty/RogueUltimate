using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneDialogueManager : MonoBehaviour, IDialogueManager
{
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;

    [SerializeField] private Canvas UI;

    // Desperate times call for desperate measures.
    // Arrays that store the local position of DialogueUI elements.
    [SerializeField] private Vector2[] leftLayoutPositions;
    [SerializeField] private Vector2[] rightLayoutPositions;

    private int _lineIndex = 0;
    private Dialogue _currentDialogue;

    /// <summary>
    /// Hide UI and show dialogue window.
    /// </summary>
    /// <param name="dialogue">Dialogue that need to be shown.</param>
    public void StartDialogue(Dialogue dialogue)
    {
        UI.enabled = false;
        _currentDialogue = dialogue;
        ChangeDialogueUIState(true);
        NextLine();
    }

    public void NextLine()
    {
        if (_lineIndex >= _currentDialogue.Lines.Length)
        {
            ChangeDialogueUIState(false);
            UI.enabled = true;
        }
        
        if (_lineIndex < _currentDialogue.Lines.Length)
        {
            characterName.text = _currentDialogue.Lines[_lineIndex].CharacterName;
            text.text = _currentDialogue.Lines[_lineIndex].DialogueLine;
            image.sprite = _currentDialogue.Lines[_lineIndex].Icon;
            ChangeLayout(_currentDialogue.Lines[_lineIndex].IconPosition);
            _lineIndex++;
        }
    }

    public void ChangeDialogueUIState(bool state)
    {
        characterName.enabled = state;
        text.enabled = state;
        image.enabled = state;
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
