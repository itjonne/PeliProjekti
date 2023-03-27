using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public float fireRate;
    [SerializeField] public float damage;
    public float lastShot = 0;
    [SerializeField][Range(0, 1)] public float _noise = 0;

    public abstract void Shoot(Transform rotation);
    public Vector3 GetNoise(Vector3 pos)
    {
        Debug.Log(pos);
        var noise = Mathf.PerlinNoise(pos.x * _noise, pos.z * _noise);

        return new Vector3(noise, 0, noise);
    }
}
