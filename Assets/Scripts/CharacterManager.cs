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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StatModifier damageTaken = new StatModifier(-1, StatModifierType.FlatAtEnd);
            data.health.AddModifier(damageTaken);
            Debug.Log(data.health.Value);
        }
        if (Input.GetMouseButtonDown(1))
        {
            StatModifier giveHealthPercent = new StatModifier(0.1f, StatModifierType.PercentAdd);
            data.health.AddModifier(giveHealthPercent);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            StatModifier damageTaken = new StatModifier(-damageDealer.damage, StatModifierType.FlatAtEnd);
            data.health.AddModifier(damageTaken);
            // Ampuu tän eventin, tästä vois ottaa kopin joku muu. Ei implementoitu
            DamageEvent.Invoke();
        }

        if (data.health.Value <= 0)
        {
            // TODO: Sama
            DeathEvent.Invoke();
        }
    }
}
