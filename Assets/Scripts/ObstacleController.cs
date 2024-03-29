﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour, IDestroyable
{

    public float speed = 100f;
    public Rigidbody2D rb2D;

    public AudioSource audioSource;
    public AudioClip collisionClip;

    void Awake()
    {
        gameObject.tag = "Obstacle";
        rb2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2D.velocity = Vector2.up * -speed + Vector2.right * Random.Range(-50,50);
        rb2D.angularVelocity = 80f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)

    {
        var HplayerCol = col.collider;
        var HplayerGO = HplayerCol.gameObject;

        if (HplayerGO.tag == "Player")
        {
            //HplayerGO.GetComponent<IDestroyable>().Destroy();
            Destroy();
        }
    }

    public void Destroy()
    {
        audioSource.PlayOneShot(collisionClip);
        rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Animator>().SetTrigger("Death");
        StartCoroutine(Terminate());
    }

    public IEnumerator Terminate()
    {
        yield return new WaitForSeconds(collisionClip.length);
        Destroy(gameObject);
    }

}
