using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    //[SerializeField] AudioClip PowerUpPickUpSFX;

    BoxCollider2D myBodyCollider;

    private void Start()
    {
        myBodyCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("foo");
        if (collision.gameObject.name == "Player")
        {
            //AudioSource.PlayClipAtPoint(PowerUpPickUpSFX, Camera.main.transform.position);
            Destroy(gameObject);
            //Debug.Log("boo");
        }
    }
}
