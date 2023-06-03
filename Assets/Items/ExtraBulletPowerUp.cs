using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBulletPowerUp : PowerUp
{
    public bool showBulletMessage = false;
    GUIStyle messageFont;
    private string bulletMessage = "Multishot!";
    // Start is called before the first frame update
    void Start()
    {
        messageFont = new GUIStyle();
        messageFont.fontSize = 20;
        messageFont.alignment = TextAnchor.MiddleCenter;
        messageFont.normal.textColor = new Color32(179, 229, 254, 254);
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

    public IEnumerator BulletMessage()
    {
        showBulletMessage = true;
        yield return new WaitForSeconds(1.25f);
        showBulletMessage = false;
        yield return null;

    }
    private void OnCollisionEnter(Collision collision)
    { 
        Debug.Log("COLLISION WITH " + collision.gameObject);
        if (collision.gameObject.GetComponent<Character>() != null)
        {
            JSAM.AudioManager.PlaySound(AudioLibSounds.sfx_WoodenBox, transform);
            Squad squad = collision.gameObject.GetComponentInParent<Squad>();
            Debug.Log("Löydetttiin kranu");
            StartCoroutine(BulletMessage());
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

    private void OnGUI()
    {

        if (showBulletMessage)
        {

            GUI.Box(new Rect(Screen.width / 2 - 75, Screen.height / 2 + 150, 120, 30), " ");
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height / 2 + 150, 120, 30), bulletMessage, messageFont);

        }
    }
}
