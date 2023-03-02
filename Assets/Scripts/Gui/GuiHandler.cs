using System;
using Zenject;

namespace DiceDemo.Gui
{
    public class GuiHandler
    {
        private readonly ThrowDiceButton _throwDiceButton;
        private readonly ThemeChangeButtonsManager _themeChangeButtonsManager;
        private readonly DiceResultMessagesManager _diceResultMessagesManager;

        public GuiHandler(
            ThrowDiceButton throwDiceButton,
            ThemeChangeButtonsManager themeChangeButtonsManager, 
            DiceResultMessagesManager diceResultMessagesManager)
        {
            _throwDiceButton = throwDiceButton;
            _themeChangeButtonsManager = themeChangeButtonsManager;
            _diceResultMessagesManager = diceResultMessagesManager;
        }

        public void ReactToDiceThrowing()
        {
            _throwDiceButton.SetPressed();
        }

        public void ReactToThemeChange(int currentThemeIndex)
        {
            _themeChangeButtonsManager.ChangeIndication(currentThemeIndex);
        }

        public void ReactToDiceResult(int[] diceResult)
        {
            if (diceResult is null) throw new ArgumentNullException(nameof(diceResult));

            _diceResultMessagesManager.PublishDiceResultMessage(diceResult);
            _throwDiceButton.SetReleased();
        }
    }
}

