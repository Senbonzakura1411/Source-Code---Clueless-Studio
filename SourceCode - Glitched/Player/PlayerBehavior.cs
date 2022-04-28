using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Feedbacks;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float feetRange;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float groundedRememberTime;
    [SerializeField] private float jumpPessedRemberTime;
    [SerializeField] public float baseSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float cooldownTime;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float baseGravity;

    private Collider2D _collider;
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRender;
    private Animator _anim;
    private GameControl _gC;
    private bool _isGrounded;
    public float _speed;
    public bool _canFlash;
    public bool _canJump;

    private float groundedRemember;
    private float jumpPressedRemember;
    private bool canPressJump;

    public float coolDown;

    private bool isRun;
    private bool isJump;

    public bool isInAir;

    [Header("Feedbacks")]
    // MMFeedbacks to play when the character starts flashing
    public MMFeedbacks FlashFeedback;

    // MMFeedbacks to play when the character lands after a jump
    public MMFeedbacks LandingFeedback;
    
    // MMFeedbacks to play when the character lands after a dies
    public MMFeedbacks DeathFeedback;

    private void Start()
    {
        _gC = GameControl.GetInstance();
        transform.position = _gC.startPos;
        _collider = gameObject.GetComponent<Collider2D>();
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
        _spriteRender = gameObject.GetComponentInChildren<SpriteRenderer>();
        _anim = gameObject.GetComponent<Animator>();
        _gC.intentos++;
        coolDown = cooldownTime;
    }

    private void Update()
    {
        SetAnimations();
        CheckIfIsGrounded();
        Jump();
        Flash();
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        _rigidBody.velocity = new Vector2(_speed, _rigidBody.velocity.y);
    }

    private void Jump()
    {
        if (jumpPressedRemember > 0)
        {
            jumpPressedRemember -= Time.deltaTime;
        }

        if (Input.GetAxisRaw("Jump") > 0 && canPressJump)
        {
            canPressJump = false;
            jumpPressedRemember = jumpPessedRemberTime;
        }

        if (Input.GetAxisRaw("Jump") <= 0)
        {
            canPressJump = true;
        }

        if ((jumpPressedRemember > 0) && (groundedRemember > 0))
        {
            jumpPressedRemember = 0;
            groundedRemember = 0;
            if (_canJump)
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpForce);
            }

        }
    }

    private void Flash()
    {
        if (_canFlash)
        {
            if (coolDown < cooldownTime)
            {
                coolDown += Time.deltaTime;
            }
            else
            {
                if (Input.GetMouseButtonDown(1))
                {
                    StartCoroutine(FlashPlayer());
                    FlashFeedback?.PlayFeedbacks();
                    _speed += speedMultiplier;
                    coolDown = 0;
                }
            }
        }
    }

    private IEnumerator FlashPlayer()
    {
        _collider.enabled = false;
        _rigidBody.AddForce(new Vector2(dashSpeed, 0), ForceMode2D.Force);
        _rigidBody.gravityScale = 0;
        yield return new WaitForSeconds(0.05f);
        _collider.enabled = true;
        _rigidBody.gravityScale = baseGravity;
    }

    private void CheckIfIsGrounded()
    {
        _isGrounded = Physics2D.OverlapCircle(feetPos.position, feetRange, whatIsGround);

        if (groundedRemember > 0)
        {
            groundedRemember -= Time.deltaTime;
        }

        if (_isGrounded)
        {
            SetRun();
            groundedRemember = groundedRememberTime;
            if (isInAir)
            {
                isInAir = false;
                LandingFeedback?.PlayFeedbacks();
            }
        }
        else
        {
            SetJump();
            isInAir = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            PlayerDeath();
        }

        if (other.gameObject.CompareTag("CheckPoint"))
        {
            _gC.startPos.x = other.gameObject.transform.position.x;
            _gC.imNewGame = false;
            _gC.playerSpeed = _speed;
        }
    }

    private void PlayerDeath()
    {
        DeathFeedback?.PlayFeedbacks();
        StartCoroutine(CallDeath());
    }

    private IEnumerator CallDeath()
    {
        _speed = 0;
        _canFlash = false;
        _canJump = false;
        _rigidBody.bodyType = RigidbodyType2D.Static;
        _spriteRender.color = Color.red;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Game");
    }

    private void SetAnimations()
    {
        _anim.SetBool("Run", isRun);
        _anim.SetBool("Jump", isJump);
    }

    public void SetRun()
    {
        isRun = true;
        isJump = false;
    }

    public void SetJump()
    {
        isRun = false;
        isJump = true;
    }
}