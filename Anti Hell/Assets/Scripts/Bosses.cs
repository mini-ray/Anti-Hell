using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosses : MonoBehaviour
{
    [SerializeField] float startSpeed = 1f;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] int hp = 3;
    [SerializeField] Vector2 deathKick = new Vector2(5f, 5f);
    [SerializeField] bool invuln = false;
    [SerializeField] float invulnTime = 1.0f;
    float invulnTimer = 0;
    [SerializeField] AudioClip InteractionSFX1;
    [SerializeField] AudioClip InteractionSFX2;
    [SerializeField] AudioClip InteractionSFX3;
    [SerializeField] GameObject CoinPickup;
    [SerializeField] GameObject Player;
    [SerializeField] bool cooldown = false;
    [SerializeField] float cooldownTime = 1.0f;
    float cooldownTimer = 0;
    [SerializeField] GameObject BossBlast;


    bool droppedCoin = false;
    bool isAlive = true;

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;


    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Health();
        Drops();
        Die();
        Invulnerable();
        Attack();
        Cooldown();


        if (IsFacingLeft())
        {
            myRigidBody.velocity = new Vector2(moveSpeed, 0f);
            //Debug.Log("Left");
        }
        else
        {
            myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
            //Debug.Log("Right");
        }

        
    }

    bool IsFacingLeft()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
        //Debug.Log("done");
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Player")) && !invuln)
    //    {
    //        myAnimator.SetBool("Attack", true);
    //        //Debug.Log(collision.gameObject.name);
    //    }
    //    else
    //    {
    //        myAnimator.SetBool("Attack", false);
    //    }
    //}
    private void Die()
    {

        if (hp == 0)
        {

            isAlive = false;
            myAnimator.SetBool("Dead", true);
            moveSpeed = 0;
            GetComponent<Rigidbody2D>().velocity = deathKick;
            Destroy(gameObject, 2f);
        }
    }

    private void Health()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Weapon")) && !invuln)
        {
            hp -= Weapon.damage;
            invuln = true;
            myAnimator.SetBool("Hurt", true);
            AudioSource.PlayClipAtPoint(InteractionSFX1, Camera.main.transform.position);
            AudioSource.PlayClipAtPoint(InteractionSFX2, Camera.main.transform.position);
            AudioSource.PlayClipAtPoint(InteractionSFX3, Camera.main.transform.position);
            //Debug.Log("Ouch");
            invuln = true;
            moveSpeed = 0;
            

        }
        else if (!invuln)
        {
            myAnimator.SetBool("Hurt", false);
            moveSpeed = startSpeed;
        }


    }

    private void Invulnerable()
    {
        if (invuln && invulnTimer < invulnTime)
        {
            invulnTimer += Time.deltaTime;
            moveSpeed = 0;
        }
        else
        {
            invulnTimer = 0;
            invuln = false;
        }
    }

    private void Drops()
    {
        //make to drop coins
        if (isAlive == false && !droppedCoin)
        {
            Instantiate(CoinPickup, transform.position, Quaternion.identity);
            droppedCoin = true;
        }
    }

    private void Attack()
    {
        float distance = Vector3.Distance(gameObject.transform.position, Player.transform.position);
        //Debug.Log(distance);
        if (distance <= 5 && !cooldown)
        {
            myAnimator.SetBool("Attack", true);
            var blast = Instantiate(BossBlast, transform.position, Quaternion.identity);
            blast.GetComponent<EnemyBlast>().moveDir = myRigidBody.velocity.normalized;
            //Debug.Log("BANG");
            moveSpeed = 0;
            cooldown = true;
        }
        else
        {
            moveSpeed = startSpeed;
            myAnimator.SetBool("Attack", false);
            //Debug.Log("Peace");
        }
    }

    private void Cooldown()
    {
        if (cooldown && cooldownTimer < cooldownTime)
        {
            cooldownTimer += Time.deltaTime;
            //Debug.Log("Halt");
        }
        else
        {
            cooldownTimer = 0;
            cooldown = false;
        }
    }

}
