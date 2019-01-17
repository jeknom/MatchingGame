using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] List<AudioClip> blockBreakSounds = new List<AudioClip>();
        private AudioSource audioSource;

        public List<AudioClip> BlockBreakSounds { get { return blockBreakSounds; } }
        
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(AudioClip sound)
        {
            Debug.Assert(sound != null, "Cannot play a null AudioClip");
            audioSource.PlayOneShot(sound);
        }
    }
}