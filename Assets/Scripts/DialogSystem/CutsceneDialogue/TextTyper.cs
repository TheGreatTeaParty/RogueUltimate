using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextTyper : MonoBehaviour
{
    public bool IsActive { get; set; } = false;

    private TextMeshProUGUI text;

    private void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        text.text = "";
    }

    public IEnumerator Type(float speed, string Newtext)
    {
        text.text = Newtext;
        IsActive = true;
        text.maxVisibleCharacters = 0;
        int _totalVisibleCharacters = text.text.Length;

        for(int i = 0; i < _totalVisibleCharacters; i++)
        {
            text.maxVisibleCharacters = i;
            if(i%5==0)
                PlaySound();
            yield return new WaitForSeconds(speed);
        }
        IsActive = false;
    }

    public void StopTyping()
    {
        IsActive = false;
        text.maxVisibleCharacters = text.text.Length;
    }

    private void PlaySound()
    {
        AudioManager.Instance.Play("Talk");
    }
}
