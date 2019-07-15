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
        movimiento = new Vector2(0f, 0f);
    }

     void Update()
    {
        movimiento.x = Input.GetAxis("Horizontal");
        movimiento.y = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        transform.Translate(movimiento * velocidad);
        //LimitarMovimiento();
    }

    void LimitarMovimiento()
    {
        Vector2  PosicionLimitada = transform.position;
        PosicionLimitada.x = Mathf.Clamp(PosicionLimitada.x, -6.89f, 6.89f);
        PosicionLimitada.y = Mathf.Clamp(PosicionLimitada.y, -0.13f, 0.13f);

        transform.position = PosicionLimitada;
    }


}
