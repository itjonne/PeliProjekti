using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEndKills : MonoBehaviour
{
 
    GUIStyle KillsFont;
    public int killCounter = 0;

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
        /*
        if (enemy.enemyDied == true)
        {
            killCounter++;
        }
        */
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 25, 100, 20), "KILL THEM: " + killCounter, KillsFont);
    }

    void CheckKill()
    {
        //Jos Enemy-Classin Die() -kutsutaan lis�t��n yksi kill counteriin

    }

}