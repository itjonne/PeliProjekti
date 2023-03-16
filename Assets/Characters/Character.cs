using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private CharacterDataSO characterData;
    // Start is called before the first frame update
    public abstract void Move();
    public void LogHealth()
    {
        Debug.Log(characterData.health.Value);
    }
}
