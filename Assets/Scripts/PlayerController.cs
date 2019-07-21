using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidad = 10f;
    Rigidbody2D rb2D;
    Vector2 movimiento;

    public AudioSource audioSource;
    public AudioClip deathClip;

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;

        if (collider.gameObject.tag == "Enemy" ||
            collider.gameObject.tag == "Obstacle")
        {
            ;
        }
    }

    public void Die()
    {
        StartCoroutine(Terminate());
    }

    IEnumerator Terminate()
    {
        rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb2D.constraints = RigidbodyConstraints2D.FreezePosition;
        audioSource.PlayOneShot(deathClip);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Animator>().SetTrigger("Death");
        this.enabled = false;

        yield return new WaitForSeconds(deathClip.length);
        Destroy(gameObject);
    }
}
