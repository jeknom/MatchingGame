using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] AudioClip basicBlock;
        [SerializeField] AudioClip bombBlock;
        private AudioSource audioSource;
        private Queue<AudioClip> soundQueue = new Queue<AudioClip>();
        
        private void Start()
        {
            this.audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (soundQueue.Count > 0 && !audioSource.isPlaying)
            {
                var clip = soundQueue.Dequeue();
                this.audioSource.PlayOneShot(clip);
            }
        }

        public void PlaySound(BlockType blockType)
        {
            AudioClip sound;
            
            if (blockType == BlockType.Bomb)
                sound = this.bombBlock;
            else
                sound = this.basicBlock;

            soundQueue.Enqueue(sound);
        }
    }
}