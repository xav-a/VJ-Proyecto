using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculos : MonoBehaviour
{

    public float velocidad =100f;
    private Transform trans;
    public Rigidbody2D rb;
    private GameObject thisObs;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * -velocidad + Vector2.right * Random.Range(-50,50);
        rb.angularVelocity = 80f;
        trans = GetComponent<Transform>();
        thisObs = trans.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float posx = trans.position.x;
        float posy = trans.position.y;
        //-19
        if (posy<-19 && thisObs!=null)
        {
         //   Debug.Log("se paso de y");
            Destroy(thisObs);
        }else if ((posx<34 || posx>804) && thisObs != null)
        {
            Destroy(thisObs);
        }

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
