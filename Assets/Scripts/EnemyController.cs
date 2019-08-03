using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 100;

    void Awake()
    {
        gameObject.tag = "Enemy";
    }

    void FixedUpdate()
    {
        if (health <= 0)
        {
            //Die();
            Debug.Log("Enemy is Dead");
        }
    }

    // Update is called once per frame
    public int LowerHealth(int damage)
    {
        health -= damage;
        Debug.Log($"I'm hit! {health}");
        return health;
    }

    void Die()
    {
        // TODO Play animation here
        // Remove object
        Destroy(gameObject);
    }
}
