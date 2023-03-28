using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
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
