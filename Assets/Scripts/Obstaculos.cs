using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculos : MonoBehaviour
{

    public float velocidad = 100f;
    public Rigidbody2D rb;

    public AudioSource audioSource;
    public AudioClip destroyedClip;

    public ParticleSystem explosionParticle;

    void Awake()
    {
        gameObject.tag = "Obstacle";
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = Vector2.up * -velocidad + Vector2.right * Random.Range(-50,50);
        rb.angularVelocity = 80f;
    }

    // Update is called once per frame
    void Update()
    {

    }

   private void OnCollisionEnter2D(Collision2D col)

    {
        var HplayerCol = col.collider;
        var HplayerGO = HplayerCol.gameObject;
        var obsGO = col.otherCollider.gameObject;

        if (HplayerGO.tag == "Player")
        {
            audioSource.PlayOneShot(destroyedClip);
            Instantiate(explosionParticle, transform.position, transform.rotation);
            GameObject.Destroy(HplayerGO);
            GameObject.Destroy(obsGO, destroyedClip.length/2.5f);
        }
    }

}
