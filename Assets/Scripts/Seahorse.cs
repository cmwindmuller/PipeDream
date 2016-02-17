using UnityEngine;
using System.Collections;

public class Seahorse : Enemy {

	public float raiseSpeed;
	public float raiseHeight;
	
	public float attackDelay;
	private float attackTime;

	public Vector3 charge;

	public override void Awake ()
	{
		base.Awake ();
		lockAxis.X = false;
		lockAxis.Y = true;
		lockAxis.Z = true;
		attackTime = -1;
		AudioHandler.get().Emerge();
	}

	public override void Update ()
	{
		base.Update ();
		if(state == State.idle)
			Approach();
		else
			Attack();
	}

	private void Approach()
	{
		if(transform.position.y + raiseSpeed * Time.deltaTime < raiseHeight)
		{
			velocity.y = raiseSpeed;
		}
		else
		{
			velocity.y = 0;
			transform.position = new Vector3(transform.position.x,
			                                 raiseHeight,
			                                 transform.position.z);
			attackTime = Time.time + attackDelay;
			state = State.attack;
		}
	}

	private void Attack()
	{
		if(attackTime > 0 && Time.time > attackTime)
		{
			attackTime = -1;
			velocity.x = worldMovement.x + transform.position.x/(transform.position.z/charge.z);
			velocity.y = charge.y;
			velocity.z = charge.z;
			UpdateFacing();
			AudioHandler.get().Seahorse();
		}
	}

}
