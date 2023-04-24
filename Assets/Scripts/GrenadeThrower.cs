using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    [SerializeField] public float force;
    [SerializeField] private Transform Hand;
    public GameObject grenadePrefab;
    public float gravity = 9.81f;

    public float angle = 45f; //Heittokulma
    private Vector3 initialVelocity; //Alkuperäinen nopeus
    private bool thrown = false; //Onko pallo heitetty


    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(1))
        {
            Vector3 target = Input.mousePosition;
            //ThrowGrenade();
        }*/
        if (Input.GetMouseButton(1) && !thrown)
        {
            // Laske heittosuunta hiiren kohdan ja pallon sijainnin perusteella
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 target = hit.point;
                Vector3 direction = target - transform.position;
                direction.y = 0;
                direction = direction.normalized;


                //Laske alkuperäinen nopeus
                float distance = Vector3.Distance(transform.position, target);
                float height = transform.position.y - hit.point.y;
                initialVelocity = CalculateLauchVelocity(distance, height, angle, gravity);
                ThrowGrenade();

            }

        }
    }

    //Laske alkuperäinen nopeus heiton kulman, etäisyyden ja korkeuden perusteella
    private Vector3 CalculateLauchVelocity(float distance, float height, float angle, float gravity)
    {
        float radianAngle = angle * Mathf.Deg2Rad;
        float x = Mathf.Sqrt(distance / (Mathf.Sin(2 * radianAngle) / gravity));
        float y = x * Mathf.Tan(radianAngle);
        float z = x * Mathf.Cos(radianAngle);
        float time = Mathf.Sqrt(2 * (y + height) / gravity);
        Vector3 velocity = new Vector3(z, y, x);
        return velocity / time;
    }
    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, Hand.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        // rb.AddForce(transform.forward * force, ForceMode.VelocityChange);
        rb.velocity = initialVelocity;
        thrown = true;
    }
} 
