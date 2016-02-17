using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class Enemy : Impermanent {

	public enum State{idle,attack};

	public bool beenHit;

	public const string TAG = "Enemy";
	
	protected State state;

	public override void Awake()
	{
		base.Awake();
		tag = TAG;
		state = State.idle;
		GetComponent<BoxCollider>().isTrigger = true;
	}

	public override void Update()
	{
		base.Update();
	}
}
