using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyDataSO enemyData;
    public float Health => enemyData.Health.Value;
    public float MovementSpeed => enemyData.MovementSpeed.Value;

}
