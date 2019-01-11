using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] List<AudioClip> blockBreakSounds = new List<AudioClip>();
        private AudioSource audioSource;

        public List<AudioClip> BlockBreakSounds { get { return blockBreakSounds; } set { blockBreakSounds = value; } }
        
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(AudioClip sound)
        {
            audioSource.PlayOneShot(sound);
        }
    }
}