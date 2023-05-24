using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUI : MonoBehaviour
{
    private Weapons weapons;
  

    // Start is called before the first frame update
    void Start()
    {
        // reload = GetComponentInParent<Weapons>().reloading;
       // weapons = FindObjectOfType(typeof(Weapons)) as Weapons;
        weapons = transform.parent.GetComponent<Weapons>();
    }

    // Update is called once per frame
    void Update()
    {
        var reload = GetComponentInParent<Weapons>().reloading;
        if (reload)
        {
            gameObject.SetActive(true);
        }

        else
        {
            gameObject.SetActive(false);
        }
    }
}
