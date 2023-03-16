using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadController : MonoBehaviour
{
    [SerializeField] private SquadRuntimeSet squadData;
    // Start is called before the first frame update
    public void AddCharacter(GameObject character)
    {
        squadData.Add(character);
    }

    public void RemoveCharacter(GameObject character)
    {
        squadData.Remove(character);
    }

    public void ChangeLeader(GameObject character)
    {
        int index = squadData.Items.IndexOf(character);

        if (index > 0) squadData.Swap(0, index); // Vaihdetaan jos ei oo johtaja + jos l�ytyy
        else Debug.LogError($"Nyt meni pieleen, yritettiin vaihtaa {character} indeksiss� {index}");
    }

    private void Update()
    {
        foreach (var character in squadData.Items)
        {
            Character c = character.GetComponent<Character>(); // TODO: Mites t�n sotkun selvitt�is
            c.LogHealth();
            c.Move();
        }
    }
}