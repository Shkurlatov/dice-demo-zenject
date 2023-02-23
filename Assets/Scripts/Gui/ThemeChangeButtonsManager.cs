using System.Collections.Generic;
using Zenject;

namespace DiceDemo.Gui
{
    public class ThemeChangeButtonsManager : IInitializable
    {
        private readonly List<string> _themeNames;
        private readonly ThemeChangeButton.Factory _themeChangeButtonFactory;
        private readonly List<ThemeChangeButton> _themeChangeButtons = new();

        public ThemeChangeButtonsManager(List<string> themeNames, ThemeChangeButton.Factory themeChangeButtonFactory)
        {
            _themeNames = themeNames;
            _themeChangeButtonFactory = themeChangeButtonFactory;
        }

        public void Initialize()
        {
            for (int i = 0; i < _themeNames.Count; i++)
            {
                ThemeChangeButton themeChangeButton = _themeChangeButtonFactory.Create(_themeNames[i], i);
                themeChangeButton.Position = themeChangeButton.StartPosition + (themeChangeButton.Offset * i);
                _themeChangeButtons.Add(themeChangeButton);
            }
        }

        public void ChangeIndication(int currentThemeIndex)
        {
            foreach (ThemeChangeButton themeChangeButton in _themeChangeButtons)
            {
                themeChangeButton.UpdateCondition(currentThemeIndex);
            }
        }
    }
}
