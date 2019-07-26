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
        posicionLimitada.x = Mathf.Clamp(posicionLimitada.x, 76, 495);
        posicionLimitada.y = Mathf.Clamp(posicionLimitada.y, 50, 250);
        transform.position = posicionLimitada;
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
