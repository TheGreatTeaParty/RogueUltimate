using UnityEngine;
using UnityEngine.Audio;


namespace Audio
{

    [System.Serializable]
    public class Sound
    {
        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume;
        [Range(1f, 3f)]
        public float pitch;

    }


}