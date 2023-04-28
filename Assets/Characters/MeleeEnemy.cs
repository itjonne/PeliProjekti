using JSAM;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class MeleeEnemy : Enemy
{
    private Character target;
    private float distanceFromTarget;
    [SerializeField] private float attackDistance = 2f;
    [SerializeField] private float attackRate = 1f;
    private float timeSinceLastAttack;
    [SerializeField] private float damage = 10f;

    private Animator animator;
    [SerializeField] private string attackTriggerName = "Attack";
    [SerializeField] private GameObject bladePrefab;

    public void Awake()
    {
        animator = GetComponent<Animator>();
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
        transform.position = (Vector3.MoveTowards(transform.position, position, movementSpeed * Time.deltaTime));
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
        timeSinceLastAttack += Time.deltaTime;
        CalculateClosestTarget();


        if (target != null)
        {
            CalculateDistanceFromTarget(target);
            if (distanceFromTarget > attackDistance)
            {
                animator.SetBool(attackTriggerName, false);
                MoveTo(target.transform.position);
            }
            else // Ollaan tarpeeksi lähellä
            {
                transform.LookAt(target.transform.position);
                if (timeSinceLastAttack >= attackRate)
                {
                    animator.SetBool(attackTriggerName, true);
                    timeSinceLastAttack = 0;
                }
            }

        }
        else
        {
            Character[] characters = GameObject.FindObjectsOfType<Character>();
            target = characters[Random.Range(0, characters.Length)];
        }
    }

    private void Attack()
    {
        if (target != null && distanceFromTarget <= attackDistance)
        {
            // Luodaan veitsi
            GameObject knife = Instantiate(bladePrefab, transform.position, Quaternion.identity);

            // Suunnataan veitsi pelaajaa kohti
            Vector3 direction = target.transform.position - transform.position;
            knife.transform.rotation = Quaternion.LookRotation(direction);

            // Lähetetään viesti veitselle, jotta se tietää, kuinka paljon vahinkoa se aiheuttaa ja millä etäisyydellä se osuu
            Blade blade = knife.GetComponent<Blade>();
            if (blade != null)
            {
                blade.damage = damage;
                blade.attackDistance = attackDistance;
            }
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