using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextTyper : MonoBehaviour
{
    public static event Action TyperStop; 
    public bool IsActive { get; set; } = false;
    public IEnumerator Type(float speed)
    {
        IsActive = true;
        TextMeshProUGUI text = gameObject.GetComponent<TextMeshProUGUI>();
        text.maxVisibleCharacters = 0;
        int _totalVisibleCharacters = text.text.Length;

        for(int i = 0; i < _totalVisibleCharacters; i++)
        {
            text.maxVisibleCharacters = i;
            yield return new WaitForSeconds(speed);
        }

        TyperStop?.Invoke();
        IsActive = false;
    }
}
