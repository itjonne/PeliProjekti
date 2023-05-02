using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollision : MonoBehaviour
{
    public ParticleSystem envImpact;
    public ParticleSystem bloodHit;

    void Start()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject other = collision.GetComponent<Collider>().gameObject;
        if (other.gameObject.CompareTag("Environment"))
        {
            Debug.Log("Osu seinille");

            Destroy(Instantiate(envImpact.gameObject, transform.position, Quaternion.identity), 2f);

            envImpact.GetComponent<ParticleSystem>().Play();
            Destroy(this.gameObject);

        }


        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Osu Vihuun");

            Destroy(Instantiate(bloodHit.gameObject, transform.position, Quaternion.identity), 2f);

            bloodHit.GetComponent<ParticleSystem>().Play();
            Destroy(this.gameObject);
        }


        if (other.gameObject.CompareTag("Player"))
        {


            Destroy(Instantiate(bloodHit.gameObject, transform.position, Quaternion.identity), 2f);

            bloodHit.GetComponent<ParticleSystem>().Play();
            Destroy(this.gameObject);
        }

    }
}
