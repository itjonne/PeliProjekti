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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            data.health -= damageDealer.damage;
            // Ampuu tän eventin, tästä vois ottaa kopin joku muu. Ei implementoitu
            DamageEvent.Invoke();
        }

        if (data.health <= 0)
        {
            // TODO: Sama
            DeathEvent.Invoke();
        }
    }
}
