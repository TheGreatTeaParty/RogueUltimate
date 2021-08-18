using UnityEngine;
using UnityEngine.UI;

public class PlayerButtonCallBack : MonoBehaviour
{
    public delegate void OnButtonCallback();
    public OnButtonCallback onStateChanged;

    private InterfaceManager _interfaceManager;
    private Animator _animator;
    private Image _outline;

    private void Start()
    {
        _outline = GetComponent<Image>();
        _animator = GetComponent<Animator>();

        if (_animator)
            _outline.enabled = false;

        _interfaceManager = InterfaceManager.Instance;
        _interfaceManager.HighlightNavButton += TurnTheHighlight;
    }

    public void ChangeState()
    {
        onStateChanged?.Invoke();
        TurnOFFTheHighlight();
    }

    public void ShowPanel()
    {
        _interfaceManager.ShowPlayerPanel();
    }

    private void TurnTheHighlight()
    {
        if (_outline && _animator)
        {
            _outline.enabled = true;
            _animator.SetBool("Outline", true);
        }
    }
    private void TurnOFFTheHighlight()
    {
        if (_outline && _animator)
        {
            _animator.SetBool("Outline", false);
            _outline.enabled = false;
        }
    }
}
