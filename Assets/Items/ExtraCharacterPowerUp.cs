using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraCharacterPowerUp : PowerUp
{
    [SerializeField] private Character newCharacter;
    // Start is called before the first frame update
    public override void GivePowerUp(Character character)
    {
        character.isLeader = false; // varmuuden vuoks pakotetaan
        Squad squad = character.GetComponentInParent<Squad>();
        Instantiate(character, transform.position, Quaternion.identity);
        squad.AddCharacter(newCharacter);
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
