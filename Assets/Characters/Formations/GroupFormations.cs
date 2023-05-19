using System.Collections.Generic;
using UnityEngine;

public class GroupFormation : MonoBehaviour
{
    public List<Formation> formations = new List<Formation>();
    private int currentFormationIndex = 0;

    private void Start()
    {
      
        foreach (Formation formation in formations)
        {
            formation.enabled = false;
        }
        formations[currentFormationIndex].enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log("Vaihdetaan ryhman muotoa");
            formations[currentFormationIndex].enabled = false;
            currentFormationIndex = (currentFormationIndex + 1) % formations.Count;
            formations[currentFormationIndex].enabled = true;
            
        }
    }
}