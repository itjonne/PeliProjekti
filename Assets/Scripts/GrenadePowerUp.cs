using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePowerUp : PowerUp
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void GivePowerUp(Character character)
    {
        GiveGrenade(character);
    }


    public void GiveGrenade(Character character)
    {
        character.GetComponentInParent<Squad>().grenadeAmount+=5;
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("COLLISION WITH " + collision.gameObject);
        if (collision.gameObject.GetComponent<Character>() != null)
        {
            Squad squad = collision.gameObject.GetComponentInParent<Squad>();
            Debug.Log("Löydetttiin kranu");
            StartCoroutine(squad.GrenadeMessage());

            // Annetaan tolle characterille tämä poweruppi
            JSAM.AudioManager.PlaySound(AudioLibSounds.sfx_WoodenBox, transform);
            GivePowerUp(collision.gameObject.GetComponent<Character>());

            Destroy(GetComponent<Collider>());
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            Destroy(this.gameObject, 3f);
            
                
        
        }
    }

}
