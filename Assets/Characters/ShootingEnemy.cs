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

    private float timeSinceLastShot;
    private float fireRate = 2f;

    public void Awake()
    {
        health = 20f;
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

    private void CalculateClosestTarget()
    {
        Character[] characters = GameObject.FindObjectsOfType<Character>();
        float closestDistance = float.PositiveInfinity;
        Character closestCharacter = null;
        foreach(Character character in characters)
        {
            float distance = Vector3.Distance(this.transform.position, character.transform.position);
            if (distance <= closestDistance)
            {
                closestDistance = distance;
                closestCharacter = character;
            }
        }
        target = closestCharacter;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        CalculateClosestTarget();


        if (target != null)
        {
            CalculateDistanceFromTarget(target);
            if (distanceFromTarget > shootingDistance)
            {
                transform.LookAt(target.transform.position);
                MoveTo(target.transform.position);
            } else // Ollaan tarpeeks l‰hell‰
            {
                transform.LookAt(target.transform.position);
                if (timeSinceLastShot >= fireRate)
                {
                    Shoot();
                    timeSinceLastShot = 0;
                }
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
        bullet.GetComponent<Rigidbody>().velocity = (muzzle.forward + new Vector3(Random.Range(0, 0), 0, Random.Range(0, 0))) * 5f;
        //ammoLeft--;
        //lastShot = Time.time;
        Destroy(bullet, 5f);
    }
}
