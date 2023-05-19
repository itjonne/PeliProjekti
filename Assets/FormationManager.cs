using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class FormationManager : MonoBehaviour

{
    public LineFormation lineFormation;
    public SquareFormation squareFormation;
    public RowFormation rowFormation;



    private void Update()
    {
        
        if (Input.GetKey(KeyCode.LeftControl))
        {
            
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("Vaihdettiin jonoon");
                SwitchToLineFormation();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("Vaihdettiin neliöön");
                SwitchToSquareFormation();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("Vaihdettiin riviin");
                SwitchToRowFormation();
            }
        }
    }
    private void SwitchToLineFormation()
    {
        
        lineFormation.enabled = true;
        squareFormation.enabled = false;
        rowFormation.enabled = false;
    }

    private void SwitchToSquareFormation()
    {
        
        lineFormation.enabled = false;
        squareFormation.enabled = true;
        rowFormation.enabled = false;
    }

    private void SwitchToRowFormation()
    {
        
        lineFormation.enabled = false;
        squareFormation.enabled = false;
        rowFormation.enabled = true;
    }
}