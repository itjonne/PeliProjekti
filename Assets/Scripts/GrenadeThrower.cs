using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    [SerializeField] public float throwForce;
    [SerializeField] private Transform Hand;
    public GameObject grenadePrefab;
    public Vector3 gravity;



    Vector3 playerToMouse;
    Vector3 mouseLocation;

    float throwDistance;

    GUIStyle myStyle = new GUIStyle();

    private void Start()
    {

        myStyle.fontSize = 16;
		myStyle.normal.textColor = Color.cyan;


        gravity = Physics.gravity;
    }

    // Update is called once per frame
    void Update()
    {

     
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            mouseLocation = hit.point;

        }

            throwDistance = Vector3.Distance(mouseLocation, transform.position);      

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 target = Input.mousePosition;
            ThrowGrenade();
        }
    }

    void ThrowGrenade()
    {
        //KRANU LENTÄÄ HIIREN KOHDALLE MUTTA EI VOI LENTÄÄ 20 UNITTIA KAUEMMAKSI
        if (throwDistance < 20)
        { 
            GameObject grenade = Instantiate(grenadePrefab, Hand.position, transform.rotation);
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * throwDistance * 1.15f, ForceMode.Impulse);
            rb.AddForce(transform.up * throwDistance * 0.7f, ForceMode.Impulse);
        }

        else
        {
            GameObject grenade = Instantiate(grenadePrefab, Hand.position, transform.rotation);
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 20 * 1.15f, ForceMode.Impulse);
            rb.AddForce(transform.up * 20 * 0.7f, ForceMode.Impulse);
        }


    }


    // HIIREN ETÄISYYDEN Debuggaukseen
    /*
    private void OnGUI()
    {
        if (gameObject.name == "SOLDIER_full")
        {
            GUI.Label(new Rect(300, 10, 100, 20), "distance: " + throwDistance, myStyle);
          
        }
    }
    */
}
