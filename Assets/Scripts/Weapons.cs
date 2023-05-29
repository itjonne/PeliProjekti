using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Weapons : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GunData gunData;
    [SerializeField] private Transform muzzle;
    // [SerializeField][Range(0, 1)] public float _noise = 0; // T�M� EI VARMAAN OLE EN�� K�YT�SS� -OSSI
    public int bulletsToShoot = 1; // T�ll� pidet��n kirjaa ammusten lukum��r�st�

    private Vector3 playerLastPos;

    float timeSinceLastShot;

    [SerializeField] private Image AmmoBar;
    [SerializeField] private Image ReloadCircle;
    [SerializeField] private Transform canvasTransform;

    [SerializeField] [Range(0, 5)] public float InaccuracyModifier; // Ossin Testi. Mit� isompi t�m� sit� ep�tarkempi pyssy. 0 = ei hajontaa

    public float bulletSpread;

    float lastShot;

    float endFill = 1f;
    float startFill;
    float fillTime;
    //reloadTime

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
        ReloadCircle.fillAmount = 0;
       
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
        //ReloadCircle.fillAmount = 1;

        reloading = true;


        JSAM.AudioManager.PlaySound(AudioLibSounds.sfx_Reload, transform);



        yield return new WaitForSeconds(reloadTime);        
        ammoLeft = magSize;
        reloading = false;
        
        //ReloadCircle.fillAmount = 0;
        UpdateAmmoBar();
    }


    private bool CanShoot() => !reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    /*
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
    */

    public void Shoot(Transform rotation)
    {
        // Debug.Log("SHoot k�ynniss�");


        if (counter < gunData.fireRate)
        {
            //counter += Time.deltaTime;
        }
        else
        {
            if (ammoLeft > 0 && reloading == false)  //reload ehto lis�tty, ett� saadaan manuaalinen lataus toimimaan
            {
                JSAM.AudioManager.PlaySound(gunData.audioClip, transform);
                counter = 0;
                // Debug.Log("T��ll� ammutaan");
                // int playerLevel = gameObject.GetComponent<Character>().level;
                for (int i = 1; i <= bulletsToShoot; i++)
                {
                    /*
                    // Jos parillinen ni ammutaan jotenki erilailla
                    if (bulletsToShoot % 2 == 0)
                    {
                         //AMPUMISANIMAATIO SYSTEEMI MUUTETTU - OSSI 
                        Debug.Log("AMMUTAAN PARITTOMASTI");
                        Vector3 bulletAngleVector;

                        // T�� laskee sen ammuksen suunnan ammusten m��r�n mukaan
                        bulletAngleVector = CalculateBulletAngle(i);

                        GameObject bullet = Instantiate(gunData.bulletPrefab, muzzle.position, Quaternion.identity);
                        gameObject.GetComponent<Animation_Soldier>().OnShoot();
                        bullet.GetComponent<DamageDealer>().shooter = this.gameObject; // Asetetaan panokselle kuka sen ampu, t�ll� voi vaikka nostaa lvl tms.
                        bullet.GetComponent<Rigidbody>().velocity = (muzzle.forward + bulletAngleVector + new Vector3(Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, 0), Random.Range(-bulletSpread, bulletSpread))) * 25f;
                        ammoLeft--;
                        lastShot = Time.time;
                        Destroy(bullet, bulletLife);
                    }
                    */

                    // Jos pariton
                    //if (bulletsToShoot % 2 == 1)
                    {
                        //T�M� kontrolloi perus yhden laukauksen ammuntaa -OSSI
                        //Debug.Log("AMMUTAAN KOLMELLA");

                        //Vector3 bulletAngleVector;          

                        // Annetaan tollasta omatekosta anglea kaikelle
                        //bulletAngleVector = (bulletsToShoot == 1) ? new Vector3(0, 0, 0) : CalculateBulletAngle(i);


                        

                        GameObject bullet = Instantiate(gunData.bulletPrefab, muzzle.position, Quaternion.identity);
                        gameObject.GetComponent<Animation_Soldier>().OnShoot(); //AMPUMISANIMAATIO SYSTEEMI MUUTETTU - OSSI 
                        bullet.GetComponent<DamageDealer>().shooter = this.gameObject; // Asetetaan panokselle kuka sen ampu, t�ll� voi vaikka nostaa lvl tms.
                        bullet.GetComponent<Rigidbody>().velocity = (muzzle.forward /*+ bulletAngleVector */  + new Vector3(Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, 0), Random.Range(-bulletSpread, bulletSpread))) * 25f;

                        lastShot = Time.time;
                        Destroy(bullet, bulletLife);
                    }

                    /*
                    // Muulloin eli ehk� jos vaan yks
                    else
                    {

                    }
                    */

                }
                ammoLeft--;
                UpdateAmmoBar();
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
        counter += Time.deltaTime;  //OSSIN TESTI
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

        if (reloading == true)
        {
            StartCoroutine(ReloadFill());
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

    // T�nne tulee powerUppeja

    public void ExtraBulletPowerUp(int amount)
    {
        bulletsToShoot += amount;
        Debug.Log("EXTRABULLETS, now: " + bulletsToShoot);
    }


    private void UpdateAmmoBar()
    {
        AmmoBar.fillAmount = ammoLeft / magSize;
    }

    private IEnumerator ReloadFill()
    {
        fillTime += Time.deltaTime;
        float percentageComplete = fillTime / reloadTime;
        ReloadCircle.fillAmount = Mathf.Lerp(startFill, endFill, percentageComplete);

        yield return new WaitForSeconds(reloadTime);
        fillTime = 0f;
        ReloadCircle.fillAmount = 0f;
    }

}
