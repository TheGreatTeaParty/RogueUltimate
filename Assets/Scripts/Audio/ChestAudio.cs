using UnityEngine;

public class ChestAudio : MonoBehaviour
{
    [SerializeField]
    AudioClip open_sound;

    [SerializeField]
    AudioClip money_sound;

    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void OpenSound()
    {
        source.PlayOneShot(open_sound);
    }
    public void MoneySound()
    {
        source.PlayOneShot(money_sound);
    }
}
