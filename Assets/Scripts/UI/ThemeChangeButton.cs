using System;
using UnityEngine;
using UnityEngine.UI;

namespace DiceDemo.UI
{
    public class ThemeChangeButton : UIButton
    {
        [SerializeField] private Text _buttonText;
        [SerializeField] private Color _darkTextColor;
        [SerializeField] private Color _lightTextColor;

        private ThemeType _themeType;
        private bool _isPressed = false;

        private Action<ThemeType> _processThemeChangeCommand;

        public void Init(ThemeType themeType, Action<ThemeType> processThemeChangeCommand)
        {
            if (processThemeChangeCommand is null) throw new ArgumentNullException(nameof(processThemeChangeCommand));

            _themeType = themeType;
            _buttonText.text = _themeType.ToString().ToUpper();
            _processThemeChangeCommand = processThemeChangeCommand;
        }

        public void UpdateCondition(ThemeType themeType)
        {
            if (_themeType == themeType)
            {
                SetPressed();

                return;
            }

            if (_isPressed)
            {
                SetReleased();
            }
        }

        protected override void OnClick()
        {
            _processThemeChangeCommand(_themeType);
        }

        private void SetPressed()
        {
            _buttonText.color = _lightTextColor;
            _isPressed = true;
        }

        private void SetReleased()
        {
            _buttonText.color = _darkTextColor;
            _isPressed = false;
        }
    }
}
