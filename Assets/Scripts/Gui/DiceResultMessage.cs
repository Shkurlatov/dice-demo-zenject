using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DiceDemo.Gui
{
    public class DiceResultMessage : GuiElement
    {
        [SerializeField] private Text _messageText;

        private Vector2 _targetPosition;
        private float _targetOpacity;
        private float _opacity;
        private Image _background;

        public string MessageText
        {
            set { _messageText.text = value; }
        }

        public Vector2 TargetPosition
        {
            get { return _targetPosition; }
            set { _targetPosition = value; }
        }

        public float TargetOpacity
        {
            get { return _targetOpacity; }
            set { _targetOpacity = value; }
        }

        public float Opacity
        {
            get { return _opacity; }
            set 
            {
                _opacity = value;
                SetOpacity();
            }
        }

        void Awake()
        {
            _background = GetComponent<Image>();
        }

        private void SetOpacity()
        {
            _messageText.color = new Color(_messageText.color.r, _messageText.color.g, _messageText.color.b, _opacity);
            _background.color = new Color(_background.color.r, _background.color.g, _background.color.b, _opacity);
        }

        public class Factory : PlaceholderFactory<DiceResultMessage>
        {
        }
    }
}
