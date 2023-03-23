using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterManager : MonoBehaviour
{
    // Character Data
    public CharacterDataSO data;

    // Events
    public UnityEvent DamageEvent;
    public UnityEvent DeathEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    /*
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            StatModifier damageTaken = new StatModifier(-damageDealer.damage, StatModifierType.FlatAtEnd);
            data.Health.AddModifier(damageTaken);
            // Ampuu tän eventin, tästä vois ottaa kopin joku muu. Ei implementoitu
            DamageEvent.Invoke();
        }

        if (data.Health.Value <= 0)
        {
            // TODO: Sama
            DeathEvent.Invoke();
        }
    } */
}
