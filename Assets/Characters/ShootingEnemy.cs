using JSAM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootingEnemy : Enemy
{
    private Character target;
    [SerializeField] private Transform muzzle;
    private float distanceFromTarget;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] private float shootingDistance = 20f; // Kuinka kaukaa t�� alkaa ampumaan

    private float timeSinceLastShot;
    public float fireRate = 2f;

    public float EnemySpread = 0.1f;
    public float bulletSpeed = 10f;
    public float AggroRange = 25f;
  

    public void Awake()
    {
       
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
        NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
        agent.destination = position;
        // TODO: Liiku tietyn matkan p��h�n
        //transform.position = (Vector3.MoveTowards(transform.position, position, movementSpeed * Time.deltaTime));
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
    public override void Update()
    {
        if (target != null)
        {
            if ((transform.position - target.transform.position).magnitude < AggroRange)
            {
                aggroed = true;
            }
        }




        timeSinceLastShot += Time.deltaTime;
        CalculateClosestTarget();

        if (aggroed == true)
        {
            if (target != null)
            {
                {
                    CalculateDistanceFromTarget(target);
                    if (distanceFromTarget > shootingDistance)
                    {
                        transform.LookAt(target.transform.position);
                        MoveTo(target.transform.position);
                    }
                    else // Ollaan tarpeeks l�hell�
                    {
                        transform.LookAt(target.transform.position);
                        if (timeSinceLastShot >= fireRate)
                        {
                            Shoot();
                            timeSinceLastShot = 0;
                        }
                    }
                }


            }

            else
            {
                Character[] characters = GameObject.FindObjectsOfType<Character>();
           
                if (characters.Length > 0)
                {
                    target = characters[Random.Range(0, characters.Length)];
                }
            }
        }


    }

    private void Shoot()
    {
        gameObject.GetComponent<Anim_Enemy1>().OnShoot();
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = (muzzle.forward + new Vector3(Random.Range(-EnemySpread, EnemySpread), Random.Range(-EnemySpread, 0), Random.Range(-EnemySpread, EnemySpread))) * bulletSpeed;
        //ammoLeft--;
        //lastShot = Time.time;
        Destroy(bullet, 3f);
    }


}
