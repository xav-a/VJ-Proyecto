using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDestroyable
{
    [Range(1, 100)]
    public int fireProbability = 10;
    public int health = 100;

    [SerializeField]
	public float moveSpeed = 25f, frequency = 20f, magnitude = 500f;

    public GameObject target;
    public GameObject weapon;
    public AudioSource audioSource;
    public AudioClip deathClip;
    private Vector3 pos;
    private Vector3 targetPos;

    private Direction vDirection;
    private Direction hDirection;

    void Awake()
    {
        gameObject.tag = "Enemy";
        targetPos = target.transform.position;
    }

    void Start()
    {
        pos = transform.position;
        vDirection = (targetPos.y > pos.y) ?
            Direction.UP :
            Direction.DOWN;
        hDirection = (targetPos.x < pos.x) ?
            Direction.UP :
            Direction.DOWN;

    }

    void Update()
    {
        //SinusoidalMovement(vertical: true, positive: false);

        int roll = UnityEngine.Random.Range(1, 100);

        if (roll <= fireProbability)
        {
            weapon.GetComponent<WeaponController>().FireWeapon();
        }
        Movement();
    }

    void LateUpdate()
    {
        transform.position = pos;
        //transform.position = pos;
        if (health <= 0) Destroy();
    }

    void Movement()
    {
        //pos = transform.position;
        pos.x += Time.deltaTime * (int) hDirection * moveSpeed;
        pos.y += Time.deltaTime * (int) vDirection * moveSpeed;
    }

    // Update is called once per frame
    public int LowerHealth(int damage)
    {
        health -= damage;
        Debug.Log($"I'm hit! {health}");
        return health;
    }

    public void Destroy()
    {
        //rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        audioSource.PlayOneShot(deathClip);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Animator>().SetTrigger("Death");
        StartCoroutine(Terminate());
    }

    public IEnumerator Terminate()
    {
        yield return new WaitForSeconds(deathClip.length);
        Destroy(gameObject);
    }

    void SinusoidalMovement(bool vertical, bool positive)
	{
        Vector3 axis = (vertical) ? transform.up : transform.right;
        Vector3 waveOrient = (vertical) ? transform.right : transform.up;
        int dir = (positive) ? 1 : -1;

        pos += (axis * dir * Time.deltaTime * moveSpeed);
        float omega = (Time.time * Time.deltaTime * frequency * Mathf.PI);
        pos = pos + (waveOrient * magnitude * Mathf.Sin(omega));
	}

    void OnBecameInvisible() {
        Destroy(gameObject);
    }

}
