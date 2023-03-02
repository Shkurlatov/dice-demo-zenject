using System.Collections.Generic;
using UnityEngine;

namespace DiceDemo.System
{
    public class MockGameData
    {
        private const string _themeKey = "Theme";

        private readonly List<int[]> _diceResults;

        public MockGameData()
        {
            _diceResults = new List<int[]>();
        }

        public void AddDiceResult(int[] diceResult)
        {
            _diceResults.Add(diceResult);
        }

        public void SaveThemeIndex(int themeIndex)
        {
            PlayerPrefs.SetInt(_themeKey, themeIndex);
            PlayerPrefs.Save();
        }

        public int LoadThemeIndex()
        {
            return PlayerPrefs.GetInt(_themeKey);
        }
    }
}
