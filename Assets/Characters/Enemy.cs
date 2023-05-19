using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using JSAM;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyDataSO enemyData;
    [SerializeField] public float health = 100f;
    [SerializeField] public float movementSpeed = 3f;
    [SerializeField] public GameObject gibs;

    private GameObject playerWhoDealtDamage;
    public bool aggroed = false;

    public float Health => enemyData.health;


    // public float MovementSpeed => enemyData.movementSpeed;

    public abstract void Update();

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

                GetShooterFromBullet(damageDealer);
                gameObject.GetComponent<Anim_Enemy1>().OnDamageTaken(); // Kutsutaan animaattoria
                this.SetHealth(-damageDealer.damage); // Kuolema tapahtuu tuolla p??luokan puolella Enemy-scriptiss?.
                Destroy(damageDealer.gameObject);
                gameObject.GetComponent<Enemy>().aggroed=true; //JOS VIHU OTTAA DAMAGEA, SE AGGROONTUU
            }
        }
    }

    private void GetShooterFromBullet(DamageDealer dd)
    {
        playerWhoDealtDamage = dd.shooter;
    }

    public void SetHealth(int damage)
    {
        health += damage;
        if (health <= -25) GibDeath();  //Jos tulee liikaa damagea, muutetaan vihu punaiseksi usvaksi
        else if (health <= 0) Die();

    }

    public void Die()
    {
        Debug.Log("PLAYERWHOKILLED" + playerWhoDealtDamage);
        //if (playerWhoDealtDamage != null) playerWhoDealtDamage.GetComponent<Character>()?.GainExp(20); // Annetaan taposta expat

        Destroy(GetComponent<Collider>());
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<NavMeshAgent>());
        movementSpeed = 0f;
        gameObject.GetComponent<Enemy>().enabled = false;
        gameObject.GetComponent<Anim_Enemy1>().OnDeath();
        Destroy(gameObject, 20);

        GameManager.Instance.KillEnemy(1); // Lis‰t‰‰n killcounteria

}
 
    public void GibDeath()
    {


       var giblets = gameObject.GetComponent<Enemy>().gibs;
   
       //var giblets = GameObject.FindGanmeObjectsWithTag("Gibs");

        Destroy(Instantiate(giblets.gameObject, transform.position, Quaternion.identity), 20f); //gibletit kohdalle, katoavat 20 sek j‰lkeen
        Destroy(gameObject);
  
    }

}
