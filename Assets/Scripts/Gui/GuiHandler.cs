using System;
using Zenject;

namespace DiceDemo.Gui
{
    public class GuiHandler : IInitializable
    {
        private readonly ThemeChangeButtonsManager _themeChangeButtonsController;
        private readonly ThrowDiceButton _throwDiceButton;
        private readonly DiceResultMessagesManager _diceResultMessagesManager;

        public GuiHandler(
            ThemeChangeButtonsManager themeChangeButtonsController, 
            DiceResultMessagesManager diceResultMessagesManager, 
            ThrowDiceButton throwDiceButton)
        {
            _themeChangeButtonsController = themeChangeButtonsController;
            _throwDiceButton = throwDiceButton;
            _diceResultMessagesManager = diceResultMessagesManager;
        }

        public void Initialize()
        {
            _throwDiceButton.Position = _throwDiceButton.StartPosition;
        }

        public void ReactToThemeChange(int currentThemeIndex)
        {
            _themeChangeButtonsController.ChangeIndication(currentThemeIndex);
        }

        public void ReactToDiceThrowing()
        {
            _throwDiceButton.SetPressed();
        }

        public void ReactToDiceResult(int[] diceResult)
        {
            if (diceResult is null) throw new ArgumentNullException(nameof(diceResult));

            _diceResultMessagesManager.PublishDiceResultMessage(diceResult);
            _throwDiceButton.SetReleased();
        }
    }
}

