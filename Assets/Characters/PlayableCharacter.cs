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
    [SerializeField] private Image healthBar;
    [SerializeField] private Transform canvasTransform;
    private NavMeshAgent agent;
 
    public float movementSpeed;
    public float maxHealth = 30;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Johtaja heittää aina kranaatit yms. Prefabeilla pitää olla grenadethrower pois päältä defaulttina
        if (isLeader == true)
        {
            GetComponent<GrenadeThrower>().enabled = true;         
        }

        else
        {
            GetComponent<GrenadeThrower>().enabled = false;
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

        gameObject.transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
    }

    public override void Follow(Character character)
    {
        if (Vector3.Distance(transform.position, character.transform.position) > 4f )
        {
            transform.position = (Vector3.MoveTowards(transform.position, character.transform.position, movementSpeed * Time.deltaTime));
        }
    }

    public override void MoveTo(Vector3 position)
    {
        agent.destination = position;
        // transform.position = (Vector3.MoveTowards(transform.position, position, movementSpeed * Time.deltaTime));
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

        if (other.gameObject.tag == "Barbwire")
        {
           Debug.Log("osuit piikkilankaan");
            //float damageAmount = other.GetComponent<DamageDealer>().damage;
            //TakeDamage(damageAmount);
            TakeDamage(2);
           movementSpeed = 0.5f;
           
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        UpdateHealthBar();
        if (health <= 0) Die();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "EndObject" && isLeader == true) // VAIHDETTU ETTÄ VAIN AKTIIVINEN PELAAJA VOI VAIHTAA KENTÄN
        {
            // Debug.Log("ENDING GAME");
            // LevelEnd();
            SceneManager.LoadScene(collision.gameObject.GetComponent<LevelEnd>().nextLevel);
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
        Destroy(characterHud, 1f);
        Destroy(gameObject, 30);

        // PÄIVITETTY, TEHDÄÄN SAMA RIMPSU KUN VIHULLE, TUHOTAAN KOMPONENTIT KUOLLESSA ETTÄ SAADAAN ANIMOITUA KUOLEMA JA RUUMIS PYSYY NÄKYVILLÄ 30 SEKUNTIA
    }

    private void LevelEnd()
    {
       // Scene scene = SceneManager.GetActiveScene();
        
       // SceneManager.LoadScene(scene.name);

    }

    //OSSIN HEALTHBAR SEKOILUT
    /*
    void OnGUI()
    {

        var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
         screenPosition.y = Screen.height - screenPosition.y;
        //GUI.Box(new Rect(screenPosition.x - 10, screenPosition.y - 40, healthBarLenght, 20), curHealth + "/" + maxHealth);
        GUI.DrawTexture(new Rect(screenPosition.x - 10, screenPosition.y + 20, 60, 10), lifeBar, ScaleMode.ScaleToFit, true, 10.0F);
    }
    */

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = health / maxHealth;
    }

}
