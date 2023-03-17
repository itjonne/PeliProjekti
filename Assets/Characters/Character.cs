using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private CharacterDataSO characterData;
    public bool isLeader;
    // Start is called before the first frame update

    public abstract void Move();
    public abstract void Follow(Character character);
    public abstract void MoveTo(Vector3 point);
    public void LogHealth()
    {
        Debug.Log(characterData.health.Value);
    }
}
