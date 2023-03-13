using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * T‰n idea ois pit‰‰ hahmon data sis‰ll‰‰n, voi lis‰t‰ niille muilleki squadin j‰senille
 * Defaultit voi varmaan s‰‰t‰‰ miten haluaa
 */


[CreateAssetMenu(menuName = "Scriptable Objects/Character Data")]
public class CharacterDataSO : ScriptableObject
{
    // DEFAULT VALUES
     private int _defaultHealth = 100;
     private int _defaultLevel = 1;

    // ACTUAL VALUES
    [Range(0, 100)]
    [SerializeField] public int health;
    [Range(1,10)]
    [SerializeField] public int level;

    public void Reset()
    {
        health = _defaultHealth;
        level = _defaultLevel;
    }

}
