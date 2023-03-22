using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Soldier : MonoBehaviour
{

	public Animator animator;

	// Start is called before the first frame update
	void Start()
    {
		animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {

		//TESTI
		//if (Input.GetAxisRaw("Horizontal") != 0)

			Debug.Log(gameObject.GetComponentInParent<Rigidbody>().velocity.magnitude);
		if (gameObject.GetComponentInParent<Rigidbody>().velocity > new Vector3(0,0,0));

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

