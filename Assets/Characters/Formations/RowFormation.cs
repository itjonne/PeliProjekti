using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowFormation : Formation
{
    public override List<Vector3> EvaluatePoints(Transform leader)
    {
        List<Vector3> points = new List<Vector3>();
        for (int i = 1; i <= FormationSize; i++) // Alotetaan ykkösestä ni ei tarvii ehkä säätää
        {
            Vector3 pos = new Vector3(leader.position.x, 0, leader.position.z);
            Vector3 backwardsVector = Quaternion.Euler(leader.rotation.eulerAngles) * Vector3.left;

            pos += GetNoise(pos); // Vähän elävöitetään
            pos += i * backwardsVector.normalized * Spread; // Yritetään laittaa spreadin verran aina taakse
            points.Add(pos);
        }
        return points;
    }
}