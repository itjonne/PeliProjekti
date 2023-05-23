using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndArrow : MonoBehaviour
{
    private LevelEnd exit;
    // Start is called before the first frame update
    void Start()
    {
        exit = FindObjectOfType<LevelEnd>();
        //var target = GameObject.FindGameObjectsWithTag("EndObject");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(exit.transform.position);
        Destroy(gameObject, 5f);
    }
}
