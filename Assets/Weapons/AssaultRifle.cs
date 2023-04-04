using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSAM;

public class AssaultRifle : Weapon
{
    [SerializeField] private Transform muzzle;

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
        /*
        if (Time.time > fireRate + lastShot)
        {
            GameObject bullet = Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = (muzzle.forward + new Vector3(Random.Range(-_noise, _noise),0, Random.Range(-_noise, _noise))) * 10f;
            lastShot = Time.time;
            Destroy(bullet, 5f);

            AudioManager.PlaySound(Sounds.sfx_MachineGun);
        }
        */

    }
}
