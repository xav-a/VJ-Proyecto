using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollSensor : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag=="Player")
        {
            col.gameObject
                .GetComponent<PlayerController>()
                .IncrementCollectibles();
            Destroy(gameObject);
        }
    }

}
