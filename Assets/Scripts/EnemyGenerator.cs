using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    public int seqNumber = 3;
    public float time = 1f;

    public int interval = 16;
    private int fireInterval = 10;
    private int minInterval = 1, factor = 5;
    public float speed = 80f;
    public float delay = 2f;

    protected int limitIzq;
    protected int limitDer;
    protected int limitRoof;

    public GameObject[] enemySet;
    protected int len, count = 0;

    void Awake()
    {
        interval *= 10;
        minInterval = interval/4;
        fireInterval = interval/2;
        factor = interval/minInterval / 2;
        len = enemySet.Length;

        Camera camera = Camera.main;
        float halfHeight = camera.orthographicSize;
        float halfWidth = camera.aspect * halfHeight;
        limitIzq = (int) (-halfWidth + camera.transform.position.x);
        limitDer =  (int) (halfWidth + camera.transform.position.x);
        limitRoof = (int) (halfHeight + camera.transform.position.y) - 10;
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        factor = factor / 2;
        yield return new WaitForSeconds(delay * 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (len != 0 && count % interval == 0)
        {
            GameObject obj = enemySet[Random.Range(0, len)];

            for (int i = 1; i <= 3; i++)
                StartCoroutine(Generate(obj, (float) i));
            interval = (interval <= minInterval) ? minInterval : interval - factor;
        }
        count++;

        if (count % fireInterval == 0)
        {
            StartCoroutine(EnemyFire(
                GameObject.FindGameObjectsWithTag("Enemy")
            ));
        }
    }

    IEnumerator EnemyFire(GameObject[] currenWave)
    {
        foreach(GameObject enemy in currenWave)
        {
            if (enemy != null)
                enemy.GetComponent<EnemyController>().FireWeapon();
            yield return new WaitForSeconds(0.75f);
        }

    }

    IEnumerator Generate(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        int posX = Random.Range(limitIzq, limitDer);
        GameObject instance = Instantiate(
            obj,
            new Vector3(posX, limitRoof, 0),
            Quaternion.identity);
        instance.GetComponent<EnemyController>().TriggerCanShoot();
    }

}