using System;
using System.Collections.Generic;
using UnityEngine;

namespace DiceDemo.Gameplay
{
    public class DiceHandler : MonoBehaviour
    {
        private DiceFactory _diceFactory;
        private List<Die> _existingDice;

        public void Init(Action<int[]> processDiceResult)
        {
            if (processDiceResult is null) throw new ArgumentNullException(nameof(processDiceResult));

            _diceFactory = GetComponent<DiceFactory>();
            _diceFactory.Init(new ResultRecorder(processDiceResult));
            _existingDice = new List<Die>();
        }

        public void InitiateDiceThrowing()
        {
            DestroyExistingDice();
            SpawnNewDice();
        }

        private void DestroyExistingDice()
        {
            foreach (Die die in _existingDice)
            {
                Destroy(die.gameObject);
            }
        }

        private async void SpawnNewDice()
        {
            _existingDice = await _diceFactory.CreateDiceSetAsync(transform);
        }
    }
}
