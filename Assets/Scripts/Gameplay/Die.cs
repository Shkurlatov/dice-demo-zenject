using System;
using UnityEngine;

namespace DiceDemo.Gameplay
{
    public class Die : MonoBehaviour
    {
        [SerializeField] private Transform[] _sides;
        [SerializeField] private int[] _values;

        private int _index;
        private Rigidbody _rigidbody;
        private ResultRecorder _resultRecorder;
        private DieState _state;

        void Awake()
        {
            if (_sides.Length == 0) throw new UnassignedReferenceException($"No one {typeof(Transform)} has been assigned to {nameof(_sides)}");
            if (_sides.Length != _values.Length) throw new UnassignedReferenceException($"Count of {nameof(_values)} is not equal to count of {nameof(_sides)}");

            _state = DieState.Falling;
        }

        void FixedUpdate()
        {
            if (_state == DieState.Rolling)
            {
                if (_rigidbody.velocity.sqrMagnitude == 0.0f)
                {
                    _state = DieState.Stopped;
                    _resultRecorder.AddDieResultByIndex(_index, GetResult());
                }
            }
        }

        void OnCollisionEnter()
        {
            if (_state == DieState.Falling)
            {
                _state = DieState.Rolling;

                if (_index == 0)
                {
                    GlobalAudio.PlayDiceRollingClip();
                }
            }
        }

        public void Init(int index, RandomImpulse impulse, ResultRecorder recorder)
        {
            _index = index;

            transform.rotation = impulse.Rotation;

            Vector3 direction = Vector3.zero - transform.position;
            Vector3 force = direction * impulse.Thrust + impulse.Deviation;
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.AddTorque(impulse.Torque, ForceMode.Impulse);
            _rigidbody.AddForce(force, ForceMode.Impulse);

            _resultRecorder = recorder ?? throw new ArgumentNullException(nameof(recorder));
        }

        private int GetResult()
        {
            int result = 0;
            float maxSideHeight = 0.0f;

            for (int i = 0; i < _sides.Length; i++)
            {
                if (_sides[i].position.y > maxSideHeight)
                {
                    maxSideHeight = _sides[i].position.y;
                    result = _values[i];
                }
            }

            return result;
        }
    }
}
