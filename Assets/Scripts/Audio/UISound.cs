using UnityEngine;

public class UISound : MonoBehaviour
{
    public void ClickSound()
    {
        AudioManager.Instance.Play("UIclick");
    }
}
