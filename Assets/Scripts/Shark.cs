using UnityEngine;
using System.Collections;

public class Shark : Destructible {

	/*public float timeScale;
	public float amount;

	private float spawnTime;
	private float strikeTime;*/

	public override void Awake ()
	{
		base.Awake ();

		AudioHandler.get().Shark();
		/*isLeftFacing = true;
		spawnTime = Time.time;*/
	}

	public override void Update ()
	{
		base.Update ();
		/*if(state == State.idle)
		{
			velocity.x = Mathf.Sin(Time.time * timeScale + spawnTime) * amount;
			if(transform.position.z < 12)
			{
				state = State.attack;
			}
			else if(transform.position.z < 20)
			{
				velocity.y = -1;
			}
		}
		else if(state == State.attack)
		{
			if(transform.position.y < 0)
			{
				velocity.y = 5;
			}
			else
			{
				velocity.y = 0;
			}
		}

		UpdateFacing();*/
		/*velocity.x = Mathf.Sin(Time.time * timeScale + spawnTime) * amount;
		velocity.z = Mathf.Cos(Time.time * timeScale + spawnTime) * amount;
		transform.rotation = Quaternion.Euler(0,Mathf.Atan2(velocity.x,velocity.z) * Mathf.Rad2Deg + 90,0);*/
	}

}
