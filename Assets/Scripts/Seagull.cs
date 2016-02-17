using UnityEngine;
using System.Collections;

public class Seagull : Enemy {

	public float flySpeed;
	public float proximity;
	public Vector3 spawnLocation;
	private int turnCount;

	private bool squawked;

	public float attackDelay;
	private float attackTime;

	public GameObject poop;

	public override void Awake ()
	{
		base.Awake ();

		velocity.x = flySpeed;

		if(Random.value > 0.5f)
		{
			spawnLocation.x *= -1;
			velocity.x *= -1;
		}
		transform.position = spawnLocation;

		lockAxis.X = true;
		lockAxis.Z = true;
		lockAxis.Y = true;

		attackTime = -1;
		AudioHandler.get().Seagull();
	}

	public override void Update ()
	{
		base.Update ();
		if(state == State.idle)
			ChasePlayer();
		if(state == State.attack)
			Attack();
		UpdateFacing();
	}

	void ChasePlayer()
	{
		if(transform.position.x > proximity && velocity.x > 0)
		{
			velocity.x *= -1;
			turnCount++;
		}
		else if(transform.position.x < -proximity && velocity.x < 0)
		{
			velocity.x *= -1;
			turnCount++;
		}
		if(!squawked && turnCount == 1)
		{
			squawked = true;
		}
		else if(turnCount > 3)
		{
			velocity.x = -transform.position.x;
			state = State.attack;
		}
	}
	
	void Attack()
	{
		if(attackTime < 0)
		{
			if(transform.position.x * (transform.position.x + velocity.x * Time.deltaTime) < 0)
			{
				velocity.x = 0;
				transform.position = new Vector3(0,transform.position.y,transform.position.z);
				lockAxis.X = false;
				attackTime = Time.time + attackDelay;
				moveScale = 0.5f;
			}
		}
		else if(Time.time >= attackTime)
		{
			attackTime = -1;
			velocity.y = flySpeed;
			GameObject poo = Instantiate(poop);
			poo.transform.position += transform.position;
			deathTime = Time.time + 1;
			AudioHandler.get().Poop();
		}
	}

}
