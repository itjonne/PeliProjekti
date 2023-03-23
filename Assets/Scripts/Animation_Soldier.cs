using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Soldier : MonoBehaviour
{

	public Animator animator;
	private Vector3 velocity;
	private Vector3 prevPos;

	// Start is called before the first frame update
	void Start()
    {
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

		//TESTI
		//if (Input.GetAxisRaw("Horizontal") != 0)

		Debug.Log(velocity);
		if ( velocity.magnitude > 0.01f)
		{
			animator.SetBool("Walk", true);
		}


		//else if (Input.GetAxisRaw("Vertical") != 0)
		/*
	
		*/
		else

		{
			animator.SetBool("Walk", false);
		}

		//Ampuminen

		if (Input.GetMouseButton(0) )
		{
			
			animator.SetBool("Shoot", true);
			animator.SetBool("Walk", false);
		}

		else

        {
			animator.SetBool("Shoot", false);
		}

	}

}
