using UnityEngine;
using System.Collections;

/************************************************
 *                 Impermanent                  *
 ************************************************
 *      Impermanent objects are things that     *
 * disappear eventually. Things like pick-ups   *
 * or enemies, impermanents die after passing   *
 * a barrier.                                   *
 *      The Pole, is a tangent vector used to   *
 * define the 2D barrier. Currently it assumes  *
 * the barrier plane runs X/Y, using only the   *
 * Z value of the Pole.                         *
 ************************************************/

public class Impermanent : Destructible {
	[System.Serializable]
	public struct Boolean3
	{
		public bool X;
		public bool Y;
		public bool Z;
	}

	public Boolean3 lockAxis;
	public static Vector3 worldMovement;	//the world moves around the player
	//movement
	//TODO: dynamic oriented
	protected float moveScale;
	private const float deathZ = -10f;

	public override void Awake()
	{
		base.Awake();

		moveScale = 1.0f;
		worldMovement = Vector3.zero;
	}
	
	// Update is called once per frame
	public override void Update ()
	{
		base.Update();

		ContemplateExistence();
	}

	public override void Move ()
	{
		//base.Move ();
		MoveRelative();
	}
	//the player stays in place, the world moves relatively
	public void MoveRelative()
	{
		Vector3 targetPosition = transform.position + velocity * Time.deltaTime;
		if(!lockAxis.X)
			targetPosition.x -= worldMovement.x * moveScale * Time.deltaTime;
		if(!lockAxis.Y)
			targetPosition.y -= worldMovement.y * moveScale * Time.deltaTime;
		if(!lockAxis.Z)
			targetPosition.z -= worldMovement.z * moveScale * Time.deltaTime;
		transform.position = targetPosition;
	}

	public virtual void ContemplateExistence()
	{
		if(transform.position.z < deathZ)
		{
			Die ();
		}
	}
}
