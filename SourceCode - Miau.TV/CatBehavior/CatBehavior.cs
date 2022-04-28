using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBehavior : MonoBehaviour
{
    public CatManager catManager;
    public CatNavSystem catNavSystem;

    public float walkPointRange;

    public Vector2 xCatLimit;
    public Vector2 zCatLimit;

    public bool isIntro;
    public bool isIdle;
    public bool isWalking;
    public bool isFollowing;
    public bool isSleeping;
    public bool isEating;
    public bool isPlaying;
    public bool isPooping;
    public bool isExit;

    private Estado estado;

    public string nameObject;

    public Vector3 catDestination;

    public float stateCounter;
    public float walkCounter;

    public float randomX;
    public float randomZ;

    public GameObject objectTarget;

    enum Estado
    {
        INTRO,
        IDLE,
        WALK,
        FOLLOW,
        EAT,
        SLEEP,
        PLAY,
        POOP,
        EXIT
    };

    private void Start()
    {
        catNavSystem = gameObject.GetComponent<CatNavSystem>();
        catManager = gameObject.GetComponent<CatManager>();
        catManager.myCat = this;
        /*catDestination = transform.position;
        catNavSystem.catTarget = catDestination;*/
        catNavSystem.canWalk = false;
        estado = Estado.INTRO;
        stateCounter = Random.Range(5, 10);
    }

    public void Update()
    {
        StateControl();
    }

    public void StateControl ()
    {
        switch (estado)
        {
            case Estado.INTRO:
                CatIntro();
                break;
            case Estado.IDLE:
                CatIdle();
                break;
            case Estado.WALK:
                CatWalking();
                break;
            case Estado.FOLLOW:
                Catfollow();
                break;
            case Estado.EAT:
                CatEat();
                break;
            case Estado.SLEEP:
                CatSleep();
                break;
            case Estado.PLAY:
                CatPlay();
                break;
            case Estado.POOP:
                CatPoop();
                break;
            case Estado.EXIT:
                CatExit();
                break;
        }
    }

    public void CatIntro()
    {
        isIdle = false;
        isWalking = false;
        isFollowing = false;
        isSleeping = false;
        isEating = false;
        isPlaying = false;
        isPooping = false;
        isIntro = true;
        isExit = false;
        if (catNavSystem.canWalk == false)
        {
            catDestination = transform.position;
            catNavSystem.canWalk = true;
        }
        if (transform.position == catDestination)
        {
            catDestination.x = Random.Range(xCatLimit.x, xCatLimit.y);
            catDestination.z = Random.Range(zCatLimit.x, zCatLimit.y);
            //SearchWalkPoint();
            catNavSystem.catTarget = catDestination;
            
        }
        if (stateCounter > 0)
        {
            stateCounter -= Time.deltaTime;
        }
        else
        {
            int randomTimer = Random.Range(5, 10);
            stateCounter = randomTimer;
            catNavSystem.catTarget = this.transform.position;
            estado = Estado.IDLE;
        }
    }

    public void CatExit()
    {
        isIdle = false;
        isWalking = false;
        isFollowing = false;
        isSleeping = false;
        isEating = false;
        isPlaying = false;
        isPooping = false;
        isIntro = false;
        isExit = true;

        catDestination.x = catManager.initialPosition.x;
        catDestination.z = catManager.initialPosition.z;
        catNavSystem.catTarget = catDestination;
        if (transform.position == catDestination)
        {
            catManager.lM.catsInGame--;
            Destroy(this.gameObject);

        }
    }

    public void CatIdle ()
    {
        isIdle = true;
        isWalking = false;
        isFollowing = false;
        isSleeping = false;
        isEating = false;
        isPlaying = false;
        isPooping = false;
        isIntro = false;
        isExit = false;
        catNavSystem.canWalk = false;
        catDestination = transform.position;
        catNavSystem.catTarget = catDestination;

        // + El navmesh del gato se detiene
        catNavSystem.navMeshAgent.isStopped = true;
        
        if (stateCounter > 0)
        {
            stateCounter -= Time.deltaTime;
        }
        else
        {
            int randomTimer = Random.Range(25,60);
            stateCounter = randomTimer;
            catDestination = transform.position;
            estado = Estado.WALK;
        }
    }

    public void CatWalking()
    {
        isIdle = false;
        isWalking = true;
        isFollowing = false;
        isSleeping = false;
        isEating = false;
        isPlaying = false;
        isPooping = false;
        isIntro = false;
        isExit = false;
        catNavSystem.canWalk = true;

        // + El navmesh del gato se reanuda
        catNavSystem.navMeshAgent.isStopped = false;

        if (walkCounter > 0)
        {
            walkCounter -= Time.deltaTime;
            if (transform.position.x == catDestination.x && transform.position.z == catDestination.z)
            {
                if (transform.position.x + (randomX*2) > xCatLimit.x && transform.position.x + (randomX*2) < xCatLimit.y)
                {
                    randomX *= 1;
                }
                else
                {
                    walkCounter = 0;
                }

                if (transform.position.z + (randomZ*2) > zCatLimit.x && transform.position.z + (randomZ *2) < zCatLimit.y)
                {
                    randomZ *= 1;
                }
                else
                {
                    walkCounter = 0;
                }
                //print("Hi");
                catDestination = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
                catNavSystem.catTarget = catDestination;
            }
        }
        else
        {
            //print("Hello");
            SearchWalkPoint();
            catNavSystem.catTarget = catDestination;
            walkCounter = Random.Range(15, 30);

        }

        
        if (stateCounter > 0)
        {
            stateCounter -= Time.deltaTime;
        }
        else
        {
            int randomTimer = Random.Range(5, 10);
            stateCounter = randomTimer;

            if (catManager.catStats.needPoop)
            {
                catNavSystem.catTarget = this.transform.position;
                estado = Estado.POOP;
            }
            else
            {
                catNavSystem.catTarget = this.transform.position;
                estado = Estado.IDLE;
            }
        }
    }

    public void SetFollow (string objectName)
    {
        if (!isExit)
        {
            estado = Estado.FOLLOW;
            nameObject = objectName;
        }
        
    }
    public void SetExit ( )
    {
        estado = Estado.EXIT;

    }

    public void Catfollow ()
    {
        isIdle = false;
        isWalking = false;
        isFollowing = true;
        isSleeping = false;
        isEating = false;
        isPlaying = false;
        isPooping = false;
        isIntro = false;
        isExit = false;
        if (objectTarget != null)
        {
            catDestination = objectTarget.transform.position;
        }

        switch (nameObject)
        {
            case "food":
                if (catManager.catStats.needEat)
                {
                    
                    if (transform.position.x != catDestination.x && transform.position.z != catDestination.z)
                    {
                        catNavSystem.catTarget = catDestination;
                    }
                    else
                    {
                        int randomTimer = Random.Range(5, 10);
                        stateCounter = randomTimer;
                        catManager.catStats.imEating = true;
                        estado = Estado.EAT;
                    }
                }
                else
                {
                    int randomTimer = Random.Range(5, 15);
                    stateCounter = randomTimer;
                    catDestination = transform.position;
                    nameObject = "";
                    estado = Estado.WALK;
                }
                break;

            case "bed":
                if (catManager.catStats.needSleep)
                {
                    if (transform.position.x != catDestination.x && transform.position.z != catDestination.z)
                    {
                        catNavSystem.catTarget = catDestination;
                    }
                    else
                    {
                        int randomTimer = Random.Range(5, 10);
                        stateCounter = randomTimer;
                        catManager.catStats.imSleep = true;
                        estado = Estado.SLEEP;
                    }
                }
                else
                {
                    int randomTimer = Random.Range(5, 15);
                    stateCounter = randomTimer;
                    catDestination = transform.position;
                    nameObject = "";
                    estado = Estado.WALK;
                }
                break;
            case "ball":
                if (transform.position.x != catDestination.x && transform.position.z != catDestination.z)
                {
                    catNavSystem.catTarget = catDestination;
                }
                else
                {
                    int randomTimer = Random.Range(5, 10);
                    stateCounter = randomTimer;
                    catManager.catStats.imSleep = true;
                    estado = Estado.PLAY;
                }
                break;
            default:
                int random = Random.Range(5, 15);
                stateCounter = random;
                catDestination = transform.position;
                estado = Estado.WALK;
                break;
        
        }
    }

    public void CatSleep ()
    {
        isIdle = false;
        isWalking = false;
        isFollowing = false;
        isSleeping = true;
        isEating = false;
        isPlaying = false;
        isPooping = false;
        isIntro = false;
        isExit = false;
        if (stateCounter > 0)
        {
            stateCounter -= Time.deltaTime;
        }
        else
        {
            int randomTimer = Random.Range(5, 15);
            stateCounter = randomTimer;
            catManager.catStats.needSleep = false;
            catManager.catStats.imSleep = false;
            catDestination = transform.position;
            nameObject = "";
            catManager.currentActivities++;
            estado = Estado.WALK;
        }
    }

    public void CatEat ()
    {
        isIdle = false;
        isWalking = false;
        isFollowing = false;
        isSleeping = false;
        isEating = true;
        isPlaying = false;
        isPooping = false;
        isIntro = false;
        isExit = false;
        if (stateCounter > 0)
        {
            stateCounter -= Time.deltaTime;
        }
        else
        {
            int randomTimer = Random.Range(5, 15);
            stateCounter = randomTimer;
            catManager.catStats.needEat = false;
            catManager.catStats.imEating = false;
            catDestination = transform.position;
            nameObject = "";
            catManager.currentActivities++;
            estado = Estado.WALK;
        }
    }

    public void CatPlay()
    {
        isIdle = false;
        isWalking = false;
        isFollowing = false;
        isSleeping = false;
        isEating = false;
        isPlaying = true;
        isPooping = false;
        isIntro = false;
        isExit = false;
        if (stateCounter > 0)
        {
            stateCounter -= Time.deltaTime;
        }
        else
        {
            int randomTimer = Random.Range(5, 15);
            stateCounter = randomTimer;
            catManager.catStats.needEat = false;
            catManager.catStats.imEating = false;
            catDestination = transform.position;
            nameObject = "";
            catManager.currentActivities++;
            estado = Estado.WALK;
        }
    }

    public void CatPoop()
    {
        isIdle = false;
        isWalking = false;
        isFollowing = false;
        isSleeping = false;
        isEating = false;
        isPlaying = false;
        isPooping = true;
        isIntro = false;
        isExit = false;
        if (stateCounter > 0)
        {
            stateCounter -= Time.deltaTime;
        }
        else
        {
            int randomTimer = Random.Range(5, 15);
            stateCounter = randomTimer;
            GameObject myPoop = Instantiate(catManager.poop, gameObject.transform.position, Quaternion.identity);
            myPoop.gameObject.GetComponent<PoopManager>().myCat = catManager;
            catManager.catStats.needPoop = false;
            catManager.catStats.poop -= catManager.catStats.maxPoop;
            catDestination = transform.position;
            catManager.currentActivities++;
            estado = Estado.WALK;
        }
    }

    public void SearchWalkPoint ()
    {
        randomZ = Random.Range(-walkPointRange, walkPointRange);
        randomX = Random.Range(-walkPointRange, walkPointRange);
        if (transform.position.x +randomX > xCatLimit.x && transform.position.x + randomX < xCatLimit.y )
        {
            catDestination.x = transform.position.x + randomX;
        }
        else
        {
            catDestination.x = transform.position.x;
        }

        if (transform.position.z + randomZ > zCatLimit.x && transform.position.z + randomZ < zCatLimit.y)
        {
            catDestination.z = transform.position.z + randomZ;
        }
        else
        {
            catDestination.z = transform.position.z;
        }
    }

}