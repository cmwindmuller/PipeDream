using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class Pickup : Impermanent {

	public int points;
	public bool skipEffect;
	private Collider boxCollider;

	public const string TAG = "Pickup";

	public override void Awake()
	{
		base.Awake();
		tag = TAG;
		boxCollider = GetComponent<BoxCollider>();
		boxCollider.isTrigger = true;
	}

	public virtual void OnPickup()
	{
	}
}