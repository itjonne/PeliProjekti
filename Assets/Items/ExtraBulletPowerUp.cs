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
        character.GetComponent<Weapons>()?.ExtraBulletPowerUp(1); // ANtaa yhden ammuksen lis��     
    } 

    private void OnCollisionEnter(Collision collision)
    { 
        Debug.Log("COLLISION WITH " + collision.gameObject);
        if (collision.gameObject.GetComponent<Character>() != null)
        {
            Debug.Log("L�ydetttiin character");
            // Annetaan tolle characterille t�m� poweruppi
            GivePowerUp(collision.gameObject.GetComponent<Character>());
            Destroy(this.gameObject);
        }
    }
}
