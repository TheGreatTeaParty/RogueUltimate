using UnityEngine;

public class PlayerButtonCallBack : MonoBehaviour
{
    public delegate void OnButtonCallback();
    public OnButtonCallback onStateChanged;

    public void ChangeState()
    {
        onStateChanged?.Invoke();
    }
    public void ShowPanel()
    {
        InterfaceManager.Instance.ShowPlayerPanel();
    }
    
}
