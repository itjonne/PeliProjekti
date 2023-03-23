using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private CharacterDataSO characterData;
    public bool isLeader;
    // Start is called before the first frame update

    // TODO: Onko typerää / miten typerää?
    public string Name => characterData.Name;
    public float Health => characterData.Health.Value;
    public float Level => characterData.Level.Value;

    public abstract void Move();
    public abstract void Follow(Character character);
    public abstract void MoveTo(Vector3 point);
    public abstract void Attack(Vector3 direction);
    public void LogHealth()
    {
        Debug.Log(characterData.Health.Value);
    }
}
