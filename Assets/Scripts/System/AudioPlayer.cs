using UnityEngine;

namespace DiceDemo.System
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private AudioClip _diceShaking;
        [SerializeField] private AudioClip _diceRolling;
        [SerializeField] private AudioClip _buttonClick;

        private GlobalAudio _globalAudio;

        void Awake()
        {
            _globalAudio = new GlobalAudio(PlayDiceRollingSound);
        }

        public void PlayButtonClickSound()
        {
            _audioSource.PlayOneShot(_buttonClick);
        }

        public void PlayDiceShakingSound()
        {
            _audioSource.PlayOneShot(_diceShaking);
        }

        private void PlayDiceRollingSound()
        {
            _audioSource.PlayOneShot(_diceRolling);
        }
    }
}
