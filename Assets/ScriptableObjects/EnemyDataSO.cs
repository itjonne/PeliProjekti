using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * T‰n idea ois pit‰‰ hahmon data sis‰ll‰‰n, voi lis‰t‰ niille muilleki squadin j‰senille
 * Defaultit voi varmaan s‰‰t‰‰ miten haluaa
 */


[CreateAssetMenu(menuName = "Scriptable Objects/Enemy Data")]
public class EnemyDataSO : ScriptableObject
{
    // DEFAULT VALUES
    private float _defaultHealth = 100;
    private float _defaultMaxHealth = 999;
    private float _defaultMovementSpeed = 3f;
    private float _defaultMaxMovementSpeed = 999;

    // ACTUAL VALUES
    [SerializeField] public string Name;
    [SerializeField] public float health;
    [SerializeField] public float movementSpeed;
    // [SerializeField] public CharacterStat Health;
    //[SerializeField] public CharacterStat MovementSpeed;

    public void Reset()
    {
        health = _defaultHealth;
        movementSpeed = _defaultMovementSpeed;
        //Health = new CharacterStat(_defaultHealth, _defaultMaxHealth);
        //MovementSpeed = new CharacterStat(_defaultMovementSpeed, _defaultMaxMovementSpeed);
        Name = "DefaultEnemy";
    }

}
