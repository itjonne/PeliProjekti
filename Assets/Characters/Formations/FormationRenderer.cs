using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationRenderer : MonoBehaviour
{
    private Formation _formation;
    public Formation Formation
    {
        get
        {
            if (_formation == null) _formation = GetComponent<Formation>();
            return _formation;
        }
        set => _formation = value;
    }

    [SerializeField] private Vector3 _unitGizmoSize;
    [SerializeField] private Color _gizmoColor;

    // Instead of just drawing gizmos, you could create an 'Army' script which would actually spawn the units in the positions
    // returned by Formation.EvaluatePoints
    private void OnDrawGizmos()
    {
        if (Formation == null || Application.isPlaying) return;
        Gizmos.color = _gizmoColor;

        Debug.Log(_unitGizmoSize);

        foreach (var pos in Formation.EvaluatePoints(transform))
        {
            Debug.Log(transform.position + pos + new Vector3(0, 1, 0));
            
            Gizmos.DrawSphere(transform.position + pos + new Vector3(0, 1, 0), 10);
        }
    }
}