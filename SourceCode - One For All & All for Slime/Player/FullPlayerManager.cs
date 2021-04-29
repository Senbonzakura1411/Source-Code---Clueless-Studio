using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullPlayerManager : MonoBehaviour
{
    [SerializeField]
    [Header("Stats")]
    public PlayerStats playerStats;

    [SerializeField]
    [Header("Animator")]
    public Animator anim;

    [SerializeField]
    [Header("Player Manager")]
    public PlayerManager playerManager;

    [SerializeField]
    [Header("Positions")]
    public Transform headPos;
    public Transform bodyPos;

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
    [Header("Ataque")]
    public float timeBtwnAtack;
    public float startTimeBtwnAtack;
    private bool atacking;
    public bool canAtack;
    public Transform atackPoint;
    public Vector2 atackRange;
    public LayerMask enemyLayers;

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
    [Header("Particle Fx")]
    public GameObject currentParticleElectric;
    public GameObject currentParticleFire;
    public GameObject currentParticleEarth;
    public GameObject electricFx;
    public GameObject fireFx;
    public GameObject earthFx;
    public GameObject earthFx2;

    [SerializeField]
    [Header("Special Atack")]
    public bool usingSpecial;
    public float electricRange;
    public float electricDamage;

    public Transform firePos;
    public Vector2 fireRange;
    public float fireDamage;

    public float earthDamage;
    public Transform earthPos;
    public Vector2 earthRange;
    public Transform earthParticlePos;

    public GameObject airProjectile;
    public float airDamage;

    [SerializeField]
    [Header("Animators")]
    public RuntimeAnimatorController[] animator;

    public bool isIdle;
    public bool isWalk;
    public bool isJump;
    public bool isFalling;
    public bool isAtacking;
    public bool isSpecial;


    public void Start()
    {
        rB2 = gameObject.GetComponent<Rigidbody2D>();
        mySprite = gameObject.GetComponent<SpriteRenderer>();
        canGetHit = true;
    }

    public void Update()
    {
        if (playerManager.canMove)
        {
            CheckIfIsGrounded();
            CheckIfUsingSpecial();
            Atack();
            DirectionLooking();
            ActivateSpecial();
            ElectricSpecialATK();
            FireSpecialATK();
            EarthSpecialATK();
            SetAnimations();
            SetAnimators();
            Falling();
        }
    }

    public void FixedUpdate()
    {
        if (playerManager.canMove)
        {
            WalkingMovement();
            Jump();

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
                if (!atacking)
                {
                    SetWalkAnim();
                }

            }
            if (moveHorizontal > 0)
            {
                playerManager.lookingRight = true;
                rB2.velocity = new Vector3(playerStats.fullSpeed, rB2.velocity.y);
            }
            if (moveHorizontal < 0)
            {
                playerManager.lookingRight = false;
                rB2.velocity = new Vector3(-playerStats.fullSpeed, rB2.velocity.y);
            }
        }
        else if (moveHorizontal == 0)
        {
            if (!standStill)
            {
                rB2.velocity = new Vector3(0, rB2.velocity.y);
                if (!atacking)
                    standStill = true;
            }
            if (isGrounded)
            {
                if (!atacking)
                {
                    SetIdleAnim();
                }
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
            //rB2.velocity = new Vector2(rB2.velocity.x, playerStats.fullJumpForce);
            rB2.AddForce(new Vector2(0, playerStats.fullJumpForce));
            if (!atacking)
            {
                SetJumpAnim();
            }
        }
    }

    public void Atack()
    {
        if (!usingSpecial)
        {
            if (timeBtwnAtack <= 0)
            {
                if (Input.GetAxis("Atack") <= 0)
                {
                    canAtack = true;
                    atacking = false;
                }
                if (Input.GetAxis("Atack") > 0 && canAtack)
                {
                    if (!atacking)
                    {
                        atacking = true;
                    }
                    canAtack = false;
                    timeBtwnAtack = startTimeBtwnAtack;
                }
            }
            else
            {
                if (isAtacking)
                {
                    atacking = false;
                }
                if (atacking)
                {
                    SetAtackAnim();
                    Collider2D[] enemyColliders = Physics2D.OverlapBoxAll(atackPoint.position, atackRange, 0f, enemyLayers);
                    foreach (Collider2D enemy in enemyColliders)
                    {
                        enemy.GetComponent<EnemyStats>().OnHit(10);
                    }
                    atacking = false;
                }
                canAtack = false;
                timeBtwnAtack -= Time.deltaTime;
            }
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

    public void CheckIfUsingSpecial ()
    {
        if (playerStats.usingFire|| playerStats.usingElectric|| playerStats.usingEarth)
        {
            usingSpecial = true;
        }
        else
        {
            usingSpecial = false;
        }
    }

    #endregion

    #region Collisions
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            playerManager.lM.lastCheckPointFullPlayer = other.gameObject.transform.position;
            playerManager.lM.lastCheckPointHeadPlayer = other.gameObject.transform.position;
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
            playerStats.fullJumpForce = playerStats.fullJumpForce * 1.5f;
        }
    }


    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tornado"))
        {
            playerStats.fullJumpForce = playerStats.fullBaseJumpForce;
        }
    }
    #endregion

    #region  Enumerators
    public IEnumerator SetDamageReposition()
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = playerManager.lM.lastCheckPointFullPlayer;
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

    #region Gizmos
    void OnDrawGizmosSelected()
    {
        if (atackPoint == null)
            return;
        Gizmos.DrawWireCube(atackPoint.position, atackRange);
        Gizmos.DrawWireSphere(transform.position, electricRange);
        Gizmos.DrawWireCube(firePos.position, fireRange);
        Gizmos.DrawWireCube(earthPos.position, earthRange);

    }
    #endregion

    #region Special

    public void ActivateSpecial()
    {
        switch (playerManager.lM.currentSlime)
        {
            case 0:
                ActivateFireSpecialATK();
                break;
            case 1:
                ActivateElectricSpecialATK();

                break;
            case 2:
                ActivateEarthSpecialATK();

                break;
            case 3:
                ActivateAirSpecialATK();

                break;
        }

    }
    public void ActivateElectricSpecialATK()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {

            if (playerStats.canUseElectric)
            {
                SetSpecialAnim();
                AudioManager.instance.Play("PAElecF");
                playerStats.electricCounterUse = playerStats.initialElectricCounterUse;
            }
        }
            
    }

    public void ElectricSpecialATK()
    {
        if (playerStats.usingElectric)
        {
            if (currentParticleElectric == null)
            {
                currentParticleElectric = Instantiate(electricFx, transform.position, Quaternion.identity);
                currentParticleElectric.transform.SetParent(this.gameObject.transform);
            }
            Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(transform.position, electricRange, enemyLayers);
            foreach (Collider2D enemy in enemyColliders)
            {
                enemy.GetComponent<EnemyStats>().OnHitElectric(electricDamage);
            }
        }
        else
        {
            if (currentParticleElectric != null)
                Destroy(currentParticleElectric.gameObject);
        }
    }

    public void ActivateFireSpecialATK()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {

            if (playerStats.canUseFire)
            {
                SetSpecialAnim();
                AudioManager.instance.Play("PAFlameT");
                playerStats.fireCounterUse = playerStats.initialFireCounterUse;
            }
        }

    }

    public void FireSpecialATK()
    {
        if (playerStats.usingFire)
        {
            if (currentParticleFire == null)
            {
                currentParticleFire = Instantiate(fireFx, firePos.transform.position, Quaternion.identity);
                currentParticleFire.transform.SetParent(firePos.transform);
                currentParticleFire.transform.localScale = new Vector3(1, 1, 1);
            }
            Collider2D[] enemyColliders = Physics2D.OverlapBoxAll(firePos.position, fireRange, 0f, enemyLayers);
            foreach (Collider2D enemy in enemyColliders)
            {
                enemy.GetComponent<EnemyStats>().OnHitFire(fireDamage);
            }
        }
        else
        {
            if (currentParticleFire != null)
                Destroy(currentParticleFire.gameObject);
        }
    }

    public void ActivateEarthSpecialATK()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {

            if (playerStats.canUseEarth)
            {
                SetSpecialAnim();
                AudioManager.instance.Play("PAHeavB");
                playerStats.earthCounterUse = playerStats.initialEarthCounterUse;
            }
        }
        
    }
    public void EarthSpecialATK()
    {
        if (playerStats.usingEarth)
        {
            if (currentParticleEarth == null)
            {
                currentParticleEarth = Instantiate(earthFx, feetPos.position, Quaternion.identity);
                currentParticleEarth.transform.SetParent(this.gameObject.transform);
                Instantiate(earthFx2, earthParticlePos.position, Quaternion.identity);
            }
            Collider2D[] enemyColliders = Physics2D.OverlapBoxAll(earthPos.position, earthRange,0f ,enemyLayers);
            foreach (Collider2D enemy in enemyColliders)
            {
                enemy.GetComponent<EnemyStats>().OnHitEarth(earthDamage);
            }
        }
        else
        {
            if (currentParticleEarth != null)
                Destroy(currentParticleEarth.gameObject);
        }
    }

    public void ActivateAirSpecialATK()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {

            if (playerStats.canUseAir)
            {
                SetSpecialAnim();
                AudioManager.instance.Play("PAAirS");
                playerStats.UseAirSpecial();
                GameObject airAtk = Instantiate(airProjectile, transform.position, Quaternion.identity);
                airAtk.gameObject.GetComponent<AirSlashManager>().damage = airDamage;
                if (!playerManager.lookingRight)
                {
                    airAtk.gameObject.GetComponent<AirSlashManager>().Speed *= -1;
                }
            }
        }
        
    }
    #endregion

    #region Animation
    public void SetIdleAnim()
    {
        isIdle = true;
        isWalk = false;
        isJump = false;
        isFalling = false;
        isAtacking = false;
        isSpecial = false;
    }

    public void SetWalkAnim()
    {
        isIdle = false;
        isWalk = true;
        isJump = false;
        isFalling = false;
        isAtacking = false;
        isSpecial = false;
    }

    public void SetJumpAnim()
    {
        isIdle = false;
        isWalk = false;
        isJump = true;
        isFalling = false;
        isAtacking = false;
        isSpecial = false;
    }

    public void SetFallingAnim()
    {
        isIdle = false;
        isWalk = false;
        isJump = false;
        isFalling = true;
        isAtacking = false;
        isSpecial = false;
    }
    public void SetAtackAnim()
    {
        isIdle = false;
        isWalk = false;
        isJump = false;
        isFalling = false;
        isAtacking = true;
        isSpecial = false;
    }

    public void SetSpecialAnim()
    {
        isIdle = false;
        isWalk = false;
        isJump = false;
        isFalling = false;
        isAtacking = false;
        isSpecial = true;
    }


    public void SetAnimations()
    {
        anim.SetBool("Idle", isIdle);
        anim.SetBool("Walk", isWalk);
        anim.SetBool("Jump", isJump);
        anim.SetBool("Falling", isFalling);
        anim.SetBool("Atack", isAtacking);
        anim.SetBool("Special", isSpecial);
    }

    public void SetAnimators()
    {
        anim.runtimeAnimatorController = animator[playerManager.lM.currentSlime];
    }

    #endregion

    #region AudioAnimEvent

    void PlayArmorWalkSound()
    {
        AudioManager.instance.Play("PAWalking");
    }
    void PlayArmorAttackSound()
    {
        AudioManager.instance.Play("PAAttack");
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
