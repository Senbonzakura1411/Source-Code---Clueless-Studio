using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    public bool walking = false;

    public float speed;
    public float rotationSpeed;

    public GameObject goodPosModel;

    [Space]

    public Transform currentCube;
    public Transform clickedCube;
    public Transform indicator;

    [Space]

    public List<Transform> finalPath = new List<Transform>();

    private Coroutine _coroutine;


    public Animator anim;

    private bool isIdle;
    private bool isWalking;
    private bool isStair;

    void Start()
    {
        RayCastDown();
    }

    void Update()
    {

        //GET CURRENT CUBE (UNDER PLAYER)

        RayCastDown();

        SetAnimations();

        // CLICK ON CUBE
        if (GameManager.Instance.IsCinematic == false)
        {
            if (!walking)
            {
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition); RaycastHit mouseHit;

                if (Physics.Raycast(mouseRay, out mouseHit))
                {
                    if (mouseHit.transform.GetComponent<Walkable>() != null)
                    {
                        clickedCube = mouseHit.transform;
                        finalPath.Clear();
                        if (clickedCube.GetComponent<Walkable>().imActive)
                        {

                            FindPath();
                            if (Input.GetMouseButtonDown(0))
                            {
                                FollowPath();
                            }

                        }
                        //indicator.position = mouseHit.transform.GetComponent<Walkable>().GetWalkPoint();
                    }
                }
            }
        }
    }
    public void CinematicPathFind(Transform target)
    {
        clickedCube = target;
        finalPath.Clear();
        if (clickedCube.GetComponent<Walkable>().imActive)
        {
            FindPath();
            FollowPath();
        }
    }

    void FindPath()
    {
        List<Transform> nextCubes = new List<Transform>();
        List<Transform> pastCubes = new List<Transform>();

        foreach (WalkPath path in currentCube.GetComponent<Walkable>().possiblePaths)
        {
            if (path.active)
            {
                nextCubes.Add(path.target);
                path.target.GetComponent<Walkable>().previousBlock = currentCube;
            }
        }

        pastCubes.Add(currentCube);

        ExploreCube(nextCubes, pastCubes);
        BuildPath();
    }

    void ExploreCube(List<Transform> nextCubes, List<Transform> visitedCubes)
    {
        Transform current = nextCubes.First();
        nextCubes.Remove(current);

        if (current == clickedCube)
        {
            return;
        }

        foreach (WalkPath path in current.GetComponent<Walkable>().possiblePaths)
        {
            if (!visitedCubes.Contains(path.target) && path.active)
            {
                nextCubes.Add(path.target);
                path.target.GetComponent<Walkable>().previousBlock = current;
            }
        }

        visitedCubes.Add(current);

        if (nextCubes.Any())
        {
            ExploreCube(nextCubes, visitedCubes);
        }
    }

    void BuildPath()
    {
        Transform cube = clickedCube;
        while (cube != currentCube)
        {
            finalPath.Add(cube);
            if (cube.GetComponent<Walkable>().previousBlock != null)
                cube = cube.GetComponent<Walkable>().previousBlock;
            else
                return;
        }

        finalPath.Insert(0, clickedCube);

        if (finalPath.Count > 1)
        {
            if (finalPath[1] == clickedCube)
            {
                goodPosModel.SetActive(true);
                goodPosModel.transform.position = finalPath[1].GetComponent<Walkable>().GetWalkPoint() + new Vector3(0, 0.4f, 0);
            }
        }

    }

    void FollowPath()
    {
        walking = true;

        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(MoveToSpot());
        }
        else
        {
            StopCoroutine(_coroutine);
            _coroutine = _coroutine = StartCoroutine(MoveToSpot());
        }
    }

    private IEnumerator MoveToSpot()
    {
        for (int i = finalPath.Count -1; i > 0; i--)
        {
            var pos = finalPath[i].GetComponent<Walkable>().GetWalkPoint() + new Vector3(0, 0.4f, 0);

            while(true)
            {
                if (Vector3.Distance(transform.position, pos) <= 0.001f)
                {
                    break;
                }
                var step = speed * Time.deltaTime; // calculate distance to move

                Quaternion rotTarget = Quaternion.LookRotation(new Vector3 (pos.x,this.transform.position.y,pos.z) - this.transform.position);
                this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotTarget, rotationSpeed * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, pos, step);
                yield return new WaitForSeconds(0.01f);
            }
        }
        ClearElements();
        yield return new WaitForEndOfFrame();
    }
    void ClearElements()
    {
        foreach (Transform t in finalPath)
        {
            t.GetComponent<Walkable>().previousBlock = null;
        }
        finalPath.Clear();
        goodPosModel.SetActive(false);
        walking = false;
    }

    public void RayCastDown()
    {

        Ray playerRay = new Ray(transform.position, -transform.up);
        RaycastHit playerHit;

        if (Physics.Raycast(playerRay, out playerHit))
        {
            if (playerHit.transform.GetComponent<Walkable>() != null)
            {
                currentCube = playerHit.transform;

                if (playerHit.transform.GetComponent<Walkable>().isStair)
                {
                    if (walking)
                    {
                        SetStair();
                    }
                    else
                    {
                        SetIdle();
                    }
                    
                }
                else
                {
                    if (walking)
                    {
                        SetWalk();
                    }
                    else
                    {
                        SetIdle();
                    }
                    
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Ray ray = new Ray(transform.position, -transform.up);
        Gizmos.DrawRay(ray);
    }


    private void SetAnimations()
    {
        anim.SetBool("Idle", isIdle);
        anim.SetBool("Walk", isWalking);
        anim.SetBool("Stair", isStair);
    }

    private void SetIdle ()
    {
        isIdle = true;
        isWalking = false;
        isStair = false;
    }

    private void SetWalk()
    {
        isIdle = false;
        isWalking = true;
        isStair = false;
    }

    private void SetStair()
    {
        isIdle = false;
        isWalking = false;
        isStair = true;
    }
}
