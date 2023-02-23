using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DiceDemo.UI
{
    public class DiceResultPanel : MonoBehaviour
    {
        [SerializeField] private DiceResultMessage _diceResultMessage;

        private const float _transformationTimeLimit = 1.0f;
        private const float _transformationSpeed = 4.0f;
        private const int _maxMessagesCount = 10;

        private Queue<DiceResultMessage> _diceResultMessages;

        private bool _isTransforming = false;

        void Awake()
        {
            _diceResultMessages = new Queue<DiceResultMessage>();
        }

        void Update()
        {
            if (_isTransforming)
            {
                foreach (DiceResultMessage diceResultMessage in _diceResultMessages)
                {
                    diceResultMessage.TransformToNextCondition();
                }
            }
        }

        public void PlaceDiceResultMessage(int[] diceResult)
        {
            DiceResultMessage message;

            if (_diceResultMessages.Count < _maxMessagesCount)
            {
                message = Instantiate(_diceResultMessage, transform);
            }
            else
            {
                message = _diceResultMessages.Dequeue();
            }

            message.Init(_transformationSpeed, diceResult);
            _diceResultMessages.Enqueue(message);

            StartCoroutine(RunTransformingCycle());
        }

        private IEnumerator RunTransformingCycle()
        {
            foreach (DiceResultMessage diceResultMessage in _diceResultMessages)
            {
                diceResultMessage.SetNextCondition();
            }

            _isTransforming = true;

            yield return new WaitForSeconds(_transformationTimeLimit);

            _isTransforming = false;

            foreach (DiceResultMessage diceResultMessage in _diceResultMessages)
            {
                diceResultMessage.StopTransformation();
            }
        }
    }
}
