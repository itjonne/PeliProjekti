using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSAM;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyDataSO enemyData;
    [SerializeField] public float health = 100f;
    [SerializeField] public float movementSpeed = 3f;

    private GameObject playerWhoDealtDamage;

    public float Health => enemyData.health;
    // public float MovementSpeed => enemyData.movementSpeed;

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
        if (health <= 0) Die();
    }

    public void Die()
    {
        Debug.Log("PLAYERWHOKILLED" + playerWhoDealtDamage);
        if (playerWhoDealtDamage != null) playerWhoDealtDamage.GetComponent<Character>()?.GainExp(20); // Annetaan taposta expat

        Destroy(GetComponent<Collider>());
        Destroy(GetComponent<Rigidbody>());
        movementSpeed = 0f;
        gameObject.GetComponent<MeleeEnemy>().enabled = false; //Varmaan v�liaikainen ratkaisu t�ss�, ilman t�t� ruumiit k��ntyilev�t pelaaja kohti vaikka ovat kuolleet
        gameObject.GetComponent<Anim_Enemy1>().OnDeath();
        Destroy(gameObject, 20);
    }
 

}
