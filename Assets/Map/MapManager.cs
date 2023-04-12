using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] blocks;
    // Start is called before the first frame update
    void Awake()
    {
        GenerateMap(5); // Tekee 9x9 mapin
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateMap(int size)
    {
        float tileWidth = 50f;
        int startBlock = 0;
        // Vikalla rivill‰
        int endBlock = Random.Range((size * size) - size + 1, size * size);
        int blockNum = 0;

        // Rullataan t‰ss‰ se "pelilauta", eka akseli
        for (int i = 0; i < size; i++)
        {
            // Toka akseli
            for (int j = 0; j < size; j++)
            {
                int RandomNum = Random.Range(0, blocks.Length);
                GameObject block = blocks[RandomNum];
                block.GetComponent<Block>().isStart = false; // T‰‰ on nyt tehty tyhm‰sti, k‰‰nnet‰‰n ne t‰ss‰ falseks
                block.GetComponent<Block>().isEnd = false; // Sama
                Vector3 position = new Vector3(i * tileWidth, 0, j * tileWidth);

                // Jos palikka on alkupalikka, t‰ll‰ hetkell‰ aina palikka numero 0
                if (blockNum == startBlock)
                {
                    block.GetComponent<Block>().isStart = true;          
                }
                // Loppupalikka, otetaan randomilla t‰ll‰ hetkell‰ vikalta rivilt‰
                if (blockNum == endBlock)
                {
                    block.GetComponent<Block>().isEnd = true;
                }
                Instantiate(block, position, Quaternion.identity);

                blockNum++;
            }
            blockNum++;
        }
    }
}
