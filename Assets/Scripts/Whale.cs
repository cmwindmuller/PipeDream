using UnityEngine;
using System.Collections;

public class Whale : Enemy {

	public float minSpeed;
	public float maxSpeed;

	public override void Awake ()
	{
		base.Awake ();
		CalculateSpeed();
		UpdateFacing();
	}

	private void CalculateSpeed()
	{
		//GameObject player = GameObject.FindGameObjectWithTag(Player.TAG);
		//Surfer surfer = player.GetComponent<Surfer>();
		//velocity.x = -1 * transform.position.x / 8;
		float dif = maxSpeed - minSpeed;
		velocity.x = minSpeed + Random.value * dif;
		if(transform.position.x > 0)
			velocity.x *= -1;
	}

}
