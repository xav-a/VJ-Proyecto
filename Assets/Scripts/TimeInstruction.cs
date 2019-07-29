using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeInstruction : MonoBehaviour
{
    public float timerleft = 8f;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        timerleft -= Time.deltaTime;

        if (timerleft <= 0)
        {

            gameObject.SetActive(false);

        }
    }
}

