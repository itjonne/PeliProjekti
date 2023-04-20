using JSAM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    private Character target;
    [SerializeField] private Transform muzzle;
    private float distanceFromTarget;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] private float shootingDistance = 20f; // Kuinka kaukaa t‰‰ alkaa ampumaan

    public void Awake()
    {
        health = 30f;
    }

    private void OnTriggerEnter(Collider other)
    {
        JSAM.AudioManager.PlaySound(Sounds.sfx_Hitmarker);

        // Jos kollisio tapahtuu pelaajan kanssa. Pelaajalla taitaa olla oma handleri, t?ss? vois olla puukotus
        if (other.GetComponent<Character>())
        {
            Debug.Log("NYT OSU");

            // Die(); T?H?N VOIS LAITTAA VEITSIANIMAATION
        }

        // Jos osuu johonkin joka tekee damagee, nyt panos
        if (other.GetComponent<DamageDealer>())
        {
            DamageDealer damageDealer = other.GetComponent<DamageDealer>();
            if (damageDealer != null)
            {
                gameObject.GetComponent<Anim_Enemy1>().OnDamageTaken(); // Kutsutaan animaattoria
                this.SetHealth(-damageDealer.damage); // Kuolema tapahtuu tuolla p??luokan puolella Enemy-scriptiss?.
                Destroy(damageDealer.gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Character[] characters = GameObject.FindObjectsOfType<Character>();
        target = characters[Random.Range(0, characters.Length)];
        if (target != null)
        {
            CalculateDistanceFromTarget(target);
            
        }
    }

    private void CalculateDistanceFromTarget(Character target)
    {
        distanceFromTarget = Vector3.Distance(this.transform.position, target.transform.position);
    }

    public void MoveTo(Vector3 position)
    {
        // TODO: Liiku tietyn matkan p‰‰h‰n
        transform.position = (Vector3.MoveTowards(transform.position, position, movementSpeed * Time.deltaTime));
    }


    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if (distanceFromTarget > shootingDistance)
            {
                MoveTo(target.transform.position);
            } else // Ollaan tarpeeks l‰hell‰
            {
                Shoot();
            }

        }
        else
        {
            Character[] characters = GameObject.FindObjectsOfType<Character>();
            target = characters[Random.Range(0, characters.Length)];
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = (muzzle.forward + new Vector3(Random.Range(0, 0), 0, Random.Range(0, 0))) * 25f;
        //ammoLeft--;
        //lastShot = Time.time;
        Destroy(bullet, 5f);
    }
}
