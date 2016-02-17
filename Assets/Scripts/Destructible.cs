using UnityEngine;
using System.Collections;

public class Destructible : Zprite {

	public float lifeSpan;
	protected float deathTime;

	public override void Awake ()
	{
		base.Awake ();
		if(lifeSpan > 0)
			deathTime = Time.time + lifeSpan;
		else deathTime = -1;
	}

	public override void Update ()
	{
		base.Update ();
		if(deathTime > 0 && Time.time > deathTime)
			Die ();
	}
	public virtual void Die()
	{
		Destroy(this.gameObject);
	}
}
