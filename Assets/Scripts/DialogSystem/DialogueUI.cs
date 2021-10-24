using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogueUI : MonoBehaviour
{
    [SerializeField]
    private Image _characterImage;
    [SerializeField]
    private TextMeshProUGUI _characterName;
    [SerializeField]
    private Animator _animator;

    private Coroutine _coroutine;
    private DialogSystem.ECharacterNames _characterEName;
    private int _phraseCount;
    private int _currentCount = 0;

    [SerializeField]
    private TextTyper _typer;

    private void Start()
    {
        Invoke("ClickHandle", 1.1f);
        AudioManager.Instance.Play("Dialog");
    }

    private void NextDialogue()
    {
        HandleDialogString();
        _currentCount++;
    }

    private void HandleDialogString()
    {
        if (_currentCount < _phraseCount)
        {
            string text = DialogSystem.GetDialogText(_characterEName);
            //We have some dialogs to move one:
            if (text != null)
            {
                _coroutine = StartTyper(0.04f, text);
            }
            //We do not have any dialogs left, repeat:
            else
            {
                text = DialogSystem.GetDialogTextPlaceHolder(_characterEName);    //Currently return placeholder
                if (text != null)
                    _coroutine = StartTyper(0.04f, text);
                else
                {
                    EndDialogue();
                }
            }
        }
        else
        {
            EndDialogue();
        }

    }
    private void SkipTyping()
    {
        StopCoroutine(_coroutine);
        _typer.StopTyping();
    }

    private Coroutine StartTyper(float speed, string Text)
    {
        return StartCoroutine(_typer.Type(speed, Text));
    }
    private void EndDialogue()
    {
        _animator.SetTrigger("Dissolve");
    }
    public void DestroyUI()
    {
        Destroy(gameObject);
    }

    public void ClickHandle()
    {
        if(_typer.IsActive)
        {
            SkipTyping();
        }
        else
        {
            NextDialogue();
        }
    }

    public void Init(Sprite characterSprite, DialogSystem.ECharacterNames characterEName, int phraseCount)
    {
        _characterImage.sprite = characterSprite;
        _characterEName = characterEName;
        _characterName.text = LocalizationSystem.GetLocalisedValue(characterEName.ToString());
        _phraseCount = phraseCount;
    }
}
