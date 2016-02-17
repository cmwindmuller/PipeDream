using UnityEngine;
using System.Collections;

public class EffectSprite : Destructible {

	public bool fade;
	private bool hasCanvas;
	//private bool hasOutline;

	// Use this for initialization
	public override void Awake () {
		base.Awake();

		if(GetComponent<CanvasRenderer>() != null)
			hasCanvas = true;
		/*if(GetComponent("Outline") != null)
			hasOutline = true;*/
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();

		if(fade)
			FadeOut();
	}

	void FadeOut()
	{
		if(hasCanvas)
		{
			float alpha = (deathTime - Time.time) / lifeSpan;
			GetComponent<CanvasRenderer>().SetAlpha(alpha);
			/*if(hasOutline)
			{

			}*/
		}
	}

}
