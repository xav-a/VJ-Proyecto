using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectibles : MonoBehaviour
{
    public float time=5;
    public GameObject collectible;

    public GameObject text;
    private Text ColText;
    private int cantCol;
    private bool creado;

    private int limitIzq;
    private int limitDer;
    private int limitRoof;
    private int limitFloor;

    private GameObject copy;

    void Awake()
    {
        Camera camera = Camera.main;
        float halfHeight = camera.orthographicSize;
        float halfWidth = camera.aspect * halfHeight;
        limitIzq = (int) (-halfWidth + camera.transform.position.x);
        limitDer =  (int) (halfWidth + camera.transform.position.x);
        limitRoof = (int) (halfHeight + camera.transform.position.y);
        limitFloor = (int) (-halfHeight + camera.transform.position.y);
    }

    // Start is called before the first frame update
    void Start()
    {
        cantCol = 0;
        copy = null;
        creado = false;
        ColText=text.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        if (copy==null)
        {
            time -= Time.deltaTime;

            if (time <= 0)
            {
                int posX = Random.Range(limitIzq, limitDer);
                int posY = Random.Range(limitFloor, limitRoof);
                copy = Instantiate(
                    collectible,
                    new Vector3(posX, posY, 0),
                    Quaternion.identity
                );
                time = 5;
                cantCol += 1;
                creado = true;
            }

        }

        if (creado && copy==null)
        {
            ColText.text = "X " + cantCol;
            creado = false;
        }
    }
}
