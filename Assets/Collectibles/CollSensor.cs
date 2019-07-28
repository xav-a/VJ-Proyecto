﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollSensor : MonoBehaviour
{
    private GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        go = this.GetComponent<Transform>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag=="Player")
        {
            Destroy(go);
        }
    }

}
