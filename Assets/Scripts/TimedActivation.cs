using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedActivation : MonoBehaviour
{
    public GameObject target;

    public float delay = 15f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Activate());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Activate()
    {
        target.SetActive(false);
        yield return new WaitForSeconds(delay);
        target.SetActive(true);
    }
}
