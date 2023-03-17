using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacter : Character
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Move()
    {
        Debug.Log("Moving");
        gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 2);
    }

    public override void Follow(Character character)
    {
        Debug.Log(Vector3.Distance(transform.position, character.transform.position));
        if (Vector3.Distance(transform.position, character.transform.position) > 2f )
        {
            transform.position = (Vector3.MoveTowards(transform.position, character.transform.position, 3f * Time.deltaTime));
        }
    }
}
