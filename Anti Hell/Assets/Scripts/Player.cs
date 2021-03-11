using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    //Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(3f, 3f);
    [SerializeField] int hp = 6;
    [SerializeField] bool invuln = false;
    [SerializeField] float invulnTime = 1.0f;
    float invulnTimer = 0;
    
    

    //State
    bool isAlive = true;
    
    //Cached conponent references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeet;
    float gravityScaleAtStart;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }

        Run();
        Jump();
        FlipSprite();
        Die();
        Health();
        Invulnerable();
        
    }
    private void Invulnerable()
    {
        if (invuln && invulnTimer < invulnTime)
        {
            invulnTimer += Time.deltaTime;
        }
        else
        {
            invulnTimer = 0;
            invuln = false;
        }
    }
    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }
    private void Jump()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
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
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")) && !invuln)
        {
            hp--;
            invuln = true;
            Debug.Log(hp);
        }


    }

    

    private void Money()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Money")))
        {

        }
    }

    
}
