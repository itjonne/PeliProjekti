using System.Collections.Generic;
using UnityEngine;

public class SquareFormation : Formation 
{
    private void Update()
    {

    }
    public override List<Vector3> EvaluatePoints(Transform leader)
    {
        List<Vector3> points = new List<Vector3>();
        int rowSize = Mathf.CeilToInt(Mathf.Sqrt(FormationSize));
        int halfRowSize = Mathf.FloorToInt((float)rowSize / 2f);
        int halfFormationSize = Mathf.FloorToInt((float)FormationSize / 2f);
        Vector3 rightVector = Quaternion.Euler(leader.rotation.eulerAngles) * Vector3.right;
        Vector3 forwardVector = Quaternion.Euler(leader.rotation.eulerAngles) * Vector3.forward;

        for (int i = 0; i < FormationSize; i++)
        {
            int row = i / rowSize;
            int col = i % rowSize;

            // Paikat suhteessa leaderiin
            Vector3 pos = new Vector3((col - halfRowSize) * Spread, 0, (row - halfRowSize) * Spread);
            pos = Quaternion.LookRotation(forwardVector, Vector3.up) * pos;
            pos += leader.position;

            pos += GetNoise(pos);
            points.Add(pos);
        }

        return points;
    }
}