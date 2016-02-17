using UnityEngine;
using System.Collections;

/************************************************
 *                    Player                    *
 ************************************************
 *      Player class. Tracks their lives.       *
 ************************************************/

public abstract class Player : MonoBehaviour {

	public const string TAG = "Player";

	public SurfHUD hud;

	public bool isAlive;
	protected int lives;
	private const int LIVES_START = 3;
	//private const int HEALTH_MAX = 5;
	//private const int HEALTH_START = 3;

	private float travelDistance;
	private float runTime;

	// Use this for initialization
	protected virtual void Awake ()
	{
		isAlive = true;
		hud.UpdateScore();
		lives = LIVES_START;
		hud.UpdateLives(lives);
	}
	
	protected virtual void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.CompareTag(Pickup.TAG))//pickups
		{
			Pickup p = col.gameObject.GetComponent<Pickup>();
			if(p != null)
			{
				hud.RenderPoints(p.points);
				hud.GainPoints(p.points);
				p.OnPickup();
				hud.UpdateScore();

				if(!p.skipEffect)
				{
					PickupEffect(p.GetType());
				}
			}
			else
			{
				Destroy(p.gameObject);
			}
		}
		else if(col.gameObject.CompareTag(Enemy.TAG))
		{
			Enemy e = col.gameObject.GetComponent<Enemy>();
			if(e != null && !e.beenHit)
			{
				e.beenHit = true;
				TakeDamage();
			}
		}
	}

	protected virtual void TakeDamage()
	{
		lives --;
		if(lives < 1)
			Death();
		hud.UpdateLives(lives);
		AudioHandler.get().Hit();
	}

	protected virtual void PickupEffect(System.Type type)
	{
		Debug.Log(type+" has no effect.");
	}

	protected virtual void Death()
	{
		if(isAlive)
		{
			isAlive = false;
		}
	}
}
