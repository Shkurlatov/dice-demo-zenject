using UnityEngine;
using DiceDemo.Signals;

namespace DiceDemo.Gui
{
    public class ThrowDiceButton : GuiButton
    {
        private Animator _animator;

        void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public override void OnClick()
        {
            _signalBus.Fire<ThrowDiceSignal>();
        }

        public void SetPressed()
        {
            _animator.SetTrigger("Pressed");
        }

        public void SetReleased()
        {
            _animator.SetTrigger("Released");
        }
    }
}
