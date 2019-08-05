using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : ObstacleGenerator
{

    public int seqNumber = 3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        count++;
        if (len != 0 && count == interval)
        {
            float fourth = (float) (limitDer - limitIzq) / 4f;

            StartCoroutine(
                GenerateObstacles(1f, seqNumber, fourth)
            );

            count = 0;
        }
    }

    IEnumerator GenerateObstacles(float time, int number, float offset)
    {
        int side = Random.Range(0, 2);
        side = (side == 0) ? -1 : 1;
        int select = Random.Range(0, len);
        while (number-- != 0)
        {
            Instantiate(
                obstacles[select],
                new Vector3(
                    Camera.main.transform.position.x + (side * offset),
                    limitRoof,
                    0),
                Quaternion.identity);
            yield return new WaitForSeconds(time);

        }
    }

}
