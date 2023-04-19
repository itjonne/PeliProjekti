using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayableCharacter : Character
{

    public float movementSpeed;
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

        gameObject.transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
    }

    public override void Follow(Character character)
    {
        if (Vector3.Distance(transform.position, character.transform.position) > 4f )
        {
            transform.position = (Vector3.MoveTowards(transform.position, character.transform.position, movementSpeed * Time.deltaTime));
        }
    }

    public override void MoveTo(Vector3 position)
    {
        transform.position = (Vector3.MoveTowards(transform.position, position, movementSpeed * Time.deltaTime));
    }

    public override void RotateTo(Character target)
    {
        transform.rotation = target.transform.rotation;

    }

    public override void Attack(Vector3 direction)
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "EndObject")
        {
            Debug.Log("ENDING GAME");
            LevelEnd();
        }

        if (collision.gameObject.tag == "Enemy") {
            health -= 10f;
            if (health <= 0) Die();
         }
    }

    private void Die()
    {
        Debug.Log("DEATH");
        gameObject.GetComponentInParent<Squad>().DestroyCharacter(this);

        gameObject.GetComponent<Animation_Soldier>()?.OnDeath(); // T�� on nyt v�h�n spagetti, t�n ei tarviis ymm�rt�� mit��n squadista/animaatiosta

        Destroy(gameObject);
        // T�h�n kuolemaefektej�
    }

    private void LevelEnd()
    {
        Scene scene = SceneManager.GetActiveScene();
        
        SceneManager.LoadScene(scene.name);
    }
}
