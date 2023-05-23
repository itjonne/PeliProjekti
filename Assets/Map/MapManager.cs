using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.AI;

public class MapManager : MonoBehaviour
{
    public bool Endable;
    public int MapSize; // tälle 
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
    
        //GameObject.FindGameObjectsWithTag("Enemy");

        if (squad != null)
        {
            if (startPosition != null) squad.SetSquadPosition(startPosition);
        }

        CleanUpStart();
     
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
        //OTETAAN NÄMÄ POIS KÄYTÖSTÄ KUN NAVMESHCOMPONENTS KÄYTÖSSÄ
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
                block.GetComponent<Block>().isStart = false; // Tää on nyt tehty tyhmästi, käännetään ne tässä falseks
                block.GetComponent<Block>().isEnd = false; // Sama
                Vector3 position = new Vector3(i * tileWidth, 0, j * tileWidth);

                // Jos palikka on alkupalikka, tällä hetkellä aina palikka numero 0
                if (blockNum == startBlock)
                {
                    block.GetComponent<Block>().isStart = true;
                    startPosition = position;
                }
                // Loppupalikka, otetaan randomilla tällä hetkellä vikalta riviltä
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

    //Putsataan Aloituspositio vihuista ympäristöstä yms.
    public void CleanUpStart()
    {

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponent<Enemy>().aggroed = false; //SHOOTTERIT LÄHTI BATTLEMENTSEISTA JUOKSEMAAN NIIN SIKSI TÄMÄ
            float range = Vector3.Distance(enemy.transform.position, startPosition);
            if(range < 25)
            {
                Destroy(enemy);
            }

        }

        foreach (GameObject env in GameObject.FindGameObjectsWithTag("Environment"))
        {
            float range = Vector3.Distance(env.transform.position, startPosition);
            if (range < 4)
            {
                Destroy(env);
            }

        }

    }

}
