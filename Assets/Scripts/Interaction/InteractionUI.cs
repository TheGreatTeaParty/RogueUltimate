using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private GameObject Button;
    private Button InteractButton;
    private InteractDetaction interactDetaction;

    private void Start()
    {
        InteractButton = Button.GetComponent<Button>();
    }

    public void Action()
    {
        interactDetaction.CallInteraction();
        SetActive(false);
    }

    public void SetInteractDetaction(InteractDetaction _interactDetaction)
    {
        interactDetaction = _interactDetaction;
    }

    public void SetText(string text)
    {
        InteractButton.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
    public void SetActive(bool condition)
    {
        Button.SetActive(condition);
    }

}
