using UnityEngine;
using System.Collections;

/************************************************
 *                 Surfer: Player               *
 ************************************************
 *      Player subclass for the game. Deals     *
 * with the more specific details.              *
 *      Uses the InputHandler to set the global *
 * 'ImpermanentObject.worldMovement'. Also      *
 * does camera shake and sounds.                *
 ************************************************/

public class Surfer : Player {

	private const float DEFAULT_SURF_SPEED = 6F;
	private const float DEFAULT_TURN_ACCEL = 0.18F;
	private const float DEFAULT_TURN_MAX = 3.6F;
	//-- surfing --
	public float surfSpeed_def;
	//-- turning --
	public float turnAccel_def;
	public float turnSpeed_max;
	//-- friction --
	public float friction;
	//-- jumping --
	bool isGrounded;
	public float gravity;
	public float jumpHeight;
	private float jumpVelo;

	//-- 'bobbing' --
	public float bobHeight;
	public float bobTime;
	public float bobOffset;
	//-- actual movement, public for reading --
	private Vector3 targetPosition;
	public Vector3 velocity;
	private float acceleration;
	private float turnScale;
	private float relativeX;

	//-- other utility --
	private Vector3 startPosition;
	private InputHandler inputHandler;
	public new Camera camera;
	private Vector3 cameraOffset;
	public GameObject sharkPrefab;
	public GameObject brokenBoardPrefab;

	/*
	 * Calls Player function first.
	 * Set up InputHandler.
	 * Store start position.
	 * Initialize 'boosting'
	 * And a lot of camera stuff.
	 */
	protected override void Awake()
	{
		base.Awake();
		inputHandler = new InputHandler();

		startPosition = transform.position;
		targetPosition = startPosition;

		isGrounded = true;
		jumpVelo = Mathf.Sqrt(Mathf.Abs(2 * gravity * jumpHeight));

		velocity = Vector3.zero;
		velocity.z = surfSpeed_def;

		cameraOffset = camera.transform.position - transform.position;
	}
	
	// Update is called once per frame
	private void Update ()
	{
		if(isAlive)
		{
			UpdateMove();
			CheckForShark();
		}
		else
		{
			UpdateDeath();
		}
		UpdateFacing();
		UpdatePhysics();
		UpdateCamera();
	}

	//MOVEMENT. Most edited function.
	private void UpdateMove()
	{
		turnScale = Mathf.Abs(velocity.z / surfSpeed_def);
		acceleration = inputHandler.getMove() * turnAccel_def * turnScale;

		velocity.x = isGrounded ? velocity.x + acceleration : velocity.x;
		velocity.y = isGrounded ? 0 : velocity.y + gravity * Time.deltaTime;
		velocity.z = surfSpeed_def;

		if(inputHandler.getJump() && isGrounded)
		{
			isGrounded = false;
			velocity.y = jumpVelo;
			AudioHandler.get().Jump();
		}

		if(velocity.x > turnSpeed_max * turnScale)
		{
			velocity.x = turnSpeed_max * turnScale;
		}
		else if(velocity.x < -turnSpeed_max * turnScale)
		{
			velocity.x = -turnSpeed_max * turnScale;
		}

		if(!isGrounded)
		{
			if(targetPosition.y + velocity.y * Time.deltaTime < startPosition.y)
			{
				targetPosition.y = startPosition.y;
				velocity.y = 0;
				isGrounded = true;
			}
		}
		targetPosition.x = startPosition.x;
		targetPosition.y = targetPosition.y + velocity.y * Time.deltaTime;
		targetPosition.z = startPosition.z;

		Vector3 finalPosition = targetPosition;
		if(isGrounded)
		{
			finalPosition.y += Mathf.Sin(Time.time * bobTime + bobOffset) * bobHeight;
		}
		transform.position = finalPosition;
	}
	
	private void UpdateFacing()
	{
		Vector3 scale = transform.localScale;
		if(acceleration < 0 && scale.x > 0)
		{
			scale.x *= -1;
			AudioHandler.get().Turn();
		}
		else if(acceleration > 0 && scale.x < 0)
		{
			scale.x *= -1;
			AudioHandler.get().Turn();
		}
		transform.localScale = scale;
	}

	private void UpdatePhysics()
	{
		transform.rotation = Quaternion.RotateTowards(transform.rotation,
		                                                     Quaternion.Euler(new Vector3(0,0,-acceleration * 2)),
		                                                     5);
		relativeX += velocity.x * Time.deltaTime;
		Impermanent.worldMovement = velocity;
		Impermanent.worldMovement.y = 0;

		if(isGrounded)
			velocity.x *= Mathf.Pow(1 - friction, Time.deltaTime);
	}

	private void UpdateCamera()
	{
		//camera position
		Vector3 destination = transform.position + cameraOffset;
		destination.y = camera.transform.position.y;
		destination.z = camera.transform.position.z;
		camera.transform.position = destination;
		/*float tooFar = 1;
		if(transform.position.x - camera.transform.position.x > tooFar)
		{
			destination.x = transform.position.x - tooFar;
			camera.transform.position = destination;
		}
		else if(transform.position.x - camera.transform.position.x < -tooFar)
		{
			destination.x = transform.position.x + tooFar;
			camera.transform.position = destination;
		}*/

		//camera rotation
		camera.transform.rotation = Quaternion.RotateTowards(camera.transform.rotation,
	                                                      Quaternion.Euler(new Vector3(
																camera.transform.eulerAngles.x,
																camera.transform.eulerAngles.y,
																-acceleration * 0.6f)),0.1f);
	}

	private void CheckForShark()
	{
		if(Mathf.Abs(relativeX) > 24)
		{
			Instantiate(sharkPrefab);
			TakeDamage();
			lives = 0;
			hud.UpdateLives(lives);
			Death();
		}
	}

	protected override void TakeDamage ()
	{
		if(isAlive)
		{
			GameObject brokeBoard = Instantiate(brokenBoardPrefab);
			if(transform.localScale.x < 0)
				brokeBoard.transform.localScale = new Vector3(brokeBoard.transform.localScale.x * -1,
				                                              brokeBoard.transform.localScale.y,
				                                              brokeBoard.transform.localScale.z);
		}
		base.TakeDamage ();
	}
	
	protected override void PickupEffect(System.Type type)
	{
		if(type.Equals(typeof(PickupRing)))
		{
		}
		else
		{
			base.PickupEffect(type);
		}
	}

	protected override void Death ()
	{
		base.Death ();
		acceleration = 0;
		AudioHandler.get().StopCurrentMusic();
		if(RecordKeeper.NewScore(hud.points))
		{
			AudioHandler.get().HiScore();
		}
		else AudioHandler.get().Death();
	}

	private void UpdateDeath()
	{
		transform.position = new Vector3(0,-10,0);
		velocity *= Mathf.Pow(0.5f, Time.deltaTime);
	}
}
