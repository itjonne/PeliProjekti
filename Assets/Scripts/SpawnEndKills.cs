using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEndKills : MonoBehaviour
{
    [SerializeField] private Component EndArrow;
    [SerializeField] private Component EndPrefab;
    GUIStyle KillsFont;
    public int enemiesKilled = 0;
    public int killGoal = 1;
    private Character player;

    private void Awake()
    {
       

    }

    // Start is called before the first frame update
    void Start()
    {

        player = FindObjectOfType<Character>(); 
      
        
    }

    // Update is called once per frame
    void Update()
    {
        var randomposition = new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));
        if (enemiesKilled >= killGoal)
        {
            
            Instantiate(EndPrefab, transform.position + randomposition, EndPrefab.transform.rotation);

            if (player != null)
            {
                Instantiate(EndArrow, player.transform.position + new Vector3(0, 2, 0), transform.rotation);
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
        GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height /20 -40, 450, 50), "It's greenie season! Kill: " + enemiesKilled + " / " + killGoal + " and get to the exit!", KillsFont);
    }

}