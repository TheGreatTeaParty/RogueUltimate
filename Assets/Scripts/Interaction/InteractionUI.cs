﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionUI : MonoBehaviour
{
    private Button _interactButton;
    private InteractDetaction _interactDetaction;

    private void Start()
    {
        _interactButton = GetComponentInChildren<Button>();
        _interactButton.gameObject.SetActive(false);
    }

    public void Action()
    {
        _interactDetaction.CallInteraction();
        _interactDetaction.DeleteInteractionData();
        SetActive(false);
    }

    public void SetInteractDetaction(InteractDetaction interactDetaction)
    {
        _interactDetaction = interactDetaction;
    }

    public void SetText(string text)
    {
        _interactButton.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
    
    public void SetActive(bool condition)
    {
        _interactButton.gameObject.SetActive(condition);
    }

}
