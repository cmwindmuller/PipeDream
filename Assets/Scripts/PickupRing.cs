using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class PickupRing : Pickup {

	const string STATE_FADE = "FADE";

	void Start ()
	{
	}

	public override void OnPickup ()
	{
		base.OnPickup ();
		GetComponent<Animator>().Play(STATE_FADE);
		moveScale = 0.15f;
		AudioHandler.get().Ring();
	}

}
