using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using System.Runtime.CompilerServices;
using UnityEditor;

public class PlayableCharacter : Character
{
    [SerializeField] private GameObject characterHud;
    [SerializeField] public Text grenadeHud;
    [SerializeField] private Image healthBar;
    [SerializeField] private Transform canvasTransform;
    public NavMeshAgent agent;
 
    public float movementSpeed;
    public float speedMultiplier;
    public float maxHealth = 30;
    private Squad squad;

    // Start is called before the first frame update
    void Start()
    {
  
        agent = GetComponent<NavMeshAgent>();
        squad = GetComponentInParent<Squad>(); //TÄMÄ TÄYTYY ALUSTAA KRANUHUDIN TAKIA
    }

    // Update is called once per frame
    void Update()
    {
        //Johtaja heittää aina kranaatit yms. Prefabeilla pitää olla grenadethrower pois päältä defaulttina
        if (isLeader == true)
        {
            GetComponent<GrenadeThrower>().enabled = true;
            grenadeHud.text = squad.grenadeAmount.ToString();
        }

        else
        {
            GetComponent<GrenadeThrower>().enabled = false;
            grenadeHud.text = " ";
        }
    }

    //TÄMÄ TEHTY ETTÄ PELAAJAN "HUDI-CANVAS" KATSOO KAMERAAN KOKOAJAN
    private void LateUpdate()
    {
        canvasTransform.LookAt(transform.position + Camera.main.transform.forward);
    }

    public override void ChangeLeader(bool leader)
    {
        this.isLeader = leader;
        GetComponent<NavMeshAgent>().enabled = !leader; // componentti disabloidaan jos on leader
    }

    public override void Move()
    {

        gameObject.transform.Translate(Vector3.forward * Time.deltaTime * agent.speed);
    }

    public override void Follow(Character character)
    {
        if (Vector3.Distance(transform.position, character.transform.position) > 4f )
        {
            transform.position = (Vector3.MoveTowards(transform.position, character.transform.position, agent.speed * Time.deltaTime));
        }
    }

    public override void MoveTo(Vector3 position)
    {
        NavMeshMover(position);
        //agent.destination = position;
        // transform.position = (Vector3.MoveTowards(transform.position, position, movementSpeed * Time.deltaTime));
    }

    private void NavMeshMover(Vector3 targetPos)
    {
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

    // Tää vois olla hiiri
    public override void RotateTowards(Vector3 point)
    {
        transform.LookAt(point);
    }

    public override void RotateTo(Character target)
    {
        transform.rotation = target.transform.rotation;

    }

    public override void Attack(Vector3 direction)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DamageDealer>())
        {
            Debug.Log("NYT OSU PUUKKO / PANOS");
            float damageAmount = other.GetComponent<DamageDealer>().damage;
            TakeDamage(damageAmount);
        }

        if (other.GetComponent<Hinderer>())
        {
            Debug.Log("osuit piikkilankaan");
            SpeedMultiplier(other.GetComponent<Hinderer>().speedMultiplier);
            ChangeAgentSpeed();
            //float damageAmount = other.GetComponent<DamageDealer>().damage;
            //TakeDamage(damageAmount);
            // TakeDamage(2);
        }

        /*
        if (other.gameObject.tag == "Barbwire")
        {

           Debug.Log("osuit piikkilankaan");
            SpeedMultiplier(0.2f);
            ChangeAgentSpeed();
            //float damageAmount = other.GetComponent<DamageDealer>().damage;
            //TakeDamage(damageAmount);
            TakeDamage(2);
        }
        */
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Hinderer>())
        {
            speedMultiplier = 1f;
            ChangeAgentSpeed();
            Debug.Log("EXIT");
        }
  
    }

    public void ChangeAgentSpeed()
    {
        agent.speed = movementSpeed * speedMultiplier;
        Debug.Log("AGENT SPEED: " + movementSpeed * speedMultiplier);
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        UpdateHealthBar();
        if (health <= 0) Die();
    }

    public void SpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "EndObject" && isLeader == true) // VAIHDETTU ETTÄ VAIN AKTIIVINEN PELAAJA VOI VAIHTAA KENTÄN
        {
            // Debug.Log("ENDING GAME");
            // LevelEnd();
            GameManager.Instance.levelFinished = true;
           // SceneManager.LoadScene(collision.gameObject.GetComponent<LevelEnd>().nextLevel);
        }

        if (collision.gameObject.tag == "Enemy") {
            // TakeDamage(10);
            
         }

        if (collision.gameObject.GetComponent<DamageDealer>() != null)
        {
            float damageAmount = collision.gameObject.GetComponent<DamageDealer>().damage;
            TakeDamage(damageAmount);
        }
    }


    private void Die()
    {
        Debug.Log("DEATH");
        gameObject.GetComponentInParent<Squad>().DestroyCharacter(this);

        Destroy(GetComponent<Collider>());
        Destroy(GetComponent<Rigidbody>());
        movementSpeed = 0f;
        Destroy(GetComponent<PlayableCharacter>());
        Destroy(GetComponent<GrenadeThrower>());
        gameObject.GetComponent<Weapons>().enabled = false;
        gameObject.GetComponent<Animation_Soldier>()?.OnDeath(); // Tää on nyt vähän spagetti, tän ei tarviis ymmärtää mitään squadista/animaatiosta
        gameObject.GetComponent<Animation_Soldier>().enabled = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        Destroy(grenadeHud);
        Destroy(characterHud, 1.2f); //poistetaan pienellä viiveellä hudi, näkyy että palkki on tyhjä
        Destroy(gameObject, 30);

        // PÄIVITETTY, TEHDÄÄN SAMA RIMPSU KUN VIHULLE, TUHOTAAN KOMPONENTIT KUOLLESSA ETTÄ SAADAAN ANIMOITUA KUOLEMA JA RUUMIS PYSYY NÄKYVILLÄ 30 SEKUNTIA
    }

    private void LevelEnd()
    {
       // Scene scene = SceneManager.GetActiveScene();
        
       // SceneManager.LoadScene(scene.name);

    }

 
    private void UpdateHealthBar()
    {
        healthBar.fillAmount = health / maxHealth;
    }

}
