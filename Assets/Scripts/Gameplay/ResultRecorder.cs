using System;

namespace DiceDemo.Gameplay
{
    public class ResultRecorder
    {
        private int[] _diceResult;
        private int _counter;

        private readonly Action<int[]> _processDiceResult;

        public ResultRecorder(Action<int[]> processDiceResult)
        {
            _processDiceResult = processDiceResult ?? throw new ArgumentNullException(nameof(processDiceResult));
        }

        public void Reset(int diceSetLength)
        {
            if (diceSetLength == 0) throw new ArgumentOutOfRangeException(nameof(diceSetLength), $"Must be greater than zero.");

            _diceResult = new int[diceSetLength];
            _counter = diceSetLength;
        }

        public void AddDieResultByIndex(int dieIndex, int dieResult)
        {
            if (_counter > 0)
            {
                _diceResult[dieIndex] = dieResult;
                _counter--;

                if (_counter == 0)
                {
                    _processDiceResult(_diceResult);
                }

                return;
            }

            throw new ArgumentOutOfRangeException(nameof(_counter), $"{nameof(_diceResult)} already recorded.");
        }
    }
}
