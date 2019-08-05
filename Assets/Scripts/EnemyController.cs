using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDestroyable
{
    [Range(1, 100)]
    public int fireProbability = 10;
    public int health = 100;

    [SerializeField]
	public float moveSpeed = 100f, magnitude = 100f;

    public GameObject target;
    public GameObject weapon;
    public AudioSource audioSource;
    public AudioClip deathClip;
    public AnimationCurve movePath;
    private Vector3 pos;
    private Vector3 origin;
    private Vector3 targetPos;

    private Direction vDirection;
    private Direction hDirection;

    void Awake()
    {
        magnitude *= 10f;
        gameObject.tag = "Enemy";
        targetPos = target.transform.position;
        movePath.postWrapMode = WrapMode.Loop;
    }

    void Start()
    {
        pos = transform.position;
        vDirection = (pos.y < targetPos.y) ?
            Direction.UP :
            Direction.DOWN;
        hDirection = (pos.x > targetPos.x) ?
            Direction.RIGHT :
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
        float sample = movePath.Evaluate(Time.time);
        //Debug.Log($"{sample} curve {magnitude}");
        //pos.x += Time.deltaTime * (int) hDirection * moveSpeed;
        pos.y += Time.deltaTime * (int) vDirection * moveSpeed;
        pos.x += (sample * magnitude) * Time.deltaTime;
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

    void OnBecameInvisible() {
        Destroy(gameObject);
    }

}
