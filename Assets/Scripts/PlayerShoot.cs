using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static Action shootInput;
    public static Action reloadInput;

    [SerializeField] private KeyCode reloadKey;


    
    void Update()
    {
        if (Input.GetMouseButton(0)) 
            shootInput?.Invoke();

        if (Input.GetKeyDown(reloadKey))
            reloadInput?.Invoke();
    }


}