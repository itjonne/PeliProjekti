using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Enemy1 : MonoBehaviour
{

	public ParticleSystem muzzle;
	public Animator animator;
	private Vector3 velocity;
	private Vector3 prevPos;


	//private float yVelocity = 0.0F;
	private float yVelocity = 0.5F;
	private float currWeight;
	// Start is called before the first frame update
	IEnumerator Start()
	{
		muzzle = GetComponentInChildren<ParticleSystem>();
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
		if (velocity.magnitude > 0.001f)
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

		StartCoroutine(WeightDelay());

		//float endWeight = Mathf.SmoothDamp(currWeight, 0.0f, ref yVelocity, 1f);
		//float endWeight = Mathf.Lerp(currWeight, 0.0f, 1f); //Lerp olisi t�h�n varmaan parempi mutta ei toimi jostain syyst�
		//animator.SetLayerWeight(1, endWeight);
	}

	public void OnDeath()
    {
		Debug.Log("OnDeath");
		animator.SetLayerWeight(1, 0);
		animator.SetTrigger("Death");

	}

	public void OnShoot()
	{
	
		animator.SetLayerWeight(1, 1);
		animator.SetTrigger("Shoot");
		muzzle.Play();
		
	}

	public void OnMelee()
    {
		animator.SetLayerWeight(1, 1);
		animator.SetTrigger("Melee");
		StartCoroutine(WeightDelay());

	}

	//T�m� palauttaa layer weightin nollaksi puolen sekunnin j�lkeen, eli kun vihuun osuu, k�velee normaalisti sen j�lkeen
	//TODO smooth systeemi layer weightille, nyt v�h�n t�ks�ht�� tuo liike takaisin p��lle, t�m� aiheuttaa animaattori bugeja jostain syyst�
	IEnumerator WeightDelay() 
    {	

		yield return new WaitForSeconds(0.5f);

		float endWeight = Mathf.SmoothDamp(currWeight, 0.0f, ref yVelocity, 0.5f);
		animator.SetLayerWeight(1, 0);

		StartCoroutine(WeightDelay());
	}
}