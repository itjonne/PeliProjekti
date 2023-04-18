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
        if (other.GetComponent<Character>())
        {
            Debug.Log("NYT OSU");
            Destroy(this.gameObject);
        }
        if (other.GetComponent<DamageDealer>())
        {
            DamageDealer damageDealer = other.GetComponent<DamageDealer>();
            if (damageDealer != null)
            {

            this.SetHealth(-damageDealer.damage);
            Destroy(damageDealer.gameObject);
            }
        }
    }

    public void MoveTo(Vector3 position)
    {
        transform.position = (Vector3.MoveTowards(transform.position, position, MovementSpeed * Time.deltaTime));
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
