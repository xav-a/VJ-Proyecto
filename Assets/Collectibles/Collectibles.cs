using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectibles : MonoBehaviour
{
    public float time=5;
    public GameObject coll;

    public GameObject text;
    private Text ColText;
    private int cantCol;
    private bool creado;

    private int limitIzq = -69;
    private int limitDer = 630;
    private int limitRoof = 300;
    private int limitFloor = 40;

    private GameObject copy;

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
            Debug.Log("momazo");
                time -= Time.deltaTime;

                if (time <= 0)
                {
                    int posX = Random.Range(limitIzq, limitDer);
                    int posY = Random.Range(limitFloor, limitRoof);
                    copy =Instantiate(
                    coll,
                    new Vector3(posX, posY, 0),
                    Quaternion.identity);
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
