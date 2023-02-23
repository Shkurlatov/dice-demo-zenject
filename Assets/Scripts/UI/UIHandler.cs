using System;
using UnityEngine;

namespace DiceDemo.UI
{
    public class UIHandler : MonoBehaviour
    {
        [SerializeField] private ThemeChangePanel _themeCangePanel;
        [SerializeField] private DiceResultPanel _diceResultPanel;
        [SerializeField] private ThrowDiceButton _throwDiceButton;

        public void Init(Action<ThemeType> processThemeChangeCommand, Action processThrowDiceCommand, ThemeType initialThemeType)
        {
            if (processThemeChangeCommand is null) throw new ArgumentNullException(nameof(processThemeChangeCommand));
            if (processThrowDiceCommand is null) throw new ArgumentNullException(nameof(processThrowDiceCommand));

            _themeCangePanel.Init(processThemeChangeCommand);
            _throwDiceButton.Init(processThrowDiceCommand);
            _themeCangePanel.ChangeIndication(initialThemeType);
        }

        public void ReactToThemeChange(ThemeType themeType)
        {
            _themeCangePanel.ChangeIndication(themeType);
        }

        public void ReactToDiceThrowing()
        {
            _throwDiceButton.SetPressed();
        }

        public void ReactToDiceResult(int[] diceResult)
        {
            if (diceResult is null) throw new ArgumentNullException(nameof(diceResult));

            _diceResultPanel.PlaceDiceResultMessage(diceResult);
            _throwDiceButton.SetReleased();
        }
    }
}
