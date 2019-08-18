using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : ObstacleGenerator
{

    public int seqNumber = 3;
    private int quad = 1;
    public float time = 1f;
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    IEnumerator Start()
    {
        factor = factor / 2;
        yield return new WaitForSeconds(delay);
    }

    // Update is called once per frame
    void Update()
    {
        if (len != 0 && count % interval == 0)
        {
            float fourth = (float) (limitDer - limitIzq) / 4f;

            StartCoroutine(
                GenerateObstacles(time, seqNumber, fourth)
            );
            interval = (interval <= minInterval) ? minInterval : interval - factor;
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
