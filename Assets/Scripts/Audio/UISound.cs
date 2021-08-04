using UnityEngine;

public class UISound : MonoBehaviour
{
    public void ClickSound()
    {
        AudioManager.Instance.Play("UIclick");
    }
    public void NewGameSound()
    {
        AudioManager.Instance.Play("NewGame");
    }
    public void ResumeSound()
    {
        AudioManager.Instance.Play("Continue");
    }
}
