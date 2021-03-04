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
    //[SerializeField] AudioClip InteractionSFX;
    [SerializeField] GameObject CoinPickup;


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


        if (IsFacingLeft())
        {
            myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
            //Debug.Log("Left");
        }
        else
        {
            myRigidBody.velocity = new Vector2(moveSpeed, 0f);
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

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }
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
            //AudioSource.PlayClipAtPoint(InteractionSFX, Camera.main.transform.position);
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
}
