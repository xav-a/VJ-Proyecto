using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDestroyable
{
    public float velocidad = 15f;

    private int collectibles = 0;

    public GameObject weapon;
    public AudioSource audioSource;
    public AudioClip deathClip;
    public AudioClip collectibleClip;
    public AudioSource audioShield;
    public AudioClip shieldDisappears;

    Rigidbody2D rb2D;
    Vector2 movement;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        movement = new Vector2(0f, 0f);
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
        transform.Translate(movement * velocidad);
        ClampMovement();
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

        if (collider.gameObject.tag == "Enemy" ||
            collider.gameObject.tag == "Obstacle")
        {
            ;
        }
    }

    public void IncrementCollectibles()
    {
        //audioSource.PlayOneShot(collectibleClip);
        collectibles++;
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
}
