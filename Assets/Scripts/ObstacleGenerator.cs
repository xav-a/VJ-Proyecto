using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{

    public int interval = 25;

    private int limitIzq;
    private int limitDer;
    private float limitRoof;

    public GameObject[] obstacles;
    private int len, count = 0;

    void Awake()
    {
        Camera camera = Camera.main;
        float halfHeight = camera.orthographicSize;
        float halfWidth = camera.aspect * halfHeight;
        limitIzq = (int) (-halfWidth + camera.transform.position.x);
        limitDer =  (int) (halfWidth + camera.transform.position.x);
        limitRoof = (int) (halfHeight + camera.transform.position.y);
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
