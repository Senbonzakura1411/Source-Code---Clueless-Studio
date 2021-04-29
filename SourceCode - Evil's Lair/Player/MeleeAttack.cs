using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeAttack : MonoBehaviour
{
    public Camera cam;
    public RaycastHit hit;
    public LayerMask whatIsEnemy;
    public AudioSource source;

    Gamepad gamepad;
    Mouse mouse;

    void Update()
    {
        if (gamepad.rightTrigger.wasPressedThisFrame || mouse.leftButton.wasPressedThisFrame)
        {
            gameObject.GetComponentInParent<Animator>().Play("Attack");
            Attack();
        }
    }
   private void FixedUpdate()
    {
        gamepad = Gamepad.current;
        if (gamepad == null)
            return;
        mouse = Mouse.current;
        if (mouse == null)
            return;
    }
    private void Attack()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 2.5f, whatIsEnemy))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
               source.PlayDelayed(0.25f);
               hit.collider.GetComponentInParent<Enemy>().TakeDamage(100, cam.GetComponentInParent<FPSController>().transform);
            }
        }
    }

 
}
