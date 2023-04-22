using JSAM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    private Character target;

    public void Awake()
    {
        
    }

    

    public void MoveTo(Vector3 position)
    {
        transform.position = (Vector3.MoveTowards(transform.position, position, movementSpeed * Time.deltaTime));
    }

    // Start is called before the first frame update
    void Start()
    {
        Character[] characters = GameObject.FindObjectsOfType<Character>();
        target = characters[Random.Range(0, characters.Length)];
    }

    // Update is called once per frame
    public override void Update()
    {
        if (target != null)
        {
            MoveTo(target.transform.position);
            transform.LookAt(target.transform.position);

        } else
        {
            Character[] characters = GameObject.FindObjectsOfType<Character>();
            target = characters[Random.Range(0, characters.Length)];
        }
    }
}
