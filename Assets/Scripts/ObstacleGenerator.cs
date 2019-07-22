using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{

    public GameObject ast1;
    public GameObject ast4;
    public GameObject ast6;
    public GameObject deb2;
    public GameObject deb3;
    public GameObject deb4;

    private float limitIzq =52;
    private float limitDer = 786;
    private float limitRoof = 364f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int selec = (int) Random.Range(1,6.99f);
        int posX = (int)Random.Range(limitIzq, limitDer);
        
        if (selec==1)
        {
            Instantiate(ast1,new Vector3(posX,limitRoof,0),Quaternion.identity);
        }else if (selec==2)
        {
            Instantiate(ast4, new Vector3(posX, limitRoof, 0), Quaternion.identity);
        }
        else if (selec==3)
        {
            Instantiate(ast6, new Vector3(posX, limitRoof, 0), Quaternion.identity);
        }
        else if (selec==4)
        {
            Instantiate(deb2, new Vector3(posX, limitRoof, 0), Quaternion.identity); 
        }else if (selec==5)
        {
            Instantiate(deb3, new Vector3(posX, limitRoof, 0), Quaternion.identity);
        }
        else if (selec==6)
        {
            Instantiate(deb4, new Vector3(posX, limitRoof, 0), Quaternion.identity);
        }
    }
}
