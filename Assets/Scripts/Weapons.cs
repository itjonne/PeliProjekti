using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSAM;


public class Weapons : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GunData gunData;
    [SerializeField] private Transform muzzle;
    [SerializeField] private AudioManager audioManager;
    // [SerializeField][Range(0, 1)] public float _noise = 0; // TÄMÄ EI VARMAAN OLE ENÄÄ KÄYTÖSSÄ -OSSI
    private int bulletsToShoot = 1; // Tällä pidetään kirjaa ammusten lukumäärästä
    
    private Vector3 playerLastPos;

    float timeSinceLastShot;

    
    [SerializeField] [Range(0, 5)] public float InaccuracyModifier; // Ossin Testi. Mitä isompi tämä sitä epätarkempi pyssy. 0 = ei hajontaa

    public float bulletSpread;

    float lastShot;

    float counter;

    //TESTAILTU RELOADIN KANSSA -OSSI
    public bool reloading;
    public int magSize;
    public float reloadTime = 2f;
    public float ammoLeft;
    public float bulletLife = 2.5f;

    private void Start()
    {
        playerLastPos = transform.position;
        ammoLeft = magSize;
        //PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
    }

    public void StartReload()
    {
        if (!reloading)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        reloading = true;

        yield return new WaitForSeconds(reloadTime);
        ammoLeft = magSize;
        reloading = false;
    }


    private bool CanShoot() => !reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

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
        // Debug.Log("SHoot käynnissä");


        if (counter < gunData.fireRate)
        {
            counter += Time.deltaTime;
        }
        else
        {
            if (ammoLeft > 0 && reloading == false)  //reload ehto lisätty, että saadaan manuaalinen lataus toimimaan
            {
                counter = 0;

                

                // Debug.Log("Täällä ammutaan");
                // int playerLevel = gameObject.GetComponent<Character>().level;
                for (int i = 1; i <= bulletsToShoot; i++)
                {
                    // Jos parillinen ni ammutaan jotenki erilailla
                    if (bulletsToShoot % 2 == 0)
                    {
                         //AMPUMISANIMAATIO SYSTEEMI MUUTETTU - OSSI 
                        Debug.Log("AMMUTAAN PARITTOMASTI");
                        Vector3 bulletAngleVector;

                        // Tää laskee sen ammuksen suunnan ammusten määrän mukaan
                        bulletAngleVector = CalculateBulletAngle(i);

                        GameObject bullet = Instantiate(gunData.bulletPrefab, muzzle.position, Quaternion.identity);
                        gameObject.GetComponent<Animation_Soldier>().OnShoot();
                        bullet.GetComponent<DamageDealer>().shooter = this.gameObject; // Asetetaan panokselle kuka sen ampu, tällä voi vaikka nostaa lvl tms.
                        bullet.GetComponent<Rigidbody>().velocity = (muzzle.forward + bulletAngleVector + new Vector3(Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, 0), Random.Range(-bulletSpread, bulletSpread))) * 25f;
                        ammoLeft--;
                        lastShot = Time.time;
                        Destroy(bullet, bulletLife);
                    }

                    // Jos pariton
                    else if (bulletsToShoot % 2 == 1)
                    {
                        //TÄMÄ kontrolloi perus yhden laukauksen ammuntaa -OSSI
                        //Debug.Log("AMMUTAAN KOLMELLA");
                        
                        Vector3 bulletAngleVector;          
                        
                        // Annetaan tollasta omatekosta anglea kaikelle
                        bulletAngleVector = (bulletsToShoot == 1) ? new Vector3(0, 0, 0) : CalculateBulletAngle(i);
            


                        GameObject bullet = Instantiate(gunData.bulletPrefab, muzzle.position, Quaternion.identity);
                        gameObject.GetComponent<Animation_Soldier>().OnShoot(); //AMPUMISANIMAATIO SYSTEEMI MUUTETTU - OSSI 
                        bullet.GetComponent<DamageDealer>().shooter = this.gameObject; // Asetetaan panokselle kuka sen ampu, tällä voi vaikka nostaa lvl tms.
                        bullet.GetComponent<Rigidbody>().velocity = (muzzle.forward + bulletAngleVector + new Vector3(Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, 0), Random.Range(-bulletSpread, bulletSpread))) * 25f; 
                        ammoLeft--;
                        lastShot = Time.time;
                        Destroy(bullet, bulletLife);
                    }
                    // Muulloin eli ehkä jos vaan yks
                    else
                    {
                        gameObject.GetComponent<Animation_Soldier>().OnShoot(); //AMPUMISANIMAATIO SYSTEEMI MUUTETTU - OSSI 
                        GameObject bullet = Instantiate(gunData.bulletPrefab, muzzle.position, Quaternion.identity);

                        bullet.GetComponent<DamageDealer>().shooter = this.gameObject; // Asetetaan panokselle kuka sen ampu, tällä voi vaikka nostaa lvl tms.
                        bullet.GetComponent<Rigidbody>().velocity = (muzzle.forward + new Vector3(Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, 0), Random.Range(-bulletSpread, bulletSpread))) * 25f;
                        ammoLeft--;
                        lastShot = Time.time;
                        Destroy(bullet, bulletLife);
                    }

                }
            }
  

            else
            {
                StartReload();
            }
        }   
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(muzzle.position, muzzle.forward);

        /*
        if (Input.GetKeyDown(KeyCode.M))
        {
            gunData = Resources.Load("Guns/MachineGun") as GunData;
        }
       */

       
        if (Input.GetKey(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
      

        if (PlayerIsMoving()) bulletSpread = 0.1f * InaccuracyModifier;
        else bulletSpread = 0.03f * InaccuracyModifier;  //Tarkempi paikaltaan mutta ei kuitenkaan täysin hajonnaton

        playerLastPos = transform.position; // tää pitää kirjaa pelaajan paikasta spreadia varten
    }

    private bool PlayerIsMoving()
    {
        if (playerLastPos != transform.position) return true;
        return false;
    }

    private void OnGunShot()
    {
        
    }
    
    /* //TÄMÄN VOI VARMAAN POISTAA JOSSAIN VAIHEESSA? -OSSI
    public Vector3 GetNoise(Vector3 pos)
    {
        Debug.Log(pos);
        var noise = Mathf.PerlinNoise(pos.x * _noise, pos.z * _noise);

        return new Vector3(noise, 0, noise);
    }
    */
    // Tänne tulee powerUppeja

    public void ExtraBulletPowerUp(int amount)
    {
        bulletsToShoot += amount;
        Debug.Log("EXTRABULLETS, now: " + bulletsToShoot);
    }


}
