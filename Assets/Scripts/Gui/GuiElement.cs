using System;
using UnityEngine;
using Zenject;

namespace DiceDemo.Gui
{
    public abstract class GuiElement : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private Vector2 _startPosition;
        private Vector2 _offset;

        public Vector2 Position
        {
            set { _rectTransform.anchoredPosition = value; }
            get { return _rectTransform.anchoredPosition; }
        }

        public Vector2 StartPosition
        {
            get { return _startPosition; }
        }

        public Vector2 Offset
        {
            get { return _offset; }
        }

        [Inject]
        public void Construct(Settings settings)
        {
            _rectTransform = GetComponent<RectTransform>();

            float width = _rectTransform.rect.width;
            float height = _rectTransform.rect.height;

            int anchorX = (_rectTransform.anchorMin.x == 1) ? -1 : 1;
            int anchorY = (_rectTransform.anchorMin.y == 1) ? -1 : 1;

            float startPositionX = anchorX * (width / 2 + settings.Interval);
            float startPositionY = anchorY * (height / 2 + settings.Interval);

            float offsetY = anchorY * (height + settings.Interval);

            _startPosition = new Vector2(startPositionX, startPositionY);
            _offset = new Vector2(0, offsetY);
        }

        [Serializable]
        public class Settings
        {
            public float Interval;
        }
    }
}
