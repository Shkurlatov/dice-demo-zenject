using System;
using UnityEngine;

namespace DiceDemo.UI
{
    public class ThrowDiceButton : UIButton
    {
        private Animator _animator;

        private Action _processThrowDiceCommand;

        public void Init(Action processThrowDiceCommand)
        {
            if (processThrowDiceCommand is null) throw new ArgumentNullException(nameof(processThrowDiceCommand));

            _animator = GetComponent<Animator>();
            _processThrowDiceCommand = processThrowDiceCommand;

            SetReleased();
        }

        public void SetPressed()
        {
            _animator.SetTrigger("Pressed");
        }

        public void SetReleased()
        {
            _animator.SetTrigger("Released");
        }

        protected override void OnClick()
        {
            _processThrowDiceCommand();
        }
    }
}
