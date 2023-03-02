using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DiceDemo.Gameplay
{
    public class ImpulseGenerator
    {
        private readonly Settings _settings;

        public ImpulseGenerator(Settings settings)
        {
            _settings = settings;
            Random.InitState((int)DateTime.UtcNow.Ticks);
        }

        public RandomImpulse GenerateRandomImpulse()
        {
            Quaternion rotation = Random.rotation;
            Vector3 torque = Random.onUnitSphere * Random.Range(_settings.MinTorque, _settings.MaxTorque);
            Vector3 deviation = Random.onUnitSphere * Random.Range(_settings.MinDeviation, _settings.MaxDeviation);
            float thrust = Random.Range(_settings.MinThrust, _settings.MaxThrust);

            return new RandomImpulse(rotation, torque, deviation, thrust);
        }

        [Serializable]
        public class Settings
        {
            public float MinTorque;
            public float MaxTorque;

            public float MinDeviation;
            public float MaxDeviation;

            public float MinThrust;
            public float MaxThrust;
        }
    }
}
