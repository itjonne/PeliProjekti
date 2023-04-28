using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBulletPowerUp : PowerUp
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
       GiveExtraBullet(character);       
    }

    public void GiveExtraBullet(Character character)
    {
        character.GetComponent<Weapons>()?.ExtraBulletPowerUp(1); // ANtaa yhden ammuksen lisää     
    } 

    private void OnCollisionEnter(Collision collision)
    { 
        Debug.Log("COLLISION WITH " + collision.gameObject);
        if (collision.gameObject.GetComponent<Character>() != null)
        {
            Debug.Log("Löydetttiin character");
            // Annetaan tolle characterille tämä poweruppi
            GivePowerUp(collision.gameObject.GetComponent<Character>());
            Destroy(this.gameObject);
        }
    }
}
