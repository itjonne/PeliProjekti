using JSAM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    private Character target;

    public void Awake()
    {
        health = 30f;
    }

    private void OnTriggerEnter(Collider other)
    {
        JSAM.AudioManager.PlaySound(Sounds.sfx_Hitmarker);
        
        // Jos kollisio tapahtuu pelaajan kanssa. Pelaajalla taitaa olla oma handleri, t�ss� vois olla puukotus
        if (other.GetComponent<Character>())
        {
            Debug.Log("NYT OSU");
            
            // Die(); T�H�N VOIS LAITTAA VEITSIANIMAATION
        }

        // Jos osuu johonkin joka tekee damagee, nyt panos
        if (other.GetComponent<DamageDealer>())
        {
            DamageDealer damageDealer = other.GetComponent<DamageDealer>();
            if (damageDealer != null)
            {
                gameObject.GetComponent<Anim_Enemy1>().OnDamageTaken(); // Kutsutaan animaattoria
                this.SetHealth(-damageDealer.damage); // Kuolema tapahtuu tuolla p��luokan puolella Enemy-scriptiss�.
                Destroy(damageDealer.gameObject);
            }
        }
    }

    public void MoveTo(Vector3 position)
    {
        transform.position = (Vector3.MoveTowards(transform.position, position, movementSpeed * Time.deltaTime));
    }

    // Start is called before the first frame update
    void Start()
    {
        Character[] characters = GameObject.FindObjectsOfType<Character>();
        target = characters[Random.Range(0, characters.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            MoveTo(target.transform.position);

        } else
        {
            Character[] characters = GameObject.FindObjectsOfType<Character>();
            target = characters[Random.Range(0, characters.Length)];
        }
    }
}
