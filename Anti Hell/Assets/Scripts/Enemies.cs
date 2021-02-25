using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] int hp = 3;
    [SerializeField] Vector2 deathKick = new Vector2(5f, 5f);
    [SerializeField] bool invuln = false;
    [SerializeField] float invulnTime = 1.0f;
    float invulnTimer = 0;

    bool isAlive = true;

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Health();
        Die();
        if (IsFacingRight())
        {
            myRigidBody.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
        
    }

    private void Die()
    {

        if (hp == 0)
        {

            isAlive = false;
            myAnimator.SetTrigger("Dead");
            GetComponent<Rigidbody2D>().velocity = deathKick;
        }
    }

    private void Health()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Weapon")) && !invuln)
        {
            hp-=Weapon.damage;
            invuln = true;
            Debug.Log("Ouch");
            
        }


    }

    private void Healt()
    {

    }
}
