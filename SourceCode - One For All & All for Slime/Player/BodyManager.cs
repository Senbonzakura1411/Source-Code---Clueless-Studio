using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyManager : MonoBehaviour
{
    [SerializeField]
    [Header("Stats")]
    public PlayerStats playerStats;

    [SerializeField]
    [Header("Animator")]
    public Animator anim;

    [SerializeField]
    [Header("PlayerManager")]
    public PlayerManager playerManager;

    [SerializeField]
    [Header("Fisicas")]
    public Rigidbody2D rB2;
    public bool standStill;
    public float jumpForce;

    [SerializeField]
    [Header("CheckGround")]
    public bool isGrounded;
    public Transform feetPos;
    public float feetRange;
    public LayerMask whatIsGround;
    public float groundedRememberTime;
    public float groundedRemember;

    [SerializeField]
    [Header("Jump")]
    public float jumpTime;
    public float fallMultiplier;
    public float jumpPessedRemberTime;
    public float jumpPressedRemember;
    public bool canPressJump;
    public float cutJumpValue;

    [SerializeField]
    [Header("IFrames")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public bool canGetHit;

    [SerializeField]
    [Header("Sprite Renderer")]
    public SpriteRenderer mySprite;


    public bool isIdle;
    public bool isWalk;
    public bool isJump;
    public bool isFalling;

    public void Start()
    {
        rB2 = gameObject.GetComponent<Rigidbody2D>();
        mySprite = gameObject.GetComponent<SpriteRenderer>();
        canGetHit = true;
    }

    public void Update()
    {
        SetAnimations();
        CheckIfIsGrounded();
        if (playerManager.playerIsSplit)
        {
            if (playerManager.controlHead == false)
            {
                if (rB2.bodyType == RigidbodyType2D.Dynamic)
                {
                    rB2.bodyType = RigidbodyType2D.Dynamic;
                }
                if (playerManager.canMove)
                {
                    WalkingMovement();
                    DirectionLooking();
                    Falling();
                }
            }
            else
            {
                SetIdleAnim();
                if (isGrounded)
                {
                    rB2.bodyType = RigidbodyType2D.Dynamic;
                }
                else
                {
                    if (rB2.bodyType == RigidbodyType2D.Dynamic)
                    {
                        rB2.bodyType = RigidbodyType2D.Dynamic;
                    }
                }
            }
        }
    }

    public void FixedUpdate()
    {
        if (playerManager.playerIsSplit)
        {
            if (playerManager.controlHead == false)
            {
                if (playerManager.canMove)
                {
                    WalkingMovement();
                    Jump();
                }

            }
        }
    }

    #region Mechanics
    public void WalkingMovement()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        if (moveHorizontal != 0)
        {
            standStill = false;


            if (isGrounded)
            {
                SetWalkAnim();
            }
            if (moveHorizontal > 0)
            {
                playerManager.lookingRight = true;
                rB2.velocity = new Vector3(playerStats.bodySpeed, rB2.velocity.y);
            }
            if (moveHorizontal < 0)
            {
                playerManager.lookingRight = false;
                rB2.velocity = new Vector3(-playerStats.bodySpeed, rB2.velocity.y);
            }
        }
        else if (moveHorizontal == 0)
        {
            if (!standStill)
            {
                rB2.velocity = new Vector3(0, rB2.velocity.y);
                /* if (!atacking)
                     standStill = true;*/
            }
            if (isGrounded)
            {
                SetIdleAnim();
            }
        }
    }

    public void Jump()
    {
        if (jumpPressedRemember > 0)
        {
            jumpPressedRemember -= Time.deltaTime;
        }
        if (Input.GetAxis("Jump") > 0 && canPressJump)
        {
            canPressJump = false;
            jumpPressedRemember = jumpPessedRemberTime;
        }
        if (Input.GetAxis("Jump") <= 0)
        {
            //jumping = false;
            canPressJump = true;
            if (rB2.velocity.y > 0)
            {
                rB2.velocity = new Vector2(rB2.velocity.x, rB2.velocity.y * cutJumpValue);
            }
        }
        if (rB2.velocity.y < 0)
        {
            //jumping = false;
        }
        if ((jumpPressedRemember > 0) && (groundedRemember > 0))
        {
            jumpPressedRemember = 0;
            groundedRemember = 0;
            //jumping = true;
            rB2.AddForce(new Vector2(0, playerStats.bodyJumpForce));
            SetJumpAnim();
        }


    }

    public void Falling()
    {
        if (!isGrounded)
        {
            SetFallingAnim();
        }
    }
    #endregion

    #region states
    public void DirectionLooking()
    {
        if (playerManager.lookingRight)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void CheckIfIsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, feetRange, whatIsGround);

        if (groundedRemember > 0)
        {
            groundedRemember -= Time.deltaTime;
        }
        if (isGrounded)
        {
            groundedRemember = groundedRememberTime;
        }
    }

    public void OnHit(int damage)
    {
        if (canGetHit)
        {
            canGetHit = false;
            playerStats.health -= damage;
            AudioManager.instance.Play("PADmgd");
            StartCoroutine(IFramesFlash());

        }
    }
    #endregion

    #region Collisions
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            playerManager.lM.lastCheckPointBodyPlayer = other.gameObject.transform.position;
        }
        if (other.gameObject.CompareTag("HitBoxRepos"))
        {
            OnHit(1);
            playerManager.canMove = false;
            playerManager.lM.ActiveRepositionPanel();
            StartCoroutine(SetDamageReposition());
        }
        if (other.gameObject.CompareTag("Fire"))
        {
            OnHit(1);
            playerManager.canMove = false;
            playerManager.lM.ActiveRepositionPanel();
            StartCoroutine(SetDamageReposition());
        }
        if (other.gameObject.CompareTag("Tornado"))
        {
            playerStats.bodyJumpForce = playerStats.bodyJumpForce * 1.5f;
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {

    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tornado"))
        {
            playerStats.bodyJumpForce = playerStats.bodyBaseJumpForce;
        }
    }
    #endregion

    #region  Enumerators
    public IEnumerator SetDamageReposition()
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = playerManager.lM.lastCheckPointBodyPlayer;
    }

    private IEnumerator IFramesFlash()
    {
        int temp = 0;
        while (temp < numberOfFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        canGetHit = true;
    }
    #endregion

    #region animations
    public void SetIdleAnim()
    {
        isIdle = true;
        isWalk = false;
        isJump = false;
        isFalling = false;
    }

    public void SetWalkAnim()
    {
        isIdle = false;
        isWalk = true;
        isJump = false;
        isFalling = false;
    }

    public void SetJumpAnim()
    {
        isIdle = false;
        isWalk = false;
        isJump = true;
        isFalling = false;
    }

    public void SetFallingAnim()
    {
        isIdle = false;
        isWalk = false;
        isJump = false;
        isFalling = true;
    }

    public void SetAnimations()
    {
        anim.SetBool("Idle", isIdle);
        anim.SetBool("Walk", isWalk);
        anim.SetBool("Jump", isJump);
        anim.SetBool("Falling", isFalling);
    }
    #endregion

    #region SoundEvents
    void PlayArmorWalkSound()
    {
        AudioManager.instance.Play("PAWalking");
    }
    void PlayArmorJumpSound()
    {
        AudioManager.instance.Play("PAJump");
    }
    void PlayArmorDeathSound()
    {
        AudioManager.instance.Play("PADeath");
    }
    #endregion
}
