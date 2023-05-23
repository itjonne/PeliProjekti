using JSAM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{

    public override void Shoot(Transform rotation)
    {
        if (Time.time > fireRate + lastShot)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = rotation.forward * 10f;
            lastShot = Time.time;

           
        }
    }
}
