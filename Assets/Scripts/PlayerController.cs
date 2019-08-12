using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float velocidad = 10f;
    Rigidbody2D rb2D;
    Vector2 movimiento;

    //Vidas del jugador
    private int lives = 3;
    public Text ColText;

    //Seccion de las variables importantes para la invulneravilidad
    private SpriteRenderer sr;
    public float time = 0.3f;
    private bool golpeado = false;
    private bool apagado = false;
    private bool prendido = true;
    private int contInvulnerability = 0;

    public AudioSource audioSource;
    public AudioClip deathClip;
    //Sonidos para el escudo
    public AudioSource audioShield;
    public AudioClip shieldDisappears;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        movimiento = new Vector2(0f, 0f);
        sr = GetComponent<SpriteRenderer>();
    }

     void Update()
    {
        movimiento = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void FixedUpdate()
    {
        transform.Translate(movimiento * velocidad);
        LimitarNave();
        if (golpeado)
        {//APAGADO, PRENDIDO, APAGADO, PRENDIDO, APAGADO, PRENDIDO
            this.Invulnerability();
        }
    }

    void Invulnerability()
    {
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        if (sr.enabled && apagado)
        {
            Debug.Log("Desactivado");
            sr.enabled = false;

        }

        if (!sr.enabled && prendido)
        {
            sr.enabled = true;
        }

        time -= Time.deltaTime;
        if (time <= 0)
        {
            time = 0.3f;
            contInvulnerability += 1;
            if (contInvulnerability == 6)
            {
                contInvulnerability = 0;
                sr.enabled = true;
                this.golpeado = false;
                gameObject.GetComponent<PolygonCollider2D>().enabled = true;
                this.prendido = true;
                this.apagado = false;
            }
            else
            {
                if (apagado)
                {
                    apagado = false;
                    prendido = true;
                }
                else
                {
                    apagado = true;
                    prendido = false;
                }
            }

        }
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
            if (child.tag == "Shield") //busco el escudo del jugador
            {
                if (!child.gameObject.activeSelf)//si el escudo no esta activo bajo vida
                {
                    if (lives==0) {
                        StartCoroutine(Terminate());
                    }
                    else
                    {
                        this.golpeado = true;
                        this.apagado = true;
                        this.prendido = false;
                        lives -= 1;
                        this.ColText.text = "Lives: " + this.lives;
                    }
                }
                else//si el escudo esta activo entonces desaparece
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
