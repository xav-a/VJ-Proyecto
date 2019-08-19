using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IDestroyable
{
    public float speed = 15f;

    private int collectibles = 0;

    public GameObject weapon;
    public GameObject shield;
    public GameObject gameOver;
    public AudioSource audioSource;
    public AudioClip deathClip;


    Rigidbody2D rb2D;
    Vector2 movement;

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

    //Sonidos para el escudo
    public AudioClip shieldAppear;
    public AudioClip shieldDisappears;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        movement = new Vector2(0f, 0f);
        sr = GetComponent<SpriteRenderer>();
    }

     void Update()
    {
        movement = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );
        if (Input.GetButtonDown("Fire1"))
        {
            weapon.GetComponent<WeaponController>().FireWeapon();
        }
    }

    void LateUpdate()
    {
        transform.Translate(movement * speed);
        ClampMovement();
        if (golpeado)
        {//APAGADO, PRENDIDO, APAGADO, PRENDIDO, APAGADO, PRENDIDO
            Invulnerability();
        }
        if (lives==0) {
            Destroy();


        }
    }

    void Invulnerability()
    {
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        if (sr.enabled && apagado)
        {
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

    void ClampMovement()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        string otherTag = collider.gameObject.tag;

        if (otherTag == "Enemy" || otherTag == "Obstacle")
        {
            if (!shield.activeSelf)//si el escudo no esta activo bajo vida
            {
                if (lives==0) {
                    Destroy();
                    gameOver.SetActive(!gameOver.activeSelf);

                }
                else
                {
                    lives -= 1;
                    this.ColText.text = "X" + this.lives;
                }
            }
            else
            {
                Invulnerability();
                shield.SetActive(false);
                audioSource.PlayOneShot(shieldDisappears, 1f);
            }
            this.golpeado = true;
            this.apagado = true;
            this.prendido = false;

            collider.gameObject.GetComponent<IDestroyable>().Destroy();

        }

    }

    public void IncrementCollectibles()
    {
        collectibles++;

        if ((collectibles % 3) == 0)
        {
            audioSource.PlayOneShot(shieldAppear, .60f);
            shield.SetActive(true);
        }
    }

    public void Destroy()
    {
        audioSource.PlayOneShot(deathClip);
        rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb2D.constraints = RigidbodyConstraints2D.FreezePosition;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Animator>().SetTrigger("Death");
        this.enabled = false;

        StartCoroutine(Terminate());
    }

    public IEnumerator Terminate()
    {
        yield return new WaitForSeconds(deathClip.length);
        Destroy(gameObject);
    }

    public int GetCollectibles()
    {
        return collectibles;
    }

    public void LowerHealth(int i)
    {
        this.lives -= i;
        this.golpeado = true;
        this.apagado = true;
        this.prendido = false;
        this.ColText.text = "X" + this.lives;
    }
}
