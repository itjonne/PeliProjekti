using JSAM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    [SerializeField] public float throwForce;
    [SerializeField] private Transform Hand;
    private Squad squad;

    private int grenadeAmount = 0;
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
        squad = GetComponentInParent<Squad>();
        if (squad != null)
        {

            grenadeAmount = GetComponentInParent<Squad>().grenadeAmount;
            Debug.Log(grenadeAmount);
        }
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
            gameObject.GetComponent<Animation_Soldier>().OnThrow();
        }
    }

    void ThrowGrenade()
    {
        if (squad.grenadeAmount <= 0) return;
        JSAM.AudioManager.PlaySound(AudioLibSounds.sfx_GrenadePin);
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

        squad.grenadeAmount -= 1;
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
