using System;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace DiceDemo.Gameplay
{
    public class DiceSpawner
    {
        private readonly Settings _settings;
        private readonly Vector3 _spawnPoint;
        private readonly ImpulseGenerator _impulseGenerator;
        private readonly List<Die> _diceSet;

        public DiceSpawner(Settings settings, Vector3 spawnPoint, ImpulseGenerator impulseGenerator, List<Die> diceSet)
        {
            _settings = settings;
            _spawnPoint = spawnPoint;
            _impulseGenerator = impulseGenerator;
            _diceSet = diceSet;
        }

        public async UniTask RespawnDiceAsync()
        {
            foreach (Die die in _diceSet)
            {
                die.gameObject.SetActive(false);
                die.Position = _spawnPoint;
                die.IsTouchSurface = false;
            }

            await UniTask.Delay(_settings.BeforeSpawnDelay);

            foreach (Die die in _diceSet)
            {
                await UniTask.Delay(_settings.BetweenSpawnDelay);

                die.gameObject.SetActive(true);
                die.ApplyImpulse(_impulseGenerator.GenerateRandomImpulse());
            }
        }

        [Serializable]
        public class Settings
        {
            public int BeforeSpawnDelay;
            public int BetweenSpawnDelay;
        }
    }
}
