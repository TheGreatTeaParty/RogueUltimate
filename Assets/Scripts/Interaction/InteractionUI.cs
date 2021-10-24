using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionUI : MonoBehaviour
{
    [SerializeField]
    private Button _interactButton;
    private TextMeshProUGUI _interactText;

    [SerializeField]
    private Button _talkButton;
    private InteractDetaction _interactDetaction;

    private void Start()
    {
        _interactButton.gameObject.SetActive(false);
        _talkButton.gameObject.SetActive(false);
        _interactText = _interactButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Action()
    {
        _interactDetaction.CallInteraction();
        _interactDetaction.DeleteInteractionData();

        SetInteractActive(false);
        SetTalkActive(false);
    }

    public void TalkAction()
    {
        _interactDetaction.CallTalkAction();
        _interactDetaction.DeleteInteractionData();
        SetInteractActive(false);
        SetTalkActive(false);
    }

    public void SetInteractDetaction(InteractDetaction interactDetaction)
    {
        _interactDetaction = interactDetaction;
    }

    public void SetActionText(string text)
    {
        _interactText.text = LocalizationSystem.GetLocalisedValue(text.Replace(' ', '_'));
    }

    public void SetTalkActive(bool condition)
    {
        _talkButton.gameObject.SetActive(condition);
    }

    public void SetInteractActive(bool condition)
    {
        _interactButton.gameObject.SetActive(condition);
    }

}
