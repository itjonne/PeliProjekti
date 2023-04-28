using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public float attackDistance = 1.5f;
    public float damage = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Aiheuta pelaajalle vahinkoa, jos pelaaja on tarpeeksi lähellä
            if(Vector3.Distance(transform.position, other.transform.position) <= attackDistance)
            {
                other.GetComponent<PlayableCharacter>().TakeDamage(damage);
            }
        }
    }
    
   

}