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
    public AudioSource audioShield;
    public AudioClip shieldDisappears;

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
        posicionLimitada.x = Mathf.Clamp(posicionLimitada.x, 230, 595);
        posicionLimitada.y = Mathf.Clamp(posicionLimitada.y, 90, 320);
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
        foreach (Transform child in gameObject.transform)
        {
            if (child.tag == "Shield")
            {
                if (!child.gameObject.activeSelf)
                {
                    StartCoroutine(Terminate());
                }
                else
                {
                    audioShield.PlayOneShot(shieldDisappears, .60f);
                    child.gameObject.SetActive(false);
                }

            }
        }
        
        
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
