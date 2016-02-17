using UnityEngine;
using System.Collections;

/************************************************
 *                 Surf Manager                 *
 ************************************************
 *      This runs a 'Surf'. A Surf is a single  *
 * run of the playable game. This is where      *
 * gameplay occurs.                             *
 *      It references the Player(Surfer) which  *
 * determines if the game should continue.      *
 ************************************************/

public class SurfReporter : MonoBehaviour {

	public bool isActive;
	public Surfer player;
	
	void Awake() {
		isActive = true;
	}

	void Update () {
		if(isActive && !player.isAlive)
		{
			CheckEndScore();
			isActive = false;
			GameManager.get().isSurfActive = false;
		}
		if(!isActive)
		{
			CheckEndSurf();
		}
	}

	void CheckEndScore()
	{
		//player.hud.points
	}

	void CheckEndSurf()
	{
		//on any input, end the game
		if(Input.anyKeyDown || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			EndGame();
		}
	}

	void EndGame()
	{
		GameManager.get().LoadMenu();
	}
}
