using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{

    public GameObject[] bonusObjects;

    private void Awake()
    {
        GenerateBonus();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateBonus()
    {
        int RandomNum = Random.Range(0, bonusObjects.Length);
        GameObject bonusObject = bonusObjects[RandomNum];

        Instantiate(bonusObject, transform.position, Quaternion.identity);
    }

}
