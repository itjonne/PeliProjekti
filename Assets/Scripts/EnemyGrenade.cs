using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrenade : MonoBehaviour
{
    [SerializeField] public float delay = 3f; //Kranaatin viive ennen räjähdystä
    [SerializeField] public float radius = 3f; //Kranaatin räjähdysalue
    [SerializeField] public float force = 200f; //Heiton voimakkuus
    [SerializeField] public int ExplosionDamage = 15; //Räjähdyksen tekemä damage




    public GameObject explosionEffect;

    float countdown;
    bool hasExploded = false;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown < 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }
    void Explode()
    {
        //Show effect //ja poistetaan myös objekti 3 sekunnin päästä
        Destroy(Instantiate(explosionEffect, transform.position, Quaternion.identity), 4f);

        //Get nearby objects
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders)
        {
            //Add Force
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
                if (rb.GetComponent<PlayableCharacter>())
                {
                    /*
                    float damageAmount = GetComponent<DamageDealer>().damage;              
                    rb.GetComponent<PlayableCharacter>()?.TakeDamage(damageAmount);
                    */
                    rb.GetComponent<PlayableCharacter>().TakeDamage(ExplosionDamage);
                }

                else if (rb.GetComponent<Enemy>())
                {
               
                    rb.GetComponent<Enemy>().SetHealth(-ExplosionDamage);
                }
            }
        }

        //Damage

        //Remove grenade
        Destroy(gameObject);
    }

}
