using UnityEngine;

namespace DiceDemo.Gameplay
{
    public readonly struct RandomImpulse
    {
        public RandomImpulse(Quaternion rotation, Vector3 torque, Vector3 deviation, float thrust)
        {
            Rotation = rotation;
            Torque = torque;
            Deviation = deviation;
            Thrust = thrust;
        }

        public Quaternion Rotation { get; }
        public Vector3 Torque { get; }
        public Vector3 Deviation { get; }
        public float Thrust { get; }
    }
}
