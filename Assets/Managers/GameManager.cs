using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        // Peli alkaa

        // Menu

        // Rakenna Squadi

        // Rakenna Mappi
            // Siisti mappi (p‰‰lleik‰kiset kivet pois)


        // OpenMenu/CloseMenu/EndGame/ChangeScene
    }
}