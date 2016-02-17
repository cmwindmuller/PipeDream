using UnityEngine;
using System.Collections;

/************************************************
 *                 Texture Panner               *
 ************************************************
 *      Uses the worldMovement to animate UVs.  *
 ************************************************/

public class TexturePanner : MonoBehaviour {

	public Vector2 tileScale;
	public float movementScale;
	Vector2 uvOffset;

	void Awake()
	{
		uvOffset = new Vector2(0,0);
	}

	void LateUpdate() 
	{
		uvOffset.x -= Impermanent.worldMovement.x
								* tileScale.x
								* Time.deltaTime
								* movementScale
								/ transform.localScale.x;
		uvOffset.y -= Impermanent.worldMovement.z
								* tileScale.y
								* Time.deltaTime
								* movementScale
								/ transform.localScale.y;
		GetComponent<Renderer>().material.SetTextureOffset("_MainTex",uvOffset);
	}
}