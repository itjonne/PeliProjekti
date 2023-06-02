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
            JSAM.AudioManager.PlaySound(AudioLibSounds.sfx_WoodenBox, transform);
            Squad squad = collision.gameObject.GetComponentInParent<Squad>();
            Debug.Log("Löydetttiin kranu");
            StartCoroutine(squad.BulletMessage());
            // Annetaan tolle characterille tämä poweruppi
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
