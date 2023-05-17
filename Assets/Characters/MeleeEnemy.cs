using JSAM;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;


public class MeleeEnemy : Enemy
{
    private Character target;
    [SerializeField] private Transform muzzle;
    private float distanceFromTarget;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] private float attackDistance = 2f;
    [SerializeField] private float attackRate = 1f;
    private float timeSinceLastAttack;
    [SerializeField] private float damage = 10f;

    private Animator animator;
    [SerializeField] private string attackTriggerName = "Attack";
    [SerializeField] private GameObject bladePrefab;

    public float AggroRange = 25f;
  

    public void Awake()
    {
        animator = GetComponent<Animator>();

    }
    
    // Start is called before the first frame update
    void Start()
    {

      //  Aggroed = false;

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
        // transform.position = (Vector3.MoveTowards(transform.position, position, movementSpeed * Time.deltaTime));
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

        timeSinceLastAttack += Time.deltaTime;
        CalculateClosestTarget();

        if (aggroed == true)
        {
            if (target != null)
            {
                CalculateDistanceFromTarget(target);
                // Kun ei viel� pystyt� ly�d�, juostaan kohti pelaajaa
                if (distanceFromTarget > attackDistance)
                {
                    transform.LookAt(target.transform.position);
                    //animator.SetBool(attackTriggerName, false);
                    MoveTo(target.transform.position);
                }
                else // Ollaan tarpeeksi l�hell�
                {
                    transform.LookAt(target.transform.position);
                    if (timeSinceLastAttack >= attackRate)
                    {

                        Attack();
                        gameObject.GetComponent<Anim_Enemy1>().OnMelee(); //haetaan vihujen animaattori skriptist� melee-metodi
                        timeSinceLastAttack = 0;
                    }
                }

            }

            else
            {
                Character[] characters = GameObject.FindObjectsOfType<Character>();
                Debug.Log(characters.Length);

                if(characters.Length > 0)
                {
                    target = characters[Random.Range(0, characters.Length)];
                }
          

            }
        }


    }
    private void Attack()
    {
        Debug.Log("ATTACKING");
        if (target != null && distanceFromTarget <= attackDistance)
        {
            // Vector3 direction = target.transform.position - transform.position;
            // Luodaan veitsi
            /*
            GameObject knife = Instantiate(bladePrefab, transform.position, Quaternion.identity);
            knife.GetComponent<Rigidbody>().velocity = transform.forward * 4f; // TODO ampuu kohti maata
            Destroy(knife, 0.5f); // tuhotaan 0.5s
            Debug.Log(knife);
            */
            //gameObject.GetComponent<Anim_Enemy1>().OnShoot();
            GameObject bullet = Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = (muzzle.forward + new Vector3(0, 0, 0)) * 12f;
            //ammoLeft--;
            //lastShot = Time.time;
            Destroy(bullet, 0.2f);  //T�H�N TEHTY MUUTOKSIA, TOIMII NYT NIINKUIN
            // Suunnataan veitsi pelaajaa kohti
            // knife.transform.rotation = Quaternion.LookRotation(direction);

            // L�hetet��n viesti veitselle, jotta se tiet��, kuinka paljon vahinkoa se aiheuttaa ja mill� et�isyydell� se osuu

        }
    }

}
       /* if (target != null)
        {
            MoveTo(target.transform.position);
            transform.LookAt(target.transform.position);

        } else
        {
            Character[] characters = GameObject.FindObjectsOfType<Character>();
            target = characters[Random.Range(0, characters.Length)];
        } */