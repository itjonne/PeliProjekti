using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager manager;


    private void Awake()
    {
        //singleton
        // Tarkistetaan, onko manageria jo olemassa
        if (manager == null)
        {
            // jos ei ole manageria, niin kerrotaan että tämä luokka on manageri
            //kerrotaan myös, että tämä manageri ei saa tuhoutua jos scene vaihtuu
            DontDestroyOnLoad(gameObject);
            manager = this;

        }
        else
        {
            // tämä ajetaan silloin jos on jo olemassa manageri ja ollaan luomassa toinen manageri, joka on liikaa!
            //tällöin tämä manageri tuhotaan pois, jolloin jää vain se ensimmäinen
            Destroy(gameObject);

        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
