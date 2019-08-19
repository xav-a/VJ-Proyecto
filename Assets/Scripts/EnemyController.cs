using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDestroyable
{
    [Range(1, 100)]
    public int fireProbability = 10;
    public int health = 100;

    public int count = 0;
    public int interval = 180;

    [SerializeField]
	public float moveSpeed = 100f;

    //public GameObject target;
    public GameObject weapon;
    public AudioSource audioSource;
    public AudioClip deathClip;
    public AnimationCurve path;

    private bool canShoot = true;
    private Vector3 pos;
    private Vector3 origin;
    private Vector3 targetPos;

    void Awake()
    {
        moveSpeed *= 5;
        gameObject.tag = "Enemy";
        path.postWrapMode = WrapMode.Loop;

        Camera camera = Camera.main;
        float halfHeight = camera.orthographicSize;
        float halfWidth = camera.aspect * halfHeight;
        float limitFloor = (-halfHeight + camera.transform.position.y);

        targetPos = new Vector3(
            pos.x,
            limitFloor,
            pos.z
        );
    }

    IEnumerator Start()
    {
        pos = origin = transform.position;
        yield return new WaitForSeconds(1f);
    }

    void Update()
    {
        //float acum += Time.deltaTime;
        //int roll = UnityEngine.Random.Range(1, 100);
        if (count % interval == 0 && canShoot)
        {
            FireWeapon();
            count = 0;
        }
        count++;
        Movement();
    }

    void LateUpdate()
    {
        transform.position = pos;
        if (health <= 0) Destroy();
    }

    void Movement()
    {
        //pos = Vector3.Lerp(origin, targetPos, moveSpeed) * Time.deltaTime;
        pos.y += Time.deltaTime * (int) Direction.DOWN * moveSpeed;
    }

    // Update is called once per frame
    public int LowerHealth(int damage)
    {
        health -= damage;
        // Debug.Log($"I'm hit! {health}");
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

    public bool TriggerCanShoot() {
        canShoot = !canShoot;
        return canShoot;
    }

    public bool GetCanShoot()
    {
        return canShoot;
    }

    public IEnumerator Terminate()
    {
        yield return new WaitForSeconds(deathClip.length);
        Destroy(gameObject);
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }

    public void FireWeapon()
    {
        weapon.GetComponent<WeaponController>().FireWeapon();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        var tag = collider.gameObject.tag;
        if (tag == "Shield")
        {
            collider.gameObject.SetActive(false);
            Destroy();
        }

        else if (tag == "Player")
        {
            collider.gameObject.GetComponent<PlayerController>().LowerHealth(1);
            Destroy();
        }
    }

}
