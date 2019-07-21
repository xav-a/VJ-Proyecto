using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidad = 10f;
    Rigidbody2D rb2D;
    Vector2 movimiento;
  

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();

    }

     void Update()
    {
        movimiento = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void FixedUpdate()
    {
        transform.Translate(movimiento * velocidad);
        LimitarNave();
    }

    void LimitarNave()
    {
        Vector2 posicionLimitada = transform.position;
        posicionLimitada.x = Mathf.Clamp(posicionLimitada.x, 230, 595);
        posicionLimitada.y = Mathf.Clamp(posicionLimitada.y, 90, 320);
        transform.position = posicionLimitada;
    }


}
