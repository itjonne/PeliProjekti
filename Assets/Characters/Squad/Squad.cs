using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squad : MonoBehaviour
{
    [SerializeField] private SquadRuntimeSet squadData;
    private InputHandler _input;
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

    public void Awake()
    {
        _input = GetComponent<InputHandler>();
        squadData.Items.Clear(); // tyhjennet‰‰n eka
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
        Character currentLeader = GetLeader();
        currentLeader.isLeader = false;

        character.isLeader = true;
    }

    public Character GetLeader()
    {
        return squadData.Items.Find(item => item.isLeader);
    }


    // Typer‰sti tehty liikkuminen, ei kannata monesti hakea tota johtajaa.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            int leaderIndex = squadData.Items.IndexOf(GetLeader());
            ChangeLeader(leaderIndex >= squadData.Items.Count - 1 ? squadData.Items[0] : squadData.Items[leaderIndex + 1]);
        }

        // Lasketaan formaation pisteet
        List<Vector3> positions = Formation.EvaluatePoints(GetLeader().transform);

        // Tehd‰‰n typer‰ v‰lilista ku en keksi nyt muuta
        List<Character> followers = new List<Character>();

        // lis‰t‰‰n listaan followerit
        foreach (Character character in squadData.Items)
        {
            if (!character.isLeader) followers.Add(character);
        }

        // T‰‰ k‰‰ntˆ teki t‰st‰ v‰h‰n siistimm‰n
        followers.Reverse();

        // Siirret‰‰n hahmot oikeeseen paikkan
        for (int i = 0; i < positions.Count; i++)
        {

            followers[i].MoveTo(positions[i]);
            followers[i].RotateTo(GetLeader());
        }

        // Ampuminen
        if (Input.GetMouseButton(0))
        {
            GetLeader().GetComponent<Weapon>()?.Shoot(GetLeader().transform);
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


    // UI k‰pistely
    private void OnGUI()
    { 
        for (int i = 0; i < squadData.Items.Count; i++)
        {
            GUI.contentColor = squadData.Items[i].isLeader ? Color.red : Color.green; // muutetaan v‰ri‰
            GUI.Label(new Rect(10 , 10 + (i * 60), 100, 20), squadData.Items[i].Name);
            GUI.Label(new Rect(10, 30 + (i * 60), 100, 20), "Health: " + squadData.Items[i].Health.ToString());
            GUI.Label(new Rect(10, 50 + (i * 60), 100, 20), "Level: " + squadData.Items[i].Level.ToString());
        }

    }
}