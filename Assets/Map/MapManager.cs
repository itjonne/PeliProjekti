using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] blocks;
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
        float tileWidth = 100f;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int RandomNum = Random.Range(0, blocks.Length);
                GameObject block = blocks[RandomNum];
                Vector3 position = new Vector3(i * tileWidth, 0, j * tileWidth);
 
                Instantiate(block, position, Quaternion.identity);
            }
        }
    }
}
