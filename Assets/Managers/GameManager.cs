using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Squad))] // Pakotetaan squadi.
public class GameManager : MonoBehaviour
{
    private Squad squad;

    private void Awake()
    {
        squad = GetComponent<Squad>();      
        InitializeSquad();
    }

    private void InitializeSquad()
    {
        Character[] characters = new Character[0]; // Mist‰ n‰‰ kaivetaan
        // squad.InitializeSquad(characters);
    }
    // Rakenna Mappi, ehk‰ mapmanagerin homma.
    // Kasaa squadi, ota jostain m‰‰r‰ (menusta)

}
