using UnityEngine;
using System.Collections;

public class Coral : Enemy {
	public Sprite[] sprite;
	public static int spriteNumber;
	// Use this for initialization
	public override void Awake () {
		base.Awake();

		gameObject.GetComponent<SpriteRenderer>().sprite = sprite[spriteNumber];
		spriteNumber++;
		if(spriteNumber >= sprite.Length)
			spriteNumber = 0;
	}
}
