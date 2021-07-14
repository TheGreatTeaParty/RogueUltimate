using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMusic : MonoBehaviour
{
    public static DungeonMusic Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);
    }

    public AudioSource Ambient;
    public AudioSource Combat;
    [Space]
    [Header("Battle Clips:")]
    public AudioClip[] BattleClips;

    private float volume = 0.3f;
    private bool _fadeOUT = false;
    private bool _fadeIN = false;

    public void TurnTheCombatMusic()
    {
        if (BattleClips.Length > 0)
        {
            Combat.clip = BattleClips[Random.Range(0, BattleClips.Length)];
            FadeOut();
        }
        else
        {
            Debug.LogWarning($"You are missing Battle Clips in {name}!");
        }
    }
    public void TurnTheCombatMusicOFF()
    {
        FadeIn();
    }

    private void FadeIn()
    {
        _fadeIN = true;
    }

    private void FadeOut()
    {
        Combat.volume = 0;
        _fadeOUT = true;
        Combat.Play();
    }
    private void FixedUpdate()
    {
        if (_fadeOUT && Combat.volume < volume)
        {
            Combat.volume += 0.05f * Time.deltaTime;
        }
        else if(_fadeOUT && Combat.volume >= volume)
        {
            _fadeOUT = false;
        }

        else if(_fadeIN && Combat.volume > 0)
        {
            Combat.volume -= 0.025f * Time.deltaTime;
        }
        else if (_fadeIN && Combat.volume <= 0)
        {
            _fadeIN = false;
            Combat.Stop();
        }
    }

}
