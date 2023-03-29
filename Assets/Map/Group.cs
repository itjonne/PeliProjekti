using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    private Enemy[] enemies;
    private Vector3 position;

    public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }
    // Start is called before the first frame update
    public Enemy[] Items
    {
        get { return enemies; }
    }
}
