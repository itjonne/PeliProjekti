using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class FormationManager : MonoBehaviour

{
    [SerializeField] public Formation lineFormation;
    public SquareFormation squareFormation;
    public RowFormation rowFormation;



    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            SwapFormation();
            /*
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("Vaihdettiin jonoon");
                SwitchToLineFormation();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("Vaihdettiin neliöön");
                SwitchToSquareFormation();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("Vaihdettiin riviin");
                SwitchToRowFormation();
            }
            */
        }
    }

    public void SwapFormation()
    {
        if (lineFormation.enabled == true)
        {
            Debug.Log("SWITCHING TO ROW");
            SwitchToRowFormation();
        }
        else if (rowFormation.enabled == true)
        {
            Debug.Log("SWITCHING TO SQUARE");
            SwitchToSquareFormation();
        }

        else if (squareFormation.enabled == true)
        {
            Debug.Log("SWITCHING TO LINE");
            SwitchToLineFormation();
        }
        else
        {
            Debug.Log("GOING TO START");
            SwitchToLineFormation();
        }

        // pyydetään squadi päivittämään formaatio
        Squad squad = GetComponent<Squad>();
        squad.UpdateFormation();
        
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