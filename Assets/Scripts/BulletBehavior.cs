using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 50f;
    void Start()
    {
        this.speed *= 10f;
        this.GetComponent<Rigidbody2D>()
            .velocity = this.transform.up * this.speed;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }

    }

    void OnBecameInvisible() {
        Destroy(this.gameObject);
    }
}
