using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private static Action shootInput;
    public static Action reloadInput;

    [SerializeField] private KeyCode reloadKey;

    public static Action ShootInput { get => shootInput; set => shootInput = value; }

    void Update()
    {
        if (Input.GetMouseButton(0)) 
            shootInput?.Invoke();

        if (Input.GetKeyDown(reloadKey))
            reloadInput?.Invoke();
    }


}