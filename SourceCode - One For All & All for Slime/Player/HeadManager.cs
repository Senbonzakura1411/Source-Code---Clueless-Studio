using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadManager : MonoBehaviour
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
    private bool separation;


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

    [SerializeField]
    [Header("Slime")]
    public int currentSlime;
    public GameObject tornadoAir;

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
        if (playerManager.playerIsSplit)
        {
            if (playerManager.controlHead)
            {
                if (playerManager.canMove)
                {

                    DirectionLooking();
                    CheckIfIsGrounded();
                    SeparationForce();
                    SlimeAbility();
                    Falling();
                }

            }
            else
            {
                rB2.velocity = new Vector2(0,rB2.velocity.y);
                SetIdleAnim();
            }
        }
    }

    public void FixedUpdate()
    {
        if (playerManager.playerIsSplit)
        {
            if (playerManager.controlHead)
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
            /*if (isGrounded)
            {
                if (!atacking)
                {
                    SetRunning();
                }
                if (recovering)
                {
                    recovering = false;
                }

            }*/
            if (isGrounded)
            {
                SetWalkAnim();
            }
            if (moveHorizontal > 0)
            {
                playerManager.lookingRight = true;
                rB2.velocity = new Vector3(playerStats.slimeSpeed, rB2.velocity.y);
            }
            if (moveHorizontal < 0)
            {
                playerManager.lookingRight = false;
                rB2.velocity = new Vector3(-playerStats.slimeSpeed, rB2.velocity.y);
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
            rB2.AddForce(new Vector2(0, playerStats.slimeJumpForce));

            SetJumpAnim();
        }
    }

    public void SeparationForce()
    {
        if (separation)
        {
            rB2.velocity = Vector2.zero;
            rB2.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
            if (playerManager.lookingRight)
            {
                rB2.AddForce(Vector2.right * 30, ForceMode2D.Impulse);
            }
            else
            {
                rB2.AddForce(Vector2.left * 30, ForceMode2D.Impulse);
            }
        }

    }

    public void StartSeparation()
    {
        StartCoroutine(InitialForce());
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
            AudioManager.instance.Play("PSDmgd");
            StartCoroutine(IFramesFlash());
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

    #region Collisions
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            playerManager.lM.lastCheckPointHeadPlayer = other.gameObject.transform.position;

        }
        if (other.gameObject.CompareTag("HitBoxRepos"))
        {
            if (other.gameObject.layer == 10)
            {
                if (currentSlime != 0)
                {
                    OnHit(1);
                    playerManager.canMove = false;
                    playerManager.lM.ActiveRepositionPanel();
                    StartCoroutine(SetDamageReposition());
                }
            }
            else
            {
                OnHit(1);
                playerManager.canMove = false;
                playerManager.lM.ActiveRepositionPanel();
                StartCoroutine(SetDamageReposition());
            }

        }
        if (other.gameObject.CompareTag("Tornado"))
        {
            playerStats.slimeJumpForce = playerStats.slimeJumpForce * 2f;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tornado"))
        {
            playerStats.slimeJumpForce = playerStats.slimeBaseJumpForce;
        }
    }
    #endregion

    #region  Enumerators
    public IEnumerator SetDamageReposition()
    {
        yield return new WaitForSeconds(0.5f);
        if (playerManager.lM.lastCheckPointHeadPlayer != playerManager.lM.lastCheckPointBodyPlayer)
        {
            playerManager.lM.lastCheckPointHeadPlayer = playerManager.lM.lastCheckPointBodyPlayer;
            transform.position = playerManager.lM.lastCheckPointHeadPlayer;
        }
        else
        {
            transform.position = playerManager.lM.lastCheckPointHeadPlayer;
        }
        
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

    private IEnumerator InitialForce()
    {
        separation = true;
        yield return new WaitForSeconds(0.1f);
        separation = false;
    }
    #endregion

    #region Individual Slime Ability
    public void SlimeAbility()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            switch (currentSlime)
            {
                case 3:
                    CreateTornado();
                    break;
            }
        }
    }

    public void CreateTornado()
    {
        if (playerManager.lM.tornadoInGame != null)
        {
            Destroy(playerManager.lM.tornadoInGame);
        }
        GameObject tornado = Instantiate(tornadoAir, transform.position, Quaternion.identity);
        playerManager.lM.tornadoInGame = tornado;

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

    #region Sounds
    void PlaySlimeWalkingSound()
    {
        AudioManager.instance.Play("PSWalking");
    }
    void PlaySlimeJumpingSound()
    {
        AudioManager.instance.Play("PSJump");
    }
    void PlaySlimeDeathSound()
    {
        AudioManager.instance.Play("PSDeath");
    }
    
    #endregion
}
