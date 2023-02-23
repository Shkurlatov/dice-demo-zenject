using System;

namespace DiceDemo
{
    public class GlobalAudio
    {
        private static Action _playDiceRollingClip;

        public GlobalAudio(Action playDiceRollingClip)
        {
            _playDiceRollingClip = playDiceRollingClip ?? throw new ArgumentNullException(nameof(playDiceRollingClip));
        }

        public static void PlayDiceRollingClip()
        {
            if (_playDiceRollingClip is null)
            {
                throw new InvalidOperationException($"{typeof(GlobalAudio)} have not instance.");
            }

            _playDiceRollingClip();
        }
    }
}
