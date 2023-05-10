using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.AI;

public class MapManager : MonoBehaviour
{
    public bool Endable;
    public int MapSize; // t‰lle 
    public GameObject[] blocks;
    private Vector3 startPosition;
    // Start is called before the first frame update
    void Awake()
    {
        GenerateMap(MapSize); // Tekee  mapin
        BakeMap();
    }

    private void Start()
    {
        // Laitetaan squadi startblockille
        Squad squad = FindObjectOfType<Squad>();
        if (squad != null)
        {
            if (startPosition != null) squad.SetSquadPosition(startPosition);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BakeMap()
    {
        /*
        NavMeshBuilder.ClearAllNavMeshes();
        NavMeshBuilder.BuildNavMesh();
        */
        //OTETAAN NƒMƒ POIS KƒYT÷STƒ KUN NAVMESHCOMPONENTS KƒYT÷SSƒ
    }

    public void GenerateMap(int size)
    {
        float tileWidth = 25f; //VAIHDETTU 50 -> 25
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
                    startPosition = position;
                }
                // Loppupalikka, otetaan randomilla t‰ll‰ hetkell‰ vikalta rivilt‰
                if (blockNum == endBlock && Endable == true)
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
