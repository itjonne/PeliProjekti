using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMapBlock : MonoBehaviour
{

    //MANUAALINEN MAP GENERAATIO BLOKEILLA
    // Start is called before the first frame update
    public GameObject[] mapBlocks;

    private void Awake()
    {
        GenerateBlock();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateBlock()
    {
        int RandomNum = Random.Range(0, mapBlocks.Length);
        GameObject mapBlock = mapBlocks[RandomNum];

        Instantiate(mapBlock, transform.position, Quaternion.identity);
    }
}
