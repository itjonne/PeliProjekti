using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnd : MonoBehaviour
{

    [SerializeField] private Component EndPrefab;
    [SerializeField] private float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEndTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEndTimer()
    {

        yield return new WaitForSeconds(spawnTime); //We wait here to pause between wave spawning
        Debug.Log("SPAWING");
      
        Instantiate(EndPrefab, transform.position, EndPrefab.transform.rotation);
     
    }
}
