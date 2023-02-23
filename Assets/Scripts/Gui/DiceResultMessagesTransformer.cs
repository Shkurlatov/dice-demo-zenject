using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;

namespace DiceDemo.Gui
{
    public class DiceResultMessagesTransformer : ITickable
    {
        private readonly Settings _settings;
        private readonly Queue<DiceResultMessage> _diceResultMessages;

        private bool _isTransforming;

        public DiceResultMessagesTransformer(Settings settings, Queue<DiceResultMessage> diceResultMessages)
        {
            _settings = settings;
            _diceResultMessages = diceResultMessages;
        }

        public async void PlaceDiceResultMessageAsync(DiceResultMessage diceResultMessage)
        {
            SetNextTargetConditions();
            SetStartCondition(diceResultMessage);
            _diceResultMessages.Enqueue(diceResultMessage);
            _isTransforming = true;

            await UniTask.Delay(_settings.TransformationTimeLimit);

            _isTransforming = false;
            CompleteTransformation();
        }

        public void Tick()
        {
            if (_isTransforming)
            {
                foreach (DiceResultMessage diceResultMessage in _diceResultMessages)
                {
                    diceResultMessage.Position = Vector2
                        .Lerp(diceResultMessage.Position, diceResultMessage.TargetPosition, Time.deltaTime * _settings.TransformationSpeed);

                    diceResultMessage.Opacity = Mathf
                        .Lerp(diceResultMessage.Opacity, diceResultMessage.TargetOpacity, Time.deltaTime * _settings.TransformationSpeed);
                }
            }
        }

        private void SetNextTargetConditions()
        {
            foreach (DiceResultMessage diceResultMessage in _diceResultMessages)
            {
                diceResultMessage.TargetPosition += diceResultMessage.Offset;
                diceResultMessage.TargetOpacity *= _settings.FadeFactor;
            }
        }

        private void SetStartCondition(DiceResultMessage diceResultMessage)
        {
            diceResultMessage.TargetPosition = diceResultMessage.StartPosition;
            diceResultMessage.Position = diceResultMessage.TargetPosition * _settings.StartOffsetFactor;
            diceResultMessage.TargetOpacity = 1.0f;
            diceResultMessage.Opacity = 0.0f;
        }

        private void CompleteTransformation()
        {
            foreach (DiceResultMessage diceResultMessage in _diceResultMessages)
            {
                diceResultMessage.Position = diceResultMessage.TargetPosition;
                diceResultMessage.Opacity = diceResultMessage.TargetOpacity;
            }
        }

        [Serializable]
        public class Settings
        {
            public float StartOffsetFactor;
            public float FadeFactor;
            public float TransformationSpeed;
            public int TransformationTimeLimit;
        }
    }
}
