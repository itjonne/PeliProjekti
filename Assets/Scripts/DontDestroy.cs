using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static DontDestroy instance;

    private void Awake()
    {
      
        
            instance = this;
            DontDestroyOnLoad(gameObject);
        

    }

    // ...
}
