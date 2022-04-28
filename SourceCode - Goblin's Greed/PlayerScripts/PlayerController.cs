using UnityEngine;

namespace PlayerScripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform playerInputSpace;

        [SerializeField, Range(0f, 100f)] private float maxWalkSpeed = 10f, maxRunSpeed = 15f, maxHasteSpeed = 25f;

        [SerializeField, Range(0f, 100f)] private float maxAcceleration = 10f, maxAirAcceleration = 1f;

        [SerializeField, Range(0f, 10f)] private float jumpHeight = 2f;

        [SerializeField, Range(0, 5)] private int maxAirJumps;

        [SerializeField, Range(0, 90)] private float maxGroundAngle = 25f;

        public bool IsHasting { get; set; }
        public bool OnGround => _groundContactCount > 0;

        private PlayerInput _input;

        private Rigidbody _rigidbody;

        private Animator _animator;

        private Vector3 _velocity, _desiredVelocity, _contactNormal;

        private bool _desiredJump;

        private int _groundContactCount;

        

        private int _jumpPhase;

        private float _speed, _minGroundDotProduct;

        #region Animator Indexes

        private static readonly int VelX = Animator.StringToHash("VelX");
        private static readonly int VelY = Animator.StringToHash("VelY");
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");
        private static readonly int IsHaste = Animator.StringToHash("IsHaste");

        #endregion

        private void OnValidate()
        {
            _minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
        }

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            OnValidate();
        }

        private void Start() => _speed = maxWalkSpeed;

        private void Update()
        {
            var playerInput = Vector2.ClampMagnitude(_input.InputVector, 1f);

            // Sprint & Animator stuff
            CheckSprint(playerInput);

            _animator.SetFloat(VelX, Input.GetAxisRaw("Horizontal"));
            _animator.SetFloat(VelY, Input.GetAxisRaw("Vertical"));


            if (playerInputSpace)
            {
                Vector3 forward = playerInputSpace.forward;
                forward.y = 0f;
                forward.Normalize();
                Vector3 right = playerInputSpace.right;
                right.y = 0f;
                right.Normalize();
                _desiredVelocity =
                    (forward * playerInput.y + right * playerInput.x) * _speed;

                // RigidBody rotation
                var startRotation = transform.rotation;
                var finalRotation = Quaternion.Lerp(startRotation, Quaternion.LookRotation(forward), 1f);
                _rigidbody.MoveRotation(finalRotation);
            }
            else
                _desiredVelocity =
                    new Vector3(playerInput.x, 0f, playerInput.y) * _speed;

            _desiredJump |= _input.JumpInput;
        }


        private void FixedUpdate()
        {
            UpdateState();
            AdjustVelocity();

            if (_desiredJump)
            {
                _desiredJump = false;
                Jump();
            }

            _rigidbody.velocity = _velocity;
            ClearState();
        }

        private void CheckSprint(Vector2 playerInput)
        {
            if (IsHasting)
            {
                _animator.SetBool(IsHaste, true);
                _animator.SetBool(IsRunning, false);
                _speed = maxHasteSpeed;
            }
            else if (_input.RunInput && playerInput.y > 0f && OnGround)
            {
                _animator.SetBool(IsRunning, true);
                _animator.SetBool(IsHaste, false);
                _speed = maxRunSpeed;
            }
            else
            {
                _animator.SetBool(IsHaste, false);
                _animator.SetBool(IsRunning, false);
                _speed = maxWalkSpeed;
            }
        }

        private void ClearState()
        {
            _groundContactCount = 0;
            _contactNormal = Vector3.zero;
        }

        private void UpdateState()
        {
            _velocity = _rigidbody.velocity;
            if (OnGround)
            {
                _animator.SetBool(IsJumping, false);
                _jumpPhase = 0;
                if (_groundContactCount > 1)
                {
                    _contactNormal.Normalize();
                }
            }
            else
            {
                _contactNormal = Vector3.up;
            }
        }

        private void AdjustVelocity()
        {
            Vector3 xAxis = ProjectOnContactPlane(Vector3.right).normalized;
            Vector3 zAxis = ProjectOnContactPlane(Vector3.forward).normalized;

            float currentX = Vector3.Dot(_velocity, xAxis);
            float currentZ = Vector3.Dot(_velocity, zAxis);

            float acceleration = OnGround ? maxAcceleration : maxAirAcceleration;
            float maxSpeedChange = acceleration * Time.deltaTime;

            float newX =
                Mathf.MoveTowards(currentX, _desiredVelocity.x, maxSpeedChange);
            float newZ =
                Mathf.MoveTowards(currentZ, _desiredVelocity.z, maxSpeedChange);

            _velocity += xAxis * (newX - currentX) + zAxis * (newZ - currentZ);
        }

        private void Jump()
        {
            if (OnGround || _jumpPhase < maxAirJumps)
            {
                _animator.SetBool(IsJumping, true);
                float jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
                float alignedSpeed = Vector3.Dot(_velocity, _contactNormal);
                if (alignedSpeed > 0f)
                {
                    jumpSpeed = Mathf.Max(jumpSpeed - alignedSpeed, 0f);
                }

                _velocity += _contactNormal * jumpSpeed;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            EvaluateCollision(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            EvaluateCollision(collision);
        }

        private void EvaluateCollision(Collision collision)
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                Vector3 normal = collision.GetContact(i).normal;
                if (normal.y >= _minGroundDotProduct)
                {
                    _groundContactCount += 1;
                    _contactNormal += normal;
                }
            }
        }

        private Vector3 ProjectOnContactPlane(Vector3 vector)
        {
            return vector - _contactNormal * Vector3.Dot(vector, _contactNormal);
        }
    }
}