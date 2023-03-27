using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : Weapon
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Shoot(Transform rotation)
    {
        if (Time.time > fireRate + lastShot)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = (rotation.forward + new Vector3(Random.Range(-_noise, _noise),0, Random.Range(-_noise, _noise))) * 10f;
            lastShot = Time.time;
        }

    }
}
