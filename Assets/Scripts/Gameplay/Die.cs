using UnityEngine;

namespace DiceDemo.Gameplay
{
    public class Die : MonoBehaviour
    {
        [SerializeField] private Transform[] _sides;
        [SerializeField] private int[] _values;

        private Rigidbody _rigidbody;
        private bool _isTouchSurface;

        public Vector3 Position
        {
            get { return transform.position; }
            set { transform.position = value; }
        }

        public Quaternion Rotation
        {
            set { transform.rotation = value; }
        }

        public Vector3 Torque
        {
            set { _rigidbody.AddTorque(value, ForceMode.Impulse); }
        }
        
        public Vector3 Force
        {
            set { _rigidbody.AddForce(value, ForceMode.Impulse); }
        }

        public float Magnitude
        {
            get { return _rigidbody.velocity.sqrMagnitude; }
        }

        public bool IsTouchSurface
        {
            get { return _isTouchSurface; }
            set { _isTouchSurface = value; }
        }

        void Awake()
        {
            if (_sides.Length == 0) throw new UnassignedReferenceException($"No one {typeof(Transform)} has been assigned to {nameof(_sides)}");
            if (_sides.Length != _values.Length) throw new UnassignedReferenceException($"Count of {nameof(_values)} is not equal to count of {nameof(_sides)}");

            _rigidbody = GetComponent<Rigidbody>();
            gameObject.SetActive(false);
        }

        void OnCollisionEnter(Collision collision)
        {
            if (_isTouchSurface)
            {
                return;
            }

            if (collision.gameObject.name == "Surface")
            {
                _isTouchSurface = true;
            }
        }

        public int GetResult()
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
