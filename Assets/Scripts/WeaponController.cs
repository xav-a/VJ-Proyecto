using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform weapon;

  /*   public LayerMask whatToHit;
    public float range = 100f;
    public float hitForce = 400f; */

    public int damageRatio = 25;


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
        RaycastHit2D hitInfo = Physics2D.Raycast(weapon.position, weapon.up);
        //Debug to check ray is created and firing
        Debug.DrawRay(weapon.position, weapon.up);

        EnemyController enemy;
        if (hitInfo)
        {
            if ((enemy = hitInfo.transform.GetComponent<EnemyController>()) != null)
            {
                //Make Enemy Take Damage and other stuff
                enemy.LowerHealth(damageRatio);
            }
        }
    }


}
