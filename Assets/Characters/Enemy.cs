using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyDataSO enemyData;
    [SerializeField] public float health = 100f;
    public float Health => enemyData.health;
    public float MovementSpeed => enemyData.movementSpeed;
    
    public void SetHealth(int damage)
    {
        Debug.Log("TAKING DAMAGE " + damage);
        Debug.Log(health);
        health += damage;

        if (health <= 0) Die();
    }

    public void Die()
    {

        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<Collider>());
        //this.enemyData.movementSpeed = 3;
        gameObject.GetComponent<Anim_Enemy1>().OnDeath();
        Destroy(gameObject, 5f);
    }
 

}
