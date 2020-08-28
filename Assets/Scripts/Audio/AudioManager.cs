using System;
using UnityEngine;
using UnityEngine.Audio;


namespace Audio
{

    
    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;


        private void Awake()
        {
            foreach (Sound s in sounds)
            {
                
            }
        }
    }


}