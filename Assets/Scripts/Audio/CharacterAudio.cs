using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    [SerializeField]
    AudioClip[] step_sounds;

    [SerializeField]
    AudioClip take_damage;

    [SerializeField]
    AudioClip death;

    [SerializeField]
    AudioClip[] attack;

    [SerializeField]
    AudioClip swing;

    [SerializeField]
    AudioClip[] extraSounds;

    [SerializeField]
    AudioSource[] source;
    public enum Type
    {
        fist,
        mele,
        bow,
        magic,
    };
    
    public void StepSound()
    {
        source[0].PlayOneShot(step_sounds[Random.Range(0, step_sounds.Length)]);
    }
    public void DamageSound()
    {
        if(take_damage)
            source[1].PlayOneShot(take_damage);
        
    }
    public void DeathSound()
    {
        if(death)
            source[1].PlayOneShot(death);
        
    }
    public void AttackSound(Type type)
    {
        
        switch (type)
        {
            case Type.fist:
                {
                    source[1].PlayOneShot(attack[0]);
                    break;
                }
            case Type.mele:
                {
                    source[1].PlayOneShot(attack[1]);
                    break;
                }
        }
        
    }

    public void SwingSound()
    {
        source[1].PlayOneShot(swing);
    }

    public void PlayExtra(int number)
    {
        source[1].PlayOneShot(extraSounds[number]);
    }
}
