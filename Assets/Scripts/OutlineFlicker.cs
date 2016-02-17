using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Outline))]

public class OutlineFlicker : MonoBehaviour {

	public int min = 1;
	public int max = 3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Random.seed = Mathf.FloorToInt(Time.time*8);
		GetComponent<Outline>().effectDistance = new Vector2(Random.Range(min,max),Random.Range(min,max));
	}
}
