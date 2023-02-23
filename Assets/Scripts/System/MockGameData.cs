using System.Collections.Generic;
using UnityEngine;

namespace DiceDemo.System
{
    public class MockGameData
    {
        const string ThemeKey = "Theme";

        private List<int[]> _diceResults;

        public MockGameData()
        {
            _diceResults = new List<int[]>();
        }

        public void AddDiceResult(int[] diceResult)
        {
            _diceResults.Add(diceResult);
        }

        public void SaveThemeType(ThemeType themeType)
        {
            PlayerPrefs.SetInt(ThemeKey, (int)themeType);
            PlayerPrefs.Save();
        }

        public ThemeType LoadThemeType()
        {
            return (ThemeType)PlayerPrefs.GetInt(ThemeKey, 0);
        }
    }
}
