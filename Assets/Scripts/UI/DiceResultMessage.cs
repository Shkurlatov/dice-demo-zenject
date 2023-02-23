using System;
using UnityEngine;
using UnityEngine.UI;

namespace DiceDemo.UI
{
    public class DiceResultMessage : MonoBehaviour
    {
        [SerializeField] private Text _messageText;

        private const float _interval = 30.0f;
        private const float _startOffsetFactor = 1.25f;
        private const float _fadeFactor = 0.6f;

        private float _transformationSpeed;

        private Vector3 _offset;
        private Image _background;
        private Color _textColor;
        private Color _imageColor;

        private Vector3 _position;
        private float _transparency;

        void Awake()
        {
            _offset = new Vector3(0.0f, -(GetComponent<RectTransform>().rect.height + _interval), 0.0f);
            _background = GetComponent<Image>();
            _textColor = _messageText.color;
            _imageColor = _background.color;
        }

        public void Init(float transformationSpeed, int[] diceResult)
        {
            if (diceResult is null) throw new ArgumentNullException(nameof(diceResult));

            _transformationSpeed = transformationSpeed;

            ResetCondition();
            SetMessageText(diceResult);
        }

        public void SetNextCondition()
        {
            _position += _offset;
            _transparency *= _fadeFactor;
            _textColor = new Color(_textColor.r, _textColor.g, _textColor.b, _transparency);
            _imageColor = new Color(_imageColor.r, _imageColor.g, _imageColor.b, _transparency);
        }

        public void TransformToNextCondition()
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _position, Time.deltaTime * _transformationSpeed);
            _messageText.color = Color.Lerp(_messageText.color, _textColor, Time.deltaTime * _transformationSpeed);
            _background.color = Color.Lerp(_background.color, _imageColor, Time.deltaTime * _transformationSpeed);
        }

        public void StopTransformation()
        {
            transform.localPosition = _position;
            _messageText.color = _textColor;
            _background.color = _imageColor;
        }

        private void ResetCondition()
        {
            _position = new Vector3(_interval, -_interval, 0.0f) - _offset;
            transform.localPosition = _position + (_offset * _startOffsetFactor);
            _transparency = 1.0f / _fadeFactor;
            _textColor = new Color(_textColor.r, _textColor.g, _textColor.b, 0.0f);
            _imageColor = new Color(_imageColor.r, _imageColor.g, _imageColor.b, 0.0f);
        }

        private void SetMessageText(int[] diceResult)
        {
            string message = diceResult[0].ToString();

            if (diceResult.Length > 1)
            {
                int sum = diceResult[0];

                for (int i = 1; i < diceResult.Length; i++)
                {
                    message += $" + {diceResult[i]}";
                    sum += diceResult[i];
                }

                message += $" = {sum}";
            }

            _messageText.text = message;
        }
    }
}
