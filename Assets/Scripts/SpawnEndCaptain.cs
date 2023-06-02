using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEndCaptain : MonoBehaviour
{
    [SerializeField] private Component EndArrow;
    [SerializeField] private Component EndPrefab;
    GUIStyle KillsFont;
    //public int enemiesKilled = 0;
    //public int killGoal = 1;
    private Character player;


    private void Awake()
    {


    }

    // Start is called before the first frame update
    void Start()
    {

        Character[] characters = GameObject.FindObjectsOfType<Character>();
        player = characters[Random.Range(0, characters.Length)];


    }

    // Update is called once per frame
    void Update()
    {


        if (GameManager.Instance.captainKilled)

        {

            var randomposition = new Vector3(Random.Range(0, 0), 0, Random.Range(0, 0));
            Instantiate(EndPrefab, transform.position + randomposition, EndPrefab.transform.rotation);

            if (player != null)
            {


                // if ( player.isLeader == true)
                {

                    Instantiate(EndArrow, player.transform.position + new Vector3(0, 5, 0), transform.rotation);
                }


            }

            else
            {
                Character[] characters = GameObject.FindObjectsOfType<Character>();
                player = characters[Random.Range(0, characters.Length)];

                Instantiate(EndArrow, player.transform.position + new Vector3(0, 5, 0), transform.rotation);
            }

            Destroy(gameObject);
        }


    }

    private void OnGUI()
    {
        KillsFont = new GUIStyle(GUI.skin.label);
        KillsFont.fontSize = 18;
        KillsFont.normal.textColor = Color.white;
        KillsFont.alignment = TextAnchor.UpperCenter;
        GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 20 - 40, 450, 50), "Bag their captain and get to the exit!", KillsFont);
    }
}
