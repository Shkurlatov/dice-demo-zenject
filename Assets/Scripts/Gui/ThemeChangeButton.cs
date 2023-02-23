using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DiceDemo.Signals;

namespace DiceDemo.Gui
{
    public class ThemeChangeButton : GuiButton
    {
        [SerializeField] private Text _buttonText;
        [SerializeField] private Color _darkTextColor;
        [SerializeField] private Color _lightTextColor;

        private int _themeIndex;
        private bool _isPressed = false;

        [Inject]
        public void Construct(string themeName, int themeIndex)
        {
            _themeIndex = themeIndex;

            gameObject.name += $" ({themeName})";
            _buttonText.text = themeName.ToUpper();
        }

        public void UpdateCondition(int currentThemeIndex)
        {
            if (_themeIndex == currentThemeIndex)
            {
                SetPressed();

                return;
            }

            if (_isPressed)
            {
                SetReleased();
            }
        }

        public override void OnClick()
        {
            _signalBus.Fire(new ThemeChangeSignal() { ThemeIndex = _themeIndex });
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

        public class Factory : PlaceholderFactory<string, int,ThemeChangeButton>
        {
        }
    }
}
