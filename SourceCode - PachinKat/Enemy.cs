using System.Collections;
using UnityEngine;

public class Enemy
{
    public int _hp { get; private set; }
    float _vel;

    float movementX = 0.75f;
    float movementY = 1f;
    bool canMove = true;

    Animator _anim;

    public static ulong points { get; set;}
    public Enemy(int hp, float vel, Animator anim)
    {
        this._hp = hp;
        this._vel = vel;
        this._anim = anim;
    }

    public void TakeDamage()
    {
        AudioManager.instance.Play("BirdDamaged");
        _anim.SetTrigger("Damaged");
        points += 100;
        _hp -= 1;
    }

    public IEnumerator Move(bool IsRight, GameObject obj)
    {
        if (IsRight)
        {
            while (canMove && obj.activeSelf )
            {
                Vector2 position = obj.transform.position;
                position.x += movementX;
                obj.transform.position = position;
                yield return new WaitForSeconds(_vel);
                position.x += movementX;
                obj.transform.position = position;
                yield return new WaitForSeconds(_vel);
                position.x += movementX;
                obj.transform.position = position;
                yield return new WaitForSeconds(_vel);
                position.x += movementX;
                obj.transform.position = position;
                yield return new WaitForSeconds(_vel);
                position.x += movementX;
                obj.transform.position = position;
                yield return new WaitForSeconds(_vel);
                position.y += movementY;
                obj.transform.position = position;
                yield return new WaitForSeconds(_vel);
            }
        }
        else
        {
            while (canMove && obj.activeSelf)
            {
                Vector2 position = obj.transform.position;
                position.x -= movementX;
                obj.transform.position = position;
                yield return new WaitForSeconds(_vel);
                position.x -= movementX;
                obj.transform.position = position;
                yield return new WaitForSeconds(_vel);
                position.x -= movementX;
                obj.transform.position = position;
                yield return new WaitForSeconds(_vel);
                position.x -= movementX;
                obj.transform.position = position;
                yield return new WaitForSeconds(_vel);
                position.x -= movementX;
                obj.transform.position = position;
                yield return new WaitForSeconds(_vel);
                position.y += movementX;
                obj.transform.position = position;
                yield return new WaitForSeconds(_vel);
            }
        }
    }

    public void Die(GameObject obj)
    {
        if (_hp <= 0)
        {
            canMove = false;
            UnityEngine.GameObject.Destroy(obj, 2f);
            _anim.SetBool("isDead", true);
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddTorque(-200f);
            
        }
    }
}
