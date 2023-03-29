using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GenerateMap(9); // Tekee 9x9 mapin
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateMap(int size)
    {
        float tileWidth = 10f;
        Tile[] tiles = { new RedTile(), new BlueTile(), new TestTile() }; // T‰‰ vaan testi

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int RandomNum = Random.Range(0, tiles.Length);
                Tile tile = tiles[RandomNum];
                Vector3 position = new Vector3(i * tileWidth, 0, j * tileWidth);
                Debug.Log(tile.tileLocation);
                Instantiate(tile.tilePrefab, position, Quaternion.identity);
            }
        }
    }
}
