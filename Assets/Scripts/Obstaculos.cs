using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculos : MonoBehaviour
{

    public float velocidad =100f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            GameObject.Destroy(HplayerGO);
            GameObject.Destroy(obsGO);
        }
    }

}
