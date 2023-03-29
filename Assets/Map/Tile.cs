using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile
{
    public float width;
    public float height;
    public Vector3 position;
    public string tileLocation = "Floor";
    public GameObject tilePrefab
    {
        get { return Resources.Load(tileLocation) as GameObject; }
    }

    private List<Group> groups;

    public float Width => width;
    public float Height => height;

    public void AddGroup(Group group)
    {
        groups.Add(group);
    }
}
