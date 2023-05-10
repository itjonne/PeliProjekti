using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Character player; // Sama systeemi nyt kun vihu prefabeilla
    public GameObject[] enemyPrefabs;
    [SerializeField] private float xPosition;
    [SerializeField] private float zPosition;

    [SerializeField] private float spawnTime;
    [SerializeField] private float spawnTimeReduce;
    [SerializeField] private float horizontalRandom;
    [SerializeField] private float verticalRandom;


    public bool active;
    public float deactiveRange;

    // Start is called before the first frame update
    void Start()
    {
        Character[] characters = GameObject.FindObjectsOfType<Character>();
        player = characters[Random.Range(0, characters.Length)];
       // player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SpawnEnemy());

        StartCoroutine(SpawnTimerReduce());
    }

    private void Update()
    {

      if (player != null)
        {
            if ((transform.position - player.transform.position).magnitude < deactiveRange ) //Spawnerit nyt paikallaan, jos pelaaja liian l�hell�, spawneri ei toimi
            {
                active = false;
            }
            else
            {
                active = true;
            }
        }

        else
        {
            Character[] characters = GameObject.FindObjectsOfType<Character>();
            player = characters[Random.Range(0, characters.Length)];
        }
                 
    }

    // Update is called once per frame
    IEnumerator SpawnEnemy()
    {

        yield return new WaitForSeconds(spawnTime); //We wait here to pause between wave spawning
        Debug.Log("SPAWING");
        var randomposition = new Vector3(Random.Range(-horizontalRandom, horizontalRandom), 0, Random.Range(-verticalRandom, verticalRandom));  // Vihut syntyv�t random et�isyydelle spawnerista

        if (active == true)
        {
            int RandomNum = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyPrefab = enemyPrefabs[RandomNum];
            enemyPrefab.GetComponent<Enemy>().aggroed = true; //JOS VIHU PREFAB SYNTYY SPAWNERISTA, AGGRO P��LLE HETI DEFAULTTINA
            Instantiate(enemyPrefab, transform.position + randomposition, enemyPrefab.transform.rotation);
            
        }

        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnTimerReduce()   // Kuinka kauan kest�� ett� spawnaus-aika pienenee yhdell�. pienin mahd. aikav�li 2 sekuntia
    {
        yield return new WaitForSeconds(spawnTimeReduce);
        spawnTime--;
        if (spawnTime > 2)
        {
            
            StartCoroutine(SpawnTimerReduce());
        }
     
    }
}
