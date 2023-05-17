using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEndKills : MonoBehaviour
{
    public Enemy enemy;
    GUIStyle KillsFont;
    public int enemiesKilled = 0;

    private void Awake()
    {
       

    }

    // Start is called before the first frame update
    void Start()
    {

     
        KillsFont = new GUIStyle();
        KillsFont.fontSize = 16;
        KillsFont.normal.textColor = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        
       
        
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 25, 100, 20), "KILL THEM: " + enemiesKilled, KillsFont);
    }

    void CheckKill()
    {
        //Jos Enemy-Classin Die() -kutsutaan lis‰t‰‰n yksi kill counteriin

    }

}