using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{

    public int interval = 25;
    public float speed = 80f;

    protected int limitIzq;
    protected int limitDer;
    protected int limitRoof;

    public GameObject[] obstacles;
    protected int len, count;

    void Awake()
    {
        count = interval;
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

        if (len != 0 && count >= interval)
        {
            count = 0;
            int selec = Random.Range(0, len);
            GameObject obj = obstacles[selec];
            obj.GetComponent<ObstacleController>().speed = speed;
            Instantiate(
                obj,
                new Vector3(posX,limitRoof,0),
                Quaternion.identity);
        }

        count++;
    }
}
