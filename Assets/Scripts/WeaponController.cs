using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject bulletType;
    public int damageRatio = 25;
    // public float fireRange = 100f;

    public AudioSource audioSource;
    public AudioClip fire1Clip;

    // public LayerMask targettableObjectLayer;

    void Awake()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }


    public void FireWeapon()
    {
        audioSource.PlayOneShot(fire1Clip, .60f);
        GameObject bullet = Instantiate(
            bulletType,
            transform.position,
            Quaternion.identity
        );
    }
}
