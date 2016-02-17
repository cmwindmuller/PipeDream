using UnityEngine;
using System.Collections;

/************************************************
 *                    Forge                     *
 ************************************************
 *      Forge is a class for spawning things.   *
 * It keeps a list of Constructs, which act as  *
 * a blueprint and a schedule.                  *
 *      A single Forge object is all a level    *
 * should need to spawn the game objects the    *
 * player encounters. Probably Impermanents.    *
 ************************************************/

[System.Serializable]
public struct Construct
{
	public float period;		//how long between spawns
	public GameObject target;	//what to spawn
	public Vector3 location;	//the central point to spawn at
	public Vector3 dimensions;	//bounding box around the central point
	public bool extents;
	public float spawnTime;		//used to store when to spawn next
	public bool useWorldSpace;  //spawn relative to the player?
}

public class Forge : Impermanent {

	public Construct[] construct;
	private int count;

	public float xFactor;

	public override void Awake () {
		base.Awake();

		lockAxis.Y = true;
		lockAxis.Z = true;

		count = construct.Length;
		for(int i = 0; i < count; i++)
		{
			construct[i].spawnTime = construct[i].period + Time.time;
		}
	}
	
	// Update is called once per frame
	public override void Update ()
	{
		base.Update();
		if(GameManager.get().isSurfActive)
			UpdateSpawn();
	}

	void UpdateSpawn()
	{
		for(int i = 0; i < count; i++)
		{
			if(Time.time > construct[i].spawnTime)
			{
				Instantiate( construct[i].target,
				            FindNextSpawn(construct[i].location, construct[i].dimensions, construct[i].extents, construct[i].useWorldSpace),
				            Quaternion.Euler(Vector3.zero));
				construct[i].spawnTime = Time.time+construct[i].period;
			}
		}
	}

	Vector3 FindNextSpawn(Vector3 position, Vector3 confines, bool extents, bool useWorldSpace)
	{
		Vector3 newSpawn = new Vector3();
		newSpawn.x = position.x + Random.value * confines.x - confines.x / 2;
		if(!useWorldSpace)
			newSpawn.x += transform.position.x;
		newSpawn.y = position.y + Random.value * confines.y - confines.y / 2;
		newSpawn.z = position.z + Random.value * confines.z - confines.z / 2;

		if(extents)
		{
			float x = 1;
			if(Random.value > 0.5f)
				x = -1;
			newSpawn.x = position.x + confines.x * x;
		}

		return newSpawn;
	}
}