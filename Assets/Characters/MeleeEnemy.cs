using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    private Character target;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OSU ", other);
        if (other.GetComponent<Character>())
        {
            Debug.Log("NYT OSU");
            Destroy(this.gameObject);
        }
    }

    public void MoveTo(Vector3 position)
    {
        transform.position = (Vector3.MoveTowards(transform.position, position, MovementSpeed * Time.deltaTime));
    }

    // Start is called before the first frame update
    void Start()
    {
        Character[] characters = GameObject.FindObjectsOfType<Character>();
        target = characters[Random.Range(0, characters.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        MoveTo(target.transform.position);
    }
}
