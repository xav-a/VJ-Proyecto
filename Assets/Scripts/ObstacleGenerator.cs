using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{

    public int interval = 10;

    private int limitIzq = 52;
    private int limitDer = 786;
    private float limitRoof = 364f;

    public GameObject[] obstacles;
    private int len, count = 0;

    void Awake()
    {
        len = obstacles.Length;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int posX = Random.Range(limitIzq, limitDer);
        count++;

        if (len != 0 && count == interval)
        {
            count = 0;
            int selec = Random.Range(0, len);
            Instantiate(
                obstacles[selec],
                new Vector3(posX,limitRoof,0),
                Quaternion.identity);
        }
    }
}
