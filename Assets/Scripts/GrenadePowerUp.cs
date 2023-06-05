using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePowerUp : PowerUp
{

    public bool showGrenadeMessage = false;
    GUIStyle messageFont;
    private string grenadeMessage = "Grenades!";

    // Start is called before the first frame update
    void Start()
    {
        messageFont = new GUIStyle();
        messageFont.fontSize = 20;
        messageFont.alignment = TextAnchor.MiddleCenter;
        messageFont.normal.textColor = new Color32(248, 231, 189, 254);


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

    public IEnumerator GrenadeMessage()
    {
        showGrenadeMessage = true;
        yield return new WaitForSeconds(1.25f);
        showGrenadeMessage = false;
        yield return null;

    }

    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("COLLISION WITH " + collision.gameObject);
        if (collision.gameObject.GetComponent<Character>() != null)
        {
            Squad squad = collision.gameObject.GetComponentInParent<Squad>();
            Debug.Log("Löydetttiin kranu");
            StartCoroutine(GrenadeMessage());

            // Annetaan tolle characterille tämä poweruppi
            JSAM.AudioManager.PlaySound(AudioLibSounds.sfx_WoodenBox, transform);
            GivePowerUp(collision.gameObject.GetComponent<Character>());

            Destroy(GetComponent<Collider>());
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            Destroy(this.gameObject, 3f); //tuhotaan viiveellä että coroutine ehtii terminoitua
            
                
        
        }
    }

    private void OnGUI()
    {
        if (showGrenadeMessage)
        {

            GUI.Box(new Rect(Screen.width / 2 - 75, Screen.height / 2 + 150, 120, 30), " ");
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height / 2 + 150, 120, 30), grenadeMessage, messageFont);

        }
    }

}
