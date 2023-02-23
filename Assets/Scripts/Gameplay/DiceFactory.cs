using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DiceDemo.Gameplay
{
    public class DiceFactory : MonoBehaviour
    {
        [SerializeField] private Die[] _diceSet;

        private const int _beforeSpawnDelay = 1000;
        private const int _betweenSpawnDelay = 50;

        private int _diceSetLength;
        private ImpulseGenerator _impulseGenerator;
        private RandomImpulse[] _randomImpulses;
        private ResultRecorder _resultRecorder;

        void Awake()
        {           
            if (_diceSet.Length == 0) throw new UnassignedReferenceException($"No one {typeof(Die)} has been assigned to {nameof(_diceSet)}");
        }

        public void Init(ResultRecorder recorder)
        {
            _diceSetLength = _diceSet.Length;
            _impulseGenerator = new ImpulseGenerator();
            _randomImpulses = new RandomImpulse[_diceSetLength];
            _resultRecorder = recorder ?? throw new ArgumentNullException(nameof(recorder));
        }

        public async UniTask<List<Die>> CreateDiceSetAsync(Transform spawnPoint)
        {
            if (spawnPoint is null) throw new ArgumentNullException(nameof(spawnPoint));

            List<Die> dice = new();
            _resultRecorder.Reset(_diceSetLength);
            UpdateRandomImpulses();

            await UniTask.Delay(_beforeSpawnDelay);

            for (int i = 0; i < _diceSetLength; i++)
            {
                Die die = Instantiate(_diceSet[i], spawnPoint);
                die.Init(i, _randomImpulses[i], _resultRecorder);
                dice.Add(die);

                await UniTask.Delay(_betweenSpawnDelay);
            }

            return dice;
        }

        private void UpdateRandomImpulses()
        {
            for (int i = 0; i < _diceSetLength; i++)
            {
                _randomImpulses[i] = _impulseGenerator.GenerateRandomImpulse();
            }
        }
    }
}
