using System;
using UnityEngine;

namespace DiceDemo.System
{
    public class AudioPlayer
    {
        readonly Settings _settings;
        readonly AudioSource _audioSource;

        public AudioPlayer(Settings settings, AudioSource audioSource)
        {
            _settings = settings;
            _audioSource = audioSource;
        }

        public void PlayButtonClickSound()
        {
            _audioSource.PlayOneShot(_settings.ButtonClickSound);
        }

        public void PlayDiceShakingSound()
        {
            _audioSource.PlayOneShot(_settings.DiceShakingSound);
        }

        public void PlayDiceRollingSound()
        {
            _audioSource.PlayOneShot(_settings.DiceRollingSound);
        }

        [Serializable]
        public class Settings
        {
            public AudioClip ButtonClickSound;
            public AudioClip DiceShakingSound;
            public AudioClip DiceRollingSound;
        }
    }
}
