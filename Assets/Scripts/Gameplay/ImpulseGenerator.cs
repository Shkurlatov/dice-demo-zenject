using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DiceDemo.Gameplay
{
    public class ImpulseGenerator
    {
        private const float _minTorque = 0.01f;
        private const float _maxTorque = 0.02f;

        private const float _minDeviation = 0.1f;
        private const float _maxDeviation = 0.2f;

        private const float _minThrust = 5.6f;
        private const float _maxThrust = 6.4f;

        public ImpulseGenerator()
        {
            Random.InitState((int)DateTime.UtcNow.Ticks);
        }

        public RandomImpulse GenerateRandomImpulse()
        {
            Quaternion rotation = Random.rotation;
            Vector3 torque = Random.onUnitSphere * Random.Range(_minTorque, _maxTorque);
            Vector3 deviation = Random.onUnitSphere * Random.Range(_minDeviation, _maxDeviation);
            float thrust = Random.Range(_minThrust, _maxThrust);

            return new RandomImpulse(rotation, torque, deviation, thrust);
        }
    }
}
