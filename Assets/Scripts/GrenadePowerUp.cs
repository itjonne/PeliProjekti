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
            Debug.Log("Löydetttiin kranu");

            // Annetaan tolle characterille tämä poweruppi
            GivePowerUp(collision.gameObject.GetComponent<Character>());
            Destroy(this.gameObject);
        }
    }

}
