using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject bulletType;
    public int damageRatio = 25;
    public float fireRange = 100f;

/*   public LayerMask targettableObjectLayer;
*/

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireWeapon();
        }
    }

    void FireWeapon()
    {
        GameObject bullet = Instantiate(
            this.bulletType,
            this.transform.position,
            Quaternion.identity
        );
    }
}
