using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject player; // Tää nyt on ehkä vähän typerä, ottaa kopin pelaajista ja liikuttaa
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float xPosition;
    [SerializeField] private float zPosition;

    [SerializeField] private float spawnTime;
    [SerializeField] private float spawnTimeReduce;
    [SerializeField] private float horizontalRandom;
    [SerializeField] private float verticalRandom;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SpawnEnemy());

        StartCoroutine(SpawnTimerReduce());
    }

    private void Update()
    {
        if (player != null)
        {
            transform.position = player.transform.position + new Vector3(xPosition, 0, zPosition); // Testi

        } else
        {
            player = GameObject.FindGameObjectWithTag("Player");
            
        }
    }

    // Update is called once per frame
    IEnumerator SpawnEnemy()
    {

        yield return new WaitForSeconds(spawnTime); //We wait here to pause between wave spawning
        Debug.Log("SPAWING");
        var randomposition = new Vector3(Random.Range(-horizontalRandom, horizontalRandom), 0, Random.Range(-verticalRandom, verticalRandom));  // Vihut syntyvät random etäisyydelle spawnerista
        Instantiate(enemyPrefab, transform.position + randomposition, enemyPrefab.transform.rotation);
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnTimerReduce()   // Kuinka kauan kestää että spawnaus-aika pienenee yhdellä. pienin mahd. aikaväli 2 sekuntia
    {
        yield return new WaitForSeconds(spawnTimeReduce);
        spawnTime--;
        if (spawnTime > 2)
        {
            
            StartCoroutine(SpawnTimerReduce());
        }
     
    }
}
