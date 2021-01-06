using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;

    [SerializeField] private Canvas UI;
    
    private int _lineIndex = 0;
    private Dialogue _currentDialogue;

    public void StartDialogue(Dialogue dialogue)
    {
        UI.enabled = false;
        _currentDialogue = dialogue;
        ChangeLine();
    }

    public void ChangeLine()
    {
        if (_lineIndex >= _currentDialogue.Lines.Length)
        {
            HideDialogueUI();
            UI.enabled = true;
        }
        
        if (_lineIndex < _currentDialogue.Lines.Length)
        {
            characterName.text = _currentDialogue.Lines[_lineIndex].CharacterName;
            text.text = _currentDialogue.Lines[_lineIndex].DialogueLine;
            image.sprite = _currentDialogue.Lines[_lineIndex].Icon;
            _lineIndex++;
        }
    }

    private void HideDialogueUI()
    {
        characterName.enabled = false;
        text.enabled = false;
        image.enabled = false;
    }
}
