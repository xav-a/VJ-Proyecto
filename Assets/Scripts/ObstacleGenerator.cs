using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{

    public int interval = 16;
    public int minInterval = 1, factor = 5;
    public float speed = 80f;
    public float delay = 2f;

    protected int limitIzq;
    protected int limitDer;
    protected int limitRoof;

    public GameObject[] obstacles;
    protected int len, count = 0;

    void Awake()
    {
        interval *= 10;
        minInterval = interval/4;
        factor = interval/minInterval / 2;
        Camera camera = Camera.main;
        float halfHeight = camera.orthographicSize;
        float halfWidth = camera.aspect * halfHeight;
        limitIzq = (int) (-halfWidth + camera.transform.position.x);
        limitDer =  (int) (halfWidth + camera.transform.position.x);
        limitRoof = (int) (halfHeight + camera.transform.position.y) - 10;
        len = obstacles.Length;
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);
    }

    // Update is called once per frame
    void Update()
    {
        int posX = Random.Range(limitIzq, limitDer);

        if (len != 0 && count % interval == 0)
        {
            GameObject obj = obstacles[Random.Range(0, len)];
            //obj.GetComponent<ObstacleController>().speed = speed;
            Instantiate(
                obj,
                new Vector3(posX, limitRoof, 0),
                Quaternion.identity);

            interval = (interval <= minInterval) ? minInterval : interval - factor;
        }

        count++;
    }

}
