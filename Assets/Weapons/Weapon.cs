using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public float fireRate;
    [SerializeField] public float damage;
    public float lastShot = 0;

    public abstract void Shoot(Transform rotation);
}
