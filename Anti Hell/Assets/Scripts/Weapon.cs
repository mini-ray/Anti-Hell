using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public static int damage = 1;

    private void Update()
    {
        DestroyObjectDelayed();
    }

    public int Attack(int damage)
    {
        return damage;
    }

    private void DestroyObjectDelayed()
    {
        // Kills the game object in 5 seconds after loading the object
        Destroy(gameObject, .5f);
    }
}
