using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private CharacterDataSO characterData;
    public float health = 30f;
    public int level = 1;
    public float exp = 0;
    public bool isLeader;
    // Start is called before the first frame update

    // TODO: Onko typer‰‰ / miten typer‰‰?
    public string Name => characterData.Name;
    public float Health => characterData.Health.Value;

    // Nyt aina 20exp v‰lein nousee levelit
    public void GainExp(int amount)
    {
        exp += amount;
        if (exp > 20) LevelUp();
    }

    public void LevelUp()
    {
        level += 1;
        exp = 0;
        Debug.Log("GAINING LEVEL");
        Debug.Log(level);
    }

    public abstract void Move();
    public abstract void Follow(Character character);
    public abstract void MoveTo(Vector3 point);

    public abstract void ChangeLeader(bool isLeader);

    // T‰‰ vois olla hiiri
    public abstract void RotateTowards(Vector3 point);

    public abstract void RotateTo(Character target);
    public abstract void Attack(Vector3 direction);
    public void LogHealth()
    {
        Debug.Log(characterData.Health.Value);
    }
}
