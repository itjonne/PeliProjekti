using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GibForce : MonoBehaviour
{

    Rigidbody gib_Rigidbody;
    public Transform myGib;

    // Start is called before the first frame update
    void Start()
    {
        gib_Rigidbody = GetComponent<Rigidbody>();

        gib_Rigidbody.AddForce(Random.Range(-5, 5), Random.Range(10, 20), Random.Range(-5, 5), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
