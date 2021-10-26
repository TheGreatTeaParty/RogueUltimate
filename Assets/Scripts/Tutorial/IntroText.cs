using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroText : MonoBehaviour
{
    public GameObject Level;
    private Coroutine _coroutine;
    private Animator animator;

    [SerializeField]
    private TextTyper _typer;

    private bool _isStarted = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        Invoke("ClickHandle", 1f);
    }
    private void HandleDialogString()
    {
        if (!_isStarted)
        {
            string text = LocalizationSystem.GetLocalisedValue("intro");
            //We have some dialogs to move one:
            if (text != null)
            {
                _coroutine = StartTyper(0.03f, text);
            }
            _isStarted = true;
        }
        else
        {
            FadeAway();
        }
    }
    private void SkipTyping()
    {
        StopCoroutine(_coroutine);
        _typer.StopTyping();
    }
    private void FadeAway()
    {
        animator.SetTrigger("Fade");
    }
    private Coroutine StartTyper(float speed, string Text)
    {
        return StartCoroutine(_typer.Type(speed, Text));
    }
    public void DestroyUI()
    {
        Level.SetActive(true);
        Destroy(transform.parent.gameObject);
    }

    public void ClickHandle()
    {
        if (_typer.IsActive)
        {
            SkipTyping();
        }
        else
        {
            HandleDialogString();
        }
    }
}
