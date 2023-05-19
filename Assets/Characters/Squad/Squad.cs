using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Squad : MonoBehaviour
{
    [SerializeField] private SquadRuntimeSet squadData;
    [SerializeField] public int grenadeAmount = 3;
    private InputHandler _input;
    private Formation _formation;
    private int formationSize;
    private List<Vector3> positions;
    private Camera camera;

    GUIStyle largeFont;

    private Vector3 leaderLastPos;
    public Formation Formation
    {
        get
        {
            if (_formation == null)
            {
                Formation[] formations = GetComponents<Formation>();
                foreach (Formation formation in formations)
                {
                    if (formation.enabled == true) _formation = formation;
                }
            }
            return _formation;
        }
        set => _formation = value;
     }       
    

    public void Awake()
    {
        // Jos skenejen v‰lill‰ menee joku paskaks ja siel‰ on jo squadi
        if (GameObject.FindObjectsOfType<Squad>().Length > 1)
        {
            Debug.LogWarning("SIELLƒ OLI JO SQUADI");
            Destroy(GameObject.FindObjectOfType<Squad>().gameObject);
        }
        
        // Sit jatkuu

            DontDestroyOnLoad(this); // T‰‰ saattaa pit‰‰ 

            _input = GetComponent<InputHandler>();
            squadData.Items.Clear(); // tyhjennet‰‰n eka

            InitializeSquad();
            InitializeFormation();        
        
    }

    public void Start()
    {
        camera = GameObject.FindObjectOfType<Camera>();
        /*
        // Laitetaan squadi oikeeseen paikkaan
        Block[] blocks = FindObjectsOfType<Block>();
        Block startBlock = Array.Find(blocks, block => block.isStart);
        Debug.Log(startBlock.transform.position);
        if (startBlock) SetSquadPosition(startBlock.transform.position);
        */

        largeFont = new GUIStyle();
        largeFont.fontSize = 16;
        largeFont.normal.textColor = Color.white;
    }

    private int GetSquadSize()
    {
        return squadData.Items.Count;
    }

    public void InitializeFormation()
    {
        Formation.FormationSize = GetSquadSize();
        positions = Formation.EvaluatePoints(GetLeader().transform);
    }

    public void UpdateFormation() {

        Formation[] formations = GetComponents<Formation>();
        foreach (Formation formation in formations)
        {
            if (formation.enabled == true)
            {
                Formation = formation;
                InitializeFormation(); 
            }
        }
    }

   

    public void InitializeSquad()
    {
        List<Character> characters = new List<Character>();
        if (squadData.Items.Count > 0)
        {
            foreach(Character character in squadData.Items)
            {
                characters.Add(character);
            }
        } else
        {
            characters.AddRange(GameObject.FindObjectsOfType<Character>());
        }
        // LIs‰t‰‰n hahmot listaan
        foreach (Character character in characters)
        {
            AddCharacter(character);
        }

        // Asetetaan johtaja
        foreach (Character character in characters)
        {
            if (character.isLeader) ChangeLeader(character);
        }
    }

    public void SetSquadPosition(Vector3 newPosition)
    {
        Debug.LogWarning("SETTING START TO " + newPosition);
        Debug.Log(squadData.Items);
        Debug.LogWarning(squadData.Items.Count);
        foreach (Character character in squadData.Items)
        {
            if (character.GetComponent<NavMeshAgent>())
            {
                Debug.LogWarning("NAVMESHAGENT");
                character.GetComponent<NavMeshAgent>().Warp(newPosition);
            } else
            {

            character.transform.position = newPosition;
            }
        }
    }

    // Start is called before the first frame update
    public void AddCharacter(Character character)
    {
        squadData.Add(character);
        Formation.FormationSize += 1;
        character.gameObject.transform.parent = this.transform; // asetetaan kivasti siell‰ n‰kym‰ss‰ siihen ryhm‰‰n
    }

    public void RemoveCharacter(Character character)
    {

        squadData.Remove(character);

        /*
        Character[] characters = GameObject.FindObjectsOfType<Character>();
        if (characters.Length > 0)
        {
            ChangeLeader(characters[0]);
        } else
        {
            SceneManager.LoadScene(0);
        }
        */
    }

    public void DestroyCharacter(Character character)
    {
        character.ChangeLeader(false);

        RemoveCharacter(character);
        Debug.Log("DESTROYING");
        
    }

    public void ChangeLeader(Character character)
    {
        Character currentLeader = GetLeader();
        currentLeader.ChangeLeader(false);

        character.ChangeLeader(true);
       
    }

    public Character GetLeader()
    {
        Character leader = squadData.Items.Find(item => item.isLeader);
        if (leader == null)
        {
            // Jos kaikki kuolee
            if (squadData.Items.Count == 0)
            {
                //EndGame(); // T‰h‰n vois laittaa sit mainmenun
                GameManager.Instance.GameOver(); //WIP
                // Destroy(this.gameObject);
                // Scene scene = SceneManager.GetActiveScene();
                // SceneManager.LoadScene(scene.name);
            }

            squadData.Items[0].ChangeLeader(true);
            return squadData.Items[0];

        }
        return leader;
    }

    private void EndGame()
    {
       // UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
        //GameManager.GoToMenu();??  //OSSI!
   
    }

    // Typer‰sti tehty liikkuminen, ei kannata monesti hakea tota johtajaa.
    private void Update()
    {
        //Squadi pysyy mutta kameraa luodaan uusiksi TODO turha tehd‰ joka framella
        if (camera == null)

        {
            camera = GameObject.FindObjectOfType<Camera>();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            int leaderIndex = squadData.Items.IndexOf(GetLeader());
            ChangeLeader(leaderIndex >= squadData.Items.Count - 1 ? squadData.Items[0] : squadData.Items[leaderIndex + 1]);
        }

        // Debug.Log(GetLeader().GetComponent<Rigidbody>().velocity.magnitude);
        // Lasketaan formaation pisteet
        // Jos pelaajalla on liikett‰, tee t‰‰
        Vector3 currentPos = GetLeader().transform.position; // Kurkataan miss‰ johtaja on

        // EI ihan 100% toimi
        if ( positions == null || currentPos != leaderLastPos)
        {
            positions = Formation.EvaluatePoints(GetLeader().transform);

        }
        leaderLastPos = currentPos; // Johtaja on t‰ss‰ ja t‰t‰ verrataan seuraavalla freimill‰

        // Tehd‰‰n typer‰ v‰lilista ku en keksi nyt muuta
        List<Character> followers = new List<Character>();
        // lis‰t‰‰n listaan followerit
        foreach (Character character in squadData.Items)
        {
            if (!character.isLeader) followers.Add(character);
        }


        // T‰‰ k‰‰ntˆ teki t‰st‰ v‰h‰n siistimm‰n
        followers.Reverse();

        // Ampuminen
        if (Input.GetMouseButton(0))
        {
            GetLeader().GetComponent<Weapons>()?.Shoot(GetLeader().transform);
            foreach (Character follower in followers) {
                follower.GetComponent<Weapons>()?.Shoot(GetLeader().transform);
            }
        }
        // Siirret‰‰n hahmot oikeeseen paikkan
        for (int i = 0; i < positions.Count; i++)
        {
            if (i < followers.Count)
            {
                followers[i].MoveTo(positions[i]);

                Ray ray = camera.ScreenPointToRay(_input.MousePosition);

                if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
                {
                    var target = hitInfo.point;
                    target.y = followers[i].transform.position.y;
                    followers[i].transform.LookAt(target);
                }
                //followers[i].RotateTowards(screenPos);
                //followers[i].RotateTo(GetLeader());
            }
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
            GUI.Label(new Rect(10, 25 + (i * 60), 100, 20), "Health: " + squadData.Items[i].health.ToString());
            GUI.Label(new Rect(10, 40 + (i * 60), 100, 20), "Level: " + squadData.Items[i].level.ToString());
            
        }

        GUI.contentColor = Color.white;
        
        GUI.Label(new Rect(100, 10, 100, 20), "Grenade: " + grenadeAmount.ToString(), largeFont);
        GUI.Label(new Rect(200, 10, 200, 20), "Formation: " + Formation.formationName);
    }
}