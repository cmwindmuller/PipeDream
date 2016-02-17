using UnityEngine;
using System.Collections;

/************************************************
 *                 InputHandler                 *
 ************************************************
 *      An in-between to the Input Class. This  *
 * is to ease multi-platform checks.
 ************************************************/

public class InputHandler {

	public float getMove()
	{
		return Input.GetAxisRaw("Move");
	}
	
	public bool getJump()
	{
		return Input.GetButtonDown("Jump");
	}

	public bool getTrick()
	{
		return Input.GetButtonDown("Trick");
	}
}
