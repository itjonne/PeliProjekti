using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Soldier : MonoBehaviour
{

	public Animator animator;
	private Vector3 velocity;
	private Vector3 prevPos;

	Vector3 playerToMouse;
	Vector3 mouseLocation;
	float directionToMouse;


	GUIStyle myStyle = new GUIStyle(); 

	// Start is called before the first frame update
	void Start()
    {
		myStyle.fontSize = 16;
		myStyle.normal.textColor = Color.cyan;
		animator = GetComponent<Animator>();		
	}

    private void FixedUpdate()
    {
		velocity = (transform.position - prevPos) / Time.deltaTime;
		prevPos = transform.position;
	}



	// Update is called once per frame
	void Update()
	{
		RaycastHit hit; 
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit))
        {
			mouseLocation = hit.point;
			mouseLocation.y = transform.position.y;

		}

		playerToMouse = mouseLocation - transform.position;

		Vector3 forward = transform.TransformDirection(Vector3.forward);
		Vector3 fixedVelocity = velocity;
		fixedVelocity.y = transform.position.y;
	
		directionToMouse = Vector3.Dot(forward.normalized, fixedVelocity.normalized);






		//Liikkuminen
		if (velocity.magnitude > 0.01f && directionToMouse > 0)
		{
			animator.SetBool("Walk", true);
			animator.SetBool("BackWalk", false);
		}


		// if (Input.GetAxisRaw("Vertical") != 0)

		else if (velocity.magnitude > 0.01f && directionToMouse < 0)

		{
			animator.SetBool("BackWalk", true);
			animator.SetBool("Walk", false);
		}

		else

		{
			animator.SetBool("Walk", false);
			animator.SetBool("BackWalk", false);
		}


		//Ampuminen

		if (Input.GetMouseButton(0))
		{
		
			animator.SetBool("Shoot", true);
			animator.SetLayerWeight(1, 1f);
			//animator.SetBool("Walk", false);
		}

		
		else if (Input.GetMouseButtonUp(0))
		{
	
			animator.SetBool("Shoot", false);
			animator.SetLayerWeight(1, 0f);
		}

		
	}

    private void OnGUI()
    {
		if (gameObject.name == "SOLDIER_full")
		{
			GUI.Label(new Rect(300, 10, 100, 20), "Suunta: " + directionToMouse, myStyle);
	
		}
	}


}

