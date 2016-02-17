using UnityEngine;
using System.Collections;

public class BoxWave : MonoBehaviour {

	public GameObject gObject;
	public Vector2 size;
	public Vector2 offset;

	// Use this for initialization
	void Start () {
		for(int x=0;x<size.x;x++)
		{
			for(int z=0;z<size.y;z++)
			{
				Vector3 spawnPos = gObject.transform.position;
				spawnPos.x = spawnPos.x+(size.x/2-x)*offset.x;
				spawnPos.z = spawnPos.z+z*offset.y;
				Instantiate(gObject,spawnPos,gObject.transform.rotation);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
