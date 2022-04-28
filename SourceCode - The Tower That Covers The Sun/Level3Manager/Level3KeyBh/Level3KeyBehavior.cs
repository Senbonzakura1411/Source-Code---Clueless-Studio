using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3KeyBehavior : MonoBehaviour
{
    public Vector3 initialPosition;
    public Vector3 respawnPosition;

    public Vector3 direction;

    public Rigidbody rigidBody;

    public float gravityMultiply;

    public bool moving;

    public float speed;

    public bool isGrounded;

    public GameObject parent;
    public float distanceGround;

    public GameObject lastGoal;

    public bool CanMove;

    int directionToMove; //1 up, 2 right, 3 down, 4 left

    public float moveH, moveV;

    public void Start()
    {
        initialPosition = transform.position;
        respawnPosition = initialPosition;
    }
    private void Update()
    {

        if (CanMove)
        {
            moveH = Input.GetAxisRaw("Horizontal");
            moveV = Input.GetAxisRaw("Vertical");

            RaycastHit hit;
            Ray rayCast = new Ray(transform.position, direction);

            Debug.DrawRay(transform.position, direction, Color.green);

            if (Physics.Raycast(rayCast, out hit, 0.5f))
            {
                if (hit.transform.gameObject.CompareTag("Obstacle"))
                {
                    StopKey();
                }
            }
            else
            {
                //nothing
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                StopKey();
                RestartCube();
            }
            
        }
        CheckIfGrounded();
    }

    public void FixedUpdate()
    {
        if (!isGrounded)
        {
            rigidBody.AddForce(new Vector3(0f, (Physics.gravity.y * gravityMultiply), 0f));
        }

        if (!moving)
        {
            if (moveV > 0)
            {
                directionToMove = 1;
                DirectionToMove();
            }
            else if (moveH > 0)
            {
                directionToMove = 2;
                DirectionToMove();
            }
            else if (moveV < 0)
            {
                directionToMove = 3;
                DirectionToMove();
            }
            else if (moveH < 0)
            {
                directionToMove = 4;
                DirectionToMove();
            }
        }
        if (CanMove)
        Move();



        if (lastGoal!= null)
        {
            respawnPosition = lastGoal.transform.position;
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        /*if (other.gameObject.CompareTag("Obstacle"))
        {
            StopKey();
        }*/
        if (other.gameObject.CompareTag("Destroyer"))
        {
            StopKey();
            RestartCube();
        }
    }


    public void StopKey()
    {
        rigidBody.velocity = Vector3.zero;
        moving = false;
    }

    public void DirectionToMove()
    {
        switch (directionToMove)
        {
            case 1:
                direction = Vector3.forward;
                break;
            case 2:
                direction = Vector3.right;
                break;
            case 3:
                direction = Vector3.back;
                break;
            case 4:
                direction = Vector3.left;
                break;
        }
        moving = true;
    }

    public void Move()
    {
        if (moving)
        {
            rigidBody.velocity = new Vector3(direction.x * speed, rigidBody.velocity.y, direction.z * speed);
        }
    }

    private void CheckIfGrounded()
    {
        
        isGrounded = Physics.Raycast(transform.position, Vector3.down, gameObject.GetComponent<Collider>().bounds.extents.y + 0.2f);

        if (isGrounded)
        {
            RaycastHit hit;
            Ray rayCast = new Ray(transform.position, Vector3.down);
            Physics.Raycast(rayCast, out hit, distanceGround);
            if (hit.transform.gameObject != null)
            {
                if (parent == null || parent != hit.transform.gameObject)
                {
                    parent = hit.transform.gameObject;
                    transform.SetParent(parent.transform);
                }
            }
           
        }
    }

    public void RestartCube ()
    {
        transform.position = respawnPosition;
    }

}
