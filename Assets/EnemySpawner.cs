using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject player; // T‰‰ nyt on ehk‰ v‰h‰n typer‰, ottaa kopin pelaajista ja liikuttaa
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float xPosition;
    [SerializeField] private float zPosition;

    [SerializeField] private float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SpawnEnemy());
    }

    private void Update()
    {
        if (player != null)
        {
            transform.position = player.transform.position + new Vector3(xPosition, 0, zPosition); // Testi

        } else
        {
            player = GameObject.FindGameObjectWithTag("Player");
            transform.position = player.transform.position + new Vector3(xPosition, 0, zPosition); // Testi
        }
    }

    // Update is called once per frame
    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(spawnTime); //We wait here to pause between wave spawning
        Debug.Log("SPAWING");
        Instantiate(enemyPrefab, transform.position, enemyPrefab.transform.rotation);
        StartCoroutine(SpawnEnemy());
    }
}
