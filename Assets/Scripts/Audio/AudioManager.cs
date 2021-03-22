using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += TryToChildPlayer;
    }

    private void TryToChildPlayer(Scene arg0, LoadSceneMode arg1)
    {
        try
        {
            transform.SetParent(PlayerOnScene.Instance.transform);
        }
        catch
        {
            Debug.Log("There is no player");
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log($"Could not find sound with sign: {name}");
            return;
        }
        s.source.Play();
    }
}