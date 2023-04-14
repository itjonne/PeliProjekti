using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSAM;

public class Weapons : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GunData gunData;
    [SerializeField] private Transform muzzle;

    float timeSinceLastShot;

    public float ammoLeft; 

    float lastShot;

    float counter; 


    private void Start()
    {
        ammoLeft = gunData.magSize;
        //PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
    }

    public void StartReload()
    {
        if (!gunData.reloading)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;

        AudioManager.PlaySound(Sounds.sfx_Reload);

        yield return new WaitForSeconds(gunData.reloadTime);
        ammoLeft = gunData.magSize;
        gunData.reloading = false;
    }


    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    public void Shoot(Transform rotation)
    {
        Debug.Log("SHoot k�ynniss�");
        /*
        if (gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    Debug.Log(hitInfo.transform.name);
                }

                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();
            }

        }
        */

        if(counter < gunData.fireRate)
        {
            counter += Time.deltaTime;
        }
        else
        {
            if (ammoLeft > 0)
            {
                counter = 0;
                Debug.Log("T��ll� ammutaan");
                GameObject bullet = Instantiate(gunData.bulletPrefab, muzzle.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody>().velocity = (muzzle.forward + new Vector3(Random.Range(0, 0), 0, Random.Range(0, 0))) * 10f;
                ammoLeft--;
                //lastShot = Time.time;
                Destroy(bullet, 5f);
                AudioManager.PlaySound(Sounds.sfx_MachineGun);
            }
            else
            {
                // ladataan ase
                StartReload();
            }
        }
        /*
        if (Time.time > gunData.fireRate + lastShot)
        {


            
        }
        */
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(muzzle.position, muzzle.forward);

        if (Input.GetKeyDown(KeyCode.M))
        {
            gunData = Resources.Load("Guns/MachineGun") as GunData;
        }


    }

    private void OnGunShot()
    {
        
    }
    /*
    public Vector3 GetNoise(Vector3 pos)
    {
        Debug.Log(pos);
        var noise = Mathf.PerlinNoise(pos.x * _noise, pos.z * _noise);

        return new Vector3(noise, 0, noise);
    }
    */



}
