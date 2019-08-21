using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    public string[] objectiveTags;

    // Start is called before the first frame update
    public float speed = 50f;
    public string originTag;
    public Direction direction = Direction.UP;
    void Start()
    {
        speed *= (10f * (int) direction) ;
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collider)
    {
        var tag = collider.gameObject.tag;
        if (tag == "Shield" && originTag != "Player")
        {
            collider.gameObject.SetActive(false);
            Destroy(gameObject);
        }

        else if (Array.Exists(objectiveTags, objTag => objTag == tag))
        {
            Destroy(gameObject);

            if (tag == "Player")
            {
                collider.gameObject.GetComponent<PlayerController>().LowerHealth(1);
            }
            else
            {
                collider.gameObject.GetComponent<IDestroyable>().Destroy();
            }
        }
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
