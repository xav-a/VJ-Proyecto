using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 50f;
    void Start()
    {
        speed *= 10f;
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collider)
    {
        var tag = collider.gameObject.tag;
        if (tag == "Enemy" || tag == "Obstacle")
        {
            Destroy(gameObject);
            collider.gameObject.GetComponent<ObstacleController>().Die();
        }
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
