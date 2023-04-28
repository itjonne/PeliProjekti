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
            // jos ei ole manageria, niin kerrotaan ett� t�m� luokka on manageri
            //kerrotaan my�s, ett� t�m� manageri ei saa tuhoutua jos scene vaihtuu
            DontDestroyOnLoad(gameObject);
            manager = this;

        }
        else
        {
            // t�m� ajetaan silloin jos on jo olemassa manageri ja ollaan luomassa toinen manageri, joka on liikaa!
            //t�ll�in t�m� manageri tuhotaan pois, jolloin j�� vain se ensimm�inen
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
