using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFormation : Formation
{
    public override IEnumerable<Vector3> EvaluatePoints(Transform leader)
    {
        for (int i = 1; i <= FormationSize; i++) // Alotetaan ykk�sest� ni ei tarvii ehk� s��t��
        {
            Vector3 pos = new Vector3(leader.position.x, 0, leader.position.z);
            Vector3 backwardsVector = Quaternion.Euler(leader.rotation.eulerAngles) * Vector3.back;

            pos += GetNoise(pos); // V�h�n el�v�itet��n
            pos += i * backwardsVector.normalized * Spread; // Yritet��n laittaa spreadin verran aina taakse
            yield return pos;
        }
    }
}
