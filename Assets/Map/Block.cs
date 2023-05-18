using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // N�it� vois sit t�st� vaan randomisoida
    public GameObject[] environmentObjects; // Puut jne.
    private List<GameObject> generatedObjects = new List<GameObject>();
    [Range(0, 25)] public float environmentDensity;

    public GameObject[] helperObjects; // Healthit jne.
    [Range(0, 25)] public float helperDensity;

    public GameObject[] enemyObjects; // Vihut
    [Range(0, 25)] public float enemyDensity;

    public GameObject endObject;
    public GameObject startObject;
    public MapManager mapManager;

    public float width; // 100
    public float height; // 100
    public bool isStart = false;
    public bool isEnd = false;
    [Range(0, 25)] public float EnvSpawnRange = 5f;

    private void Awake()
    {
        
    }

    private void Start()
    {
        foreach (var obj in generatedObjects)
        {
            Destroy(obj);
        }

        GetPlaneSize();
        GenerateEnvironment();
        GenerateHelpers();
        GenerateEnemies(); 
        if (isEnd) GenerateEnd();
        if (isStart) GenerateStart();

   
    }

    void GenerateEnvironment()
    {
        foreach (var obj in environmentObjects)
        {
            int amount = (int)Random.Range(0, environmentDensity * width / 50); // T�� nyt vaa testi
            for (int i = 0; i < amount; i++)
            {
                Vector3 randomPosition = new Vector3(Random.Range(-width / EnvSpawnRange, width / EnvSpawnRange), 0, Random.Range(-height / EnvSpawnRange, height / EnvSpawnRange));  
                Debug.Log(width + " " + height);
                Debug.Log(Random.Range(-width / 5, width / 5));

                GameObject newObject = Instantiate(obj, transform.position + randomPosition, Quaternion.Euler(new Vector3(0, Random.Range(0,360), 0)));
                generatedObjects.Add(newObject);
            }
        }
    }

    void GenerateHelpers()
    {
        foreach (var obj in helperObjects)
        {
            int amount = (int)Random.Range(0, helperDensity * width / 50); // T�� nyt vaa testi
            for (int i = 0; i < amount; i++)
            {
                Vector3 randomPosition = new Vector3(Random.Range(-width / EnvSpawnRange, width / EnvSpawnRange), 0, Random.Range(-height / EnvSpawnRange, height / EnvSpawnRange));
               
                GameObject newObject = Instantiate(obj, transform.position + randomPosition, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
                generatedObjects.Add(newObject);
            }
        }
    }

    void GenerateEnemies()
    {
        foreach (var obj in enemyObjects)
        {
            int amount = (int)Random.Range(0, enemyDensity * width / 50); // T�� nyt vaa testi
            for (int i = 0; i < amount; i++)
            {
                Vector3 randomPosition = new Vector3(Random.Range(-width / EnvSpawnRange, width / EnvSpawnRange), 0, Random.Range(-height / EnvSpawnRange, height / EnvSpawnRange));
                
                GameObject enemyObject = Instantiate(obj, transform.position + randomPosition, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
                enemyObject.GetComponent<Enemy>().aggroed = false;
                generatedObjects.Add(enemyObject);
            }
        }
    }


    void GetPlaneSize()
    {
        Mesh planeMesh = GetComponent<MeshFilter>().mesh;
        Bounds bounds = planeMesh.bounds;
        float boundsX = transform.localScale.x * bounds.size.x;
        float boundsY = transform.localScale.y * bounds.size.y;
        float boundsZ = transform.localScale.z * bounds.size.z;
        width = boundsX;
        height = boundsZ;
    }

    void GenerateEnd()
    {
        // Mesh planeMesh = GetComponent<MeshFilter>().mesh;
        // Destroy(planeMesh);
        Instantiate(endObject, transform.position, Quaternion.identity);
    }

    void GenerateStart()
    {
        // Instantiate(startObject, transform.position + new Vector3(20, 0 ,0), Quaternion.identity);
    }
}
