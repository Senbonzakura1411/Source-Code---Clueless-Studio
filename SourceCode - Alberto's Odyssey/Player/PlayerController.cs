using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform playerInputSpace;

        [SerializeField, Range(0f, 100f)] private float maxWalkSpeed = 10f, maxRunSpeed = 15f;

        [SerializeField, Range(0f, 100f)] private float maxAcceleration = 10f, maxAirAcceleration = 1f;

        [SerializeField] private bool canRun, canJump;
    
        [SerializeField, Range(0f, 10f)] private float jumpHeight = 2f;

        [SerializeField, Range(0, 5)] private int maxAirJumps;

        [SerializeField, Range(0, 90)] private float maxGroundAngle = 25f;


        private Rigidbody _rigidbody;

        private Animator _animator;

        private Vector3 _velocity, _desiredVelocity, _contactNormal;

        private bool _desiredJump;

        private int _groundContactCount;

        private bool OnGround => _groundContactCount > 0;

        private int _jumpPhase;

        private float _speed, _minGroundDotProduct;

        #region Animator Indexes

        private static readonly int VelX = Animator.StringToHash("VelX");
        private static readonly int VelY = Animator.StringToHash("VelY");
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");

        #endregion

        private void OnValidate()
        {
            _minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            OnValidate();
        }

        private void Start() => _speed = maxWalkSpeed;

        private void Update()
        {
            Vector2 playerInput;
            playerInput.x = Input.GetAxisRaw("Horizontal");
            playerInput.y = Input.GetAxisRaw("Vertical");
            playerInput = Vector2.ClampMagnitude(playerInput, 1f);

            // Sprint stuff
            if (canRun && Input.GetKey(KeyCode.LeftShift) && playerInput.y > 0f && OnGround)
            {
                _animator.SetBool(IsRunning, true);
                _speed = maxRunSpeed;
            }
            else
            {
                _animator.SetBool(IsRunning, false);
                _speed = maxWalkSpeed;
            }
        
       
        
            if (playerInputSpace)
            {
                Vector3 forward = -playerInputSpace.forward;
                forward.y = 0f;
                forward.Normalize();
                Vector3 right = -playerInputSpace.right;
                right.y = 0f;
                right.Normalize();
                _desiredVelocity =
                    (forward * playerInput.y + right * playerInput.x) * _speed;
            }
            else
                _desiredVelocity =
                    new Vector3(playerInput.x, 0f, playerInput.y) * _speed;
        
            // Animator stuff
            _animator.SetFloat(VelX, Vector3.Dot(_velocity, ProjectOnContactPlane(transform.right).normalized));
            _animator.SetFloat(VelY, Vector3.Dot(_velocity, ProjectOnContactPlane(transform.forward).normalized));
        
            if (canJump)
            {
                _desiredJump |= Input.GetButtonDown("Jump");
            }
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