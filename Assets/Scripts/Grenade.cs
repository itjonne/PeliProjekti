using JSAM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] public float delay = 3f; //Kranaatin viive ennen r�j�hdyst�
    [SerializeField] public float radius = 5f; //Kranaatin r�j�hdysalue
    [SerializeField] public float force = 200f; //Heiton voimakkuus

    


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
        //Show effect //ja poistetaan my�s objekti 3 sekunnin p��st�
        JSAM.AudioManager.PlaySound(AudioLibSounds.sfx_GrenadeBoom, transform);
        Destroy(Instantiate(explosionEffect, transform.position, Quaternion.identity), 3f);

        //Get nearby objects
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders)
        {
            //Add Force
          Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
                if (rb.GetComponent<Enemy>())
                {
                    rb.GetComponent<Enemy>().SetHealth(-100);
                }
            }
        }
        
        //Damage

        //Remove grenade
        Destroy(gameObject);
    }

   
}
