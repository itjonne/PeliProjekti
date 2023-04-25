using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapExperiment : MonoBehaviour
{
    public bool Endable;
    public int MapSize; // tälle 
    public GameObject[] blocks;
    // Start is called before the first frame update
    void Awake()
    {
        GenerateMap(MapSize); // Tekee  mapin
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateMap(int size)
    {
        float tileWidth = 25f; //VAIHDETTU 50 -> 25
        int startBlock = 0;
        // Vikalla rivillä
        int endBlock = Random.Range((size * size) - size + 1, size * size);
        int blockNum = 0;


        // Rullataan tässä se "pelilauta", eka akseli
        for (int i = 0; i < size; i++)
        {
            // Toka akseli
            for (int j = 0; j < size; j++)
            {
                int RandomNum = Random.Range(0, blocks.Length);
                GameObject block = blocks[RandomNum];
                //Vector3 position = new Vector3(i * tileWidth, 0, j * tileWidth);
                Vector3 position = new Vector3(i * tileWidth, 0, j * tileWidth);
                Instantiate(block, position, Quaternion.identity);

                blockNum++;
            }
            blockNum++;
        }
    }
}
