using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squad : MonoBehaviour
{
    [SerializeField] private SquadRuntimeSet squadData;

    public void Start()
    {
        squadData.Items.Clear(); // tyhjennetään eka

        Character[] characters = GameObject.FindObjectsOfType<Character>();
        foreach (Character character in characters)
        {
            Debug.Log(character);
            AddCharacter(character);
        }
        foreach(Character character in characters)
        {
            if (character.isLeader) ChangeLeader(character);
        }
        
    }

    // Start is called before the first frame update
    public void AddCharacter(Character character)
    {
        squadData.Add(character);
    }

    public void RemoveCharacter(Character character)
    {
        squadData.Remove(character);
    }

    public void ChangeLeader(Character character)
    {
        int index = squadData.Items.IndexOf(character);

        if (index > 0) squadData.Swap(0, index); // Vaihdetaan jos ei oo johtaja + jos löytyy
        else Debug.LogError($"Nyt meni pieleen, yritettiin vaihtaa {character} indeksissä {index}");
    }

    private void Update()
    {
        for (int i = 0; i < squadData.Items.Count; i++)
        {
            //character.LogHealth();
            // squadData.Items[i].Move();
            squadData.Items[i].Follow(squadData.Items[0]);
        }
    }
}