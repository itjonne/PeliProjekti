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
     private float _defaultHealth = 100;
     private float _defaultMaxHealth = 999;
     private float _defaultLevel = 1;
    private float _defaultMaxLevel = 99;

    // ACTUAL VALUES
    [SerializeField] public string Name;
    [SerializeField] public CharacterStat Health;
    [SerializeField] public CharacterStat Level;

    public void Reset()
    {
        Health = new CharacterStat(_defaultHealth, _defaultMaxHealth);
        Level = new CharacterStat(_defaultLevel, _defaultMaxLevel);
        Name = "Default";
    }

}
