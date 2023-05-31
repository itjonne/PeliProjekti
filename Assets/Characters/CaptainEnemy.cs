using JSAM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CaptainEnemy : Enemy
{
    private Character target;

    [SerializeField] private Transform muzzle;
    private float distanceFromTarget;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] private float shootingDistance = 20f; // Kuinka kaukaa t?? alkaa ampumaan

    private float timeSinceLastShot;
    public float fireRate = 2f;

    public float EnemySpread = 0.1f;
    public float bulletSpeed = 10f;
    public float AggroRange = 25f;

    private float SHOOTING_BLOCKER_DISTANCE = 2f; // TODO: T�� m��ritt�� miten kaukana pelaajasta se ammunann blockkava asia on maksimissaan. Ei ihan 100% toimi.

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

        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = shootingDistance;
    }

    override public void Die()
    {
        //if (playerWhoDealtDamage != null) playerWhoDealtDamage.GetComponent<Character>()?.GainExp(20); // Annetaan taposta expat
        JSAM.AudioManager.PlaySound(AudioLibSounds.sfx_Meaty, transform);
        Destroy(GetComponent<Collider>());
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<NavMeshAgent>());
        movementSpeed = 0f;
        gameObject.GetComponent<Enemy>().enabled = false;
        gameObject.GetComponent<Anim_Enemy1>().OnDeath();
        Destroy(gameObject, 20);

        GameManager.Instance.KillEnemy(1); // Lis�t��n killcounteria
        GameManager.Instance.KillCaptain();
    }

    private void CalculateDistanceFromTarget(Character target)
    {
        distanceFromTarget = Vector3.Distance(this.transform.position, target.transform.position);
    }

    public void MoveTo(Vector3 position)
    {
        NavMeshMover(position);
        // NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
        // agent.destination = position;
        // TODO: Liiku tietyn matkan p??h?n
        //transform.position = (Vector3.MoveTowards(transform.position, position, movementSpeed * Time.deltaTime));
    }

    private void CalculateClosestTarget()
    {
        Character[] characters = GameObject.FindObjectsOfType<Character>();
        float closestDistance = float.PositiveInfinity;
        Character closestCharacter = null;
        foreach (Character character in characters)
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
                    else // Ollaan tarpeeks l?hell?
                    {
                        // MoveTo(transform.position);

                        transform.LookAt(target.transform.position);

                        // Kurkataan jos jotain on v�liss�, ja liikutaan sit l�hemm�s kunnes voidaan ampua
                        RaycastHit hit;
                        if (Physics.Raycast(muzzle.position, muzzle.forward, out hit, distanceFromTarget - SHOOTING_BLOCKER_DISTANCE, -1))
                        {
                            Debug.Log("EI VOI AMPUA");
                            Debug.DrawRay(muzzle.position, muzzle.forward * hit.distance, Color.yellow);
                            MoveTo(target.transform.position); // TODO: Saattaa bugittaa introlevelint paikallaan olevat vihut
                        }
                        else
                        {
                            if (timeSinceLastShot >= fireRate)
                            {
                                Debug.LogWarning("NYT EI OO");
                                Shoot();
                                timeSinceLastShot = 0;

                            }

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

    private void NavMeshMover(Vector3 targetPos)
    {
        NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
        agent.SetDestination(targetPos); //Don't forget to initiate the first movement.
        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(transform.position, targetPos, NavMesh.AllAreas, path))
        {
            agent.SetPath(path);
        }
        else
        {
            StartCoroutine(Coroutine());
            IEnumerator Coroutine()
            {
                yield return null;
                if (path.status == NavMeshPathStatus.PathComplete)
                {
                    agent.SetPath(path);
                }
            }
        }


    }

    private void Shoot()
    {
        JSAM.AudioManager.PlaySound(AudioLibSounds.sfx_EnemyGun, transform);
        gameObject.GetComponent<Anim_Enemy1>().OnShoot();
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = (muzzle.forward + new Vector3(Random.Range(-EnemySpread, EnemySpread), Random.Range(-EnemySpread, 0), Random.Range(-EnemySpread, EnemySpread))) * bulletSpeed;
        //ammoLeft--;
        //lastShot = Time.time;
        Destroy(bullet, 3f);
    }


}
