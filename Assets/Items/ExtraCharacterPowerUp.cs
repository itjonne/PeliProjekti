using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraCharacterPowerUp : PowerUp
{
    [SerializeField] private GameObject characterPrefab;
    // Start is called before the first frame update
    public override void GivePowerUp(Character character)
    {
        Squad squad = character.GetComponentInParent<Squad>();
        GameObject newCharacter = Instantiate(characterPrefab, transform.position, Quaternion.identity);
        newCharacter.GetComponent<Character>().isLeader = false; // varmuuden vuoks pakotetaan       
        squad.AddCharacter(newCharacter.GetComponent<Character>());

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLISION WITH " + collision.gameObject);
        if (collision.gameObject.GetComponent<Character>() != null)
        {
            // Annetaan tolle characterille tämä poweruppi
            GivePowerUp(collision.gameObject.GetComponent<Character>());
            Destroy(this.gameObject);
        }
    }
}
