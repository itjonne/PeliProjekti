using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Soldier : MonoBehaviour
{
	public ParticleSystem muzzle;
	public Animator animator;
	private Vector3 velocity;
	private Vector3 prevPos;

	Vector3 playerToMouse;
	Vector3 mouseLocation;
	float directionToMouse;
	float directionToMouse_x;


	GUIStyle myStyle = new GUIStyle(); 

	// Start is called before the first frame update
	void Start()
    {
		myStyle.fontSize = 16;
		myStyle.normal.textColor = Color.cyan;
		animator = GetComponent<Animator>();
		muzzle = GetComponentInChildren<ParticleSystem>();
		muzzle = GameObject.Find("MuzzleParticles").GetComponent<ParticleSystem>();
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
		Vector3 sideway = transform.TransformDirection(Vector3.left);
		Vector3 fixedVelocity = velocity;
		fixedVelocity.y = transform.position.y;
	
		directionToMouse = Vector3.Dot(forward.normalized, fixedVelocity.normalized);
		directionToMouse_x = Vector3.Dot(sideway.normalized, fixedVelocity.normalized);

		//sekoilu



		//Liikkuminen
		if (velocity.magnitude > 0.01f && directionToMouse > 0.5f)
		{
			animator.SetBool("Walk", true);
			animator.SetBool("BackWalk", false);
			animator.SetBool("Strafe_Left", false);
			animator.SetBool("Strafe_Right", false);
		}

		else if (velocity.magnitude > 0.01f && directionToMouse < -0.5f)

		{
			animator.SetBool("Walk", false);
			animator.SetBool("BackWalk", true);
			animator.SetBool("Strafe_Left", false);
			animator.SetBool("Strafe_Right", false);
		}

		else if (velocity.magnitude > 0.01f && directionToMouse_x > 0.8f && directionToMouse != 0)

		{
			animator.SetBool("Walk", false);
			animator.SetBool("BackWalk", false);
			animator.SetBool("Strafe_Left", true);
			animator.SetBool("Strafe_Right", false);
		}

		else if (velocity.magnitude > 0.01f && directionToMouse_x < -0.8f && directionToMouse != 0)

		{
			animator.SetBool("Walk", false);
			animator.SetBool("BackWalk", false);
			animator.SetBool("Strafe_Left", false);
			animator.SetBool("Strafe_Right", true);
		}


		else

		{
			animator.SetBool("Walk", false);
			animator.SetBool("BackWalk", false);
			animator.SetBool("Strafe_Left", false);
			animator.SetBool("Strafe_Right", false);
		}


		//Ampuminen
		//NÄMÄ TOIMIVAT SILLÄ EHDOLLA ETTÄ ASEESSA ON LUOTEJA
		if (Input.GetMouseButton(0))
		{
		
			animator.SetBool("Shoot", true);
			muzzle.Play(); // TÄMÄ PITÄÄ SAADA LAUKEAMAAN SILLÄ HETKELLÄ KUN LUOTI LUODAAN
			animator.SetLayerWeight(1, 1f);
			//animator.SetBool("Walk", false);
		}

		
		else if (Input.GetMouseButtonUp(0))
		{
	
			animator.SetBool("Shoot", false);
			muzzle.Stop();
			//TÄHÄN PITÄÄ SAADA 0.1 SEK DELAY
			animator.SetLayerWeight(1, 0f);
			
		}

		
	}

	/*
    private void OnGUI()
    {
		if (gameObject.name == "SOLDIER_full")
		{
			GUI.Label(new Rect(300, 10, 100, 20), "Suunta: " + directionToMouse, myStyle);
			GUI.Label(new Rect(300, 30, 100, 20), "Suunta_X: " + directionToMouse_x, myStyle);
		}
	}
	*/

}

