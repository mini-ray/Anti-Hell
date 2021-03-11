using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] GameObject Weapon;
    [SerializeField] bool atack = false;
    

    Animator myAnimator;


    // Start is called before the first frame update
    void Start()
    {
        myAnimator = transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(Weapon, transform.position, Quaternion.identity);
            myAnimator.SetTrigger("Attack");
            
            
        }
    }

    
}
