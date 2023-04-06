using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] blocks;
    // Start is called before the first frame update
    void Start()
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
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int RandomNum = Random.Range(0, blocks.Length);
                GameObject block = blocks[RandomNum];
                block.GetComponent<Block>().isStart = false; // T‰‰ on nyt tehty tyhm‰sti, k‰‰nnet‰‰n ne t‰ss‰ falseks
                block.GetComponent<Block>().isEnd = false; // Sama
                Vector3 position = new Vector3(i * tileWidth, 0, j * tileWidth);

                if (blockNum == startBlock)
                {
                    Debug.Log("STARTING POSITION");
                    Debug.Log(position);
                    block.GetComponent<Block>().isStart = true;
                }
                if (blockNum == endBlock)
                {
                    Debug.Log("Ending POSITION");
                    Debug.Log(position);
                    block.GetComponent<Block>().isEnd = true;
                }


                Instantiate(block, position, Quaternion.identity);

                blockNum++;
            }
            blockNum++;
        }
    }
}
