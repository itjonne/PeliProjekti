using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapons : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GunData gunData;
    [SerializeField] private Transform muzzle;
   // [SerializeField][Range(0, 1)] public float _noise = 0; // T�M� EI VARMAAN OLE EN�� K�YT�SS� -OSSI
    private int bulletsToShoot = 1; // T�ll� pidet��n kirjaa ammusten lukum��r�st�
    
    private Vector3 playerLastPos;

    float timeSinceLastShot;

    public float ammoLeft;
    [SerializeField] [Range(0, 5)] public float InaccuracyModifier; // Ossin Testi. Mit� isompi t�m� sit� ep�tarkempi pyssy. 0 = ei hajontaa

    public float bulletSpread;

    float lastShot;

    float counter; 


    private void Start()
    {
        playerLastPos = transform.position;
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

        yield return new WaitForSeconds(gunData.reloadTime);
        ammoLeft = gunData.magSize;
        gunData.reloading = false;
    }


    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    private Vector3 CalculateBulletAngle(int i)
    {
        float angle = 0.1f;
        // Annetaan tollasta omatekosta anglea kaikelle
        if (i <= Mathf.Floor(bulletsToShoot / 2))
        {
            return new Vector3(i * -angle, 0, i * -angle);
        }
        else
        {
            return new Vector3(i * -angle, 0, i * -angle);
        }
    }

    public void Shoot(Transform rotation)
    {
        // Debug.Log("SHoot k�ynniss�");


        if (counter < gunData.fireRate)
        {
            counter += Time.deltaTime;
        }
        else
        {
            if (ammoLeft > 0)
            {
                counter = 0;
                // Debug.Log("T��ll� ammutaan");
                // int playerLevel = gameObject.GetComponent<Character>().level;
                for (int i = 1; i <= bulletsToShoot; i++)
                {
                    // Jos parillinen ni ammutaan jotenki erilailla
                    if (bulletsToShoot % 2 == 0)
                    {
                        Debug.Log("AMMUTAAN PARITTOMASTI");
                        Vector3 bulletAngleVector;

                        // T�� laskee sen ammuksen suunnan ammusten m��r�n mukaan
                        bulletAngleVector = CalculateBulletAngle(i);

                        GameObject bullet = Instantiate(gunData.bulletPrefab, muzzle.position, Quaternion.identity);

                        bullet.GetComponent<DamageDealer>().shooter = this.gameObject; // Asetetaan panokselle kuka sen ampu, t�ll� voi vaikka nostaa lvl tms.
                        bullet.GetComponent<Rigidbody>().velocity = (muzzle.forward + bulletAngleVector + new Vector3(Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, 0), Random.Range(-bulletSpread, bulletSpread))) * 25f;
                        //ammoLeft--;
                        //lastShot = Time.time;
                        Destroy(bullet, 3f);
                    }
                    // Jos pariton
                    else if (bulletsToShoot % 2 == 1)
                    {
                        //T�M� kontrolloi perus yhden laukauksen ammuntaa -OSSI
                        //Debug.Log("AMMUTAAN KOLMELLA");

                        Vector3 bulletAngleVector;          
                        
                        // Annetaan tollasta omatekosta anglea kaikelle
                        bulletAngleVector = (bulletsToShoot == 1) ? new Vector3(0, 0, 0) : CalculateBulletAngle(i);
            


                        GameObject bullet = Instantiate(gunData.bulletPrefab, muzzle.position, Quaternion.identity);

                        bullet.GetComponent<DamageDealer>().shooter = this.gameObject; // Asetetaan panokselle kuka sen ampu, t�ll� voi vaikka nostaa lvl tms.
                        bullet.GetComponent<Rigidbody>().velocity = (muzzle.forward + bulletAngleVector + new Vector3(Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, 0), Random.Range(-bulletSpread, bulletSpread))) * 25f; 
                        //ammoLeft--;
                        //lastShot = Time.time;
                        Destroy(bullet, 3f);
                    }
                    // Muulloin eli ehk� jos vaan yks
                    else
                    {
                       
                        GameObject bullet = Instantiate(gunData.bulletPrefab, muzzle.position, Quaternion.identity);

                        bullet.GetComponent<DamageDealer>().shooter = this.gameObject; // Asetetaan panokselle kuka sen ampu, t�ll� voi vaikka nostaa lvl tms.
                        bullet.GetComponent<Rigidbody>().velocity = (muzzle.forward + new Vector3(Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, 0), Random.Range(-bulletSpread, bulletSpread))) * 25f;
                        //ammoLeft--;
                        //lastShot = Time.time;
                        Destroy(bullet, 3f);
                    }

                }
            }
            else
            {
                // ladataan ase
                StartReload();
            }
        }   
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(muzzle.position, muzzle.forward);

        if (Input.GetKeyDown(KeyCode.M))
        {
            gunData = Resources.Load("Guns/MachineGun") as GunData;
        }
       
        if (PlayerIsMoving()) bulletSpread = 0.1f * InaccuracyModifier;
        else bulletSpread = 0.03f * InaccuracyModifier;  //Tarkempi paikaltaan mutta ei kuitenkaan t�ysin hajonnaton

        playerLastPos = transform.position; // t�� pit�� kirjaa pelaajan paikasta spreadia varten
    }

    private bool PlayerIsMoving()
    {
        if (playerLastPos != transform.position) return true;
        return false;
    }

    private void OnGunShot()
    {
        
    }
    
    /* //T�M�N VOI VARMAAN POISTAA JOSSAIN VAIHEESSA? -OSSI
    public Vector3 GetNoise(Vector3 pos)
    {
        Debug.Log(pos);
        var noise = Mathf.PerlinNoise(pos.x * _noise, pos.z * _noise);

        return new Vector3(noise, 0, noise);
    }
    */
    // T�nne tulee powerUppeja

    public void ExtraBulletPowerUp(int amount)
    {
        bulletsToShoot += amount;
        Debug.Log("EXTRABULLETS, now: " + bulletsToShoot);
    }


}
