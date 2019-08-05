using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : ObstacleGenerator
{

    public int seqNumber = 3;
    private int quad = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (len != 0 && count >= interval)
        {
            float fourth = (float) (limitDer - limitIzq) / 4f;

            StartCoroutine(
                GenerateObstacles(1f, seqNumber, fourth)
            );
            count = 0;
        }

        count++;
    }

    IEnumerator GenerateObstacles(float time, int number, float offset)
    {
        quad = (count % 3) + 1;
        int select = Random.Range(0, len);
        while (number-- != 0)
        {
            Instantiate(
                obstacles[select],
                new Vector3(
                    limitIzq + (quad * offset),
                    limitRoof,
                    0),
                Quaternion.identity);
            yield return new WaitForSeconds(time);
        }
        quad++;
    }

}
