using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeIntruction : MonoBehaviour
{
    public float timerleft ;
    public GameObject instrucciones;

    // Start is called before the first frame update
    void Start()
    {
        instrucciones.gameObject.SetActive(true);
         
    }

    // Update is called once per frame
    void Update()
    {
        timerleft -= Time.deltaTime;
       

        if (timerleft <= 0)
        {

            instrucciones.gameObject.SetActive(false);

        }
    }
}

