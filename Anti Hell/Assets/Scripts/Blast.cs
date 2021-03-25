using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    [SerializeField] public static int damage = 1;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] public Vector3 moveDir;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    BoxCollider2D myBodyCollider;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = transform.parent.GetComponent<Animator>();
        myBodyCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        DestroyObjectDelayed();

        //if (IsFacingRight())
        //{
        //    myRigidBody.velocity = new Vector2(moveSpeed, 0f);
        //}
        //else
        //{
        //    myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
        //}

        myRigidBody.velocity = moveDir * moveSpeed;
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    public int Attack(int damage)
    {
        return damage;
    }

    private void DestroyObjectDelayed()
    {
        Destroy(gameObject, 5f);
        /*if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy","Ground")))
        {
            // Kills the game object in 5 seconds after loading the object
            Destroy(gameObject, .5f);
        }*/
    }


}
