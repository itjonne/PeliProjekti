using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Enemy1 : MonoBehaviour
{
	public Animator animator;
	private Vector3 velocity;
	private Vector3 prevPos;


	private float yVelocity = 0.0F;
	private float currWeight;
	// Start is called before the first frame update
	IEnumerator Start()
	{
		animator = GetComponent<Animator>();

		while (true) //Substate machine kuolemille, randomisoi kuolemisanimaatio
        {
			yield return new WaitForSeconds(1);
			animator.SetInteger("DeathIndex", Random.Range(0, 2));
        }
	}

	private void FixedUpdate()
	{
		velocity = (transform.position - prevPos) / Time.deltaTime;
		prevPos = transform.position;
	}



	// Update is called once per frame
	void Update()
	{

		currWeight = animator.GetLayerWeight(1);

		//Liikkuminen
		if (velocity.magnitude > 0.01f)
		{
			animator.SetBool("Walk", true);
		}


		// if (Input.GetAxisRaw("Vertical") != 0)

		else

		{
			animator.SetBool("Walk", false);
		}





	}

	public void OnDamageTaken()
    {
		//Debug.Log("OnDamageTaken");
		animator.SetLayerWeight(1, 1);
		animator.SetTrigger("Hurt");

		//float endWeight = Mathf.SmoothDamp(currWeight, 0.0f, ref yVelocity, 1f);
		//float endWeight = Mathf.Lerp(currWeight, 0.0f, 1f); //Lerp olisi tähän varmaan parempi mutta ei toimi jostain syystä
		//animator.SetLayerWeight(1, endWeight);
	}

	public void OnDeath()
    {
		Debug.Log("OnDeath");
		animator.SetLayerWeight(1, 0);
		animator.SetTrigger("Death");

	}

}