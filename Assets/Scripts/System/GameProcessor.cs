using Zenject;
using DiceDemo.Scenery;
using DiceDemo.Gameplay;
using DiceDemo.Gui;
using DiceDemo.Signals;

namespace DiceDemo.System
{
    public class GameProcessor : IInitializable
    {
        private readonly Environment _environment;
        private readonly DiceManager _diceManager;
        private readonly GuiHandler _guiHandler;
        private readonly MockGameData _mockGameData;
        private readonly AudioPlayer _audioPlayer;

        private bool _isWaitingDiceResult = false;

        public GameProcessor(
            Environment environment, 
            DiceManager diceController, 
            GuiHandler guiHandler, 
            MockGameData mockGameData,
            AudioPlayer audioPlayer)
        {
            _environment = environment;
            _diceManager = diceController;
            _guiHandler = guiHandler;
            _mockGameData = mockGameData;
            _audioPlayer = audioPlayer;
        }

        public void Initialize()
        {
            SetEnvironmentTheme(_mockGameData.LoadThemeIndex());
        }

        public void OnThrowDiceCommand()
        {
            if (_isWaitingDiceResult)
            {
                return;
            }

            _isWaitingDiceResult = true;
            _guiHandler.ReactToDiceThrowing();
            _diceManager.ThrowDice();
            _audioPlayer.PlayDiceShakingSound();
        }
        
        public void OnDiceTouchSurface()
        {
            _audioPlayer.PlayDiceRollingSound();
        }

        public void OnDiceResult(DiceResultSignal diceResultSignal)
        {
            _mockGameData.AddDiceResult(diceResultSignal.DiceResult);
            _guiHandler.ReactToDiceResult(diceResultSignal.DiceResult);
            _isWaitingDiceResult = false;
        }

        public void OnThemeChangeCommand(ThemeChangeSignal themeChangeSignal)
        {
            _audioPlayer.PlayButtonClickSound();

            if (_environment.CurrentThemeIndex != themeChangeSignal.ThemeIndex)
            {
                SetEnvironmentTheme(themeChangeSignal.ThemeIndex);
                _mockGameData.SaveThemeIndex(themeChangeSignal.ThemeIndex);
            }
        }

        private void SetEnvironmentTheme(int themeIndex)
        {
            _environment.SetTheme(themeIndex);
            _guiHandler.ReactToThemeChange(themeIndex);
        }
    }
}
