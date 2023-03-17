using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squad : MonoBehaviour
{
    [SerializeField] private SquadRuntimeSet squadData;
    private Formation _formation;
    public Formation Formation
    {
        get
        {
            if (_formation == null) _formation = GetComponent<Formation>();
            return _formation;
        }
        set => _formation = value;
    }

    public void Start()
    {
        squadData.Items.Clear(); // tyhjennetään eka
        Formation.Spread = 3f;

        Character[] characters = GameObject.FindObjectsOfType<Character>();
        foreach (Character character in characters)
        {
            Debug.Log(character);
            AddCharacter(character);
        }
        Formation.FormationSize = characters.Length - 1;
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
        List<Vector3> positions = Formation.EvaluatePoints(squadData.Items[0].transform);
        Debug.Log(positions.Count);
        for (int i = 0; i < positions.Count; i++)
        {
            squadData.Items[i + 1].MoveTo(positions[i]);
        }
        /*
        for (int i = 0; i < squadData.Items.Count; i++)
        {
            //character.LogHealth();
            squadData.Items[i].MoveTo();

            // squadData.Items[i].Follow(squadData.Items[0]);
        }
        */
    }
}