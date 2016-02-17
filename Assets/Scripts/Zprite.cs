using UnityEngine;
using System.Collections;

public class Zprite : MonoBehaviour {

	public Vector3 velocity;
	public Vector3 acceleration;
	public Vector3 torque;

	private bool _isLeftFacing;

	public virtual void Awake()
	{
	}

	public virtual void Update()
	{
		UpdateVelocity();
		UpdateTorque();
		Move();
	}

	public virtual void UpdateVelocity()
	{
		velocity += acceleration * Time.deltaTime;
	}

	public virtual void UpdateTorque()
	{
		transform.Rotate(torque.x * Time.deltaTime,torque.y * Time.deltaTime,torque.z * Time.deltaTime);
	}

	public virtual void Move()
	{
		transform.position += velocity * Time.deltaTime;
	}

	public virtual void UpdateFacing()
	{
		if(!_isLeftFacing)
		{
			if(velocity.x > 0 && transform.localScale.x < 0 || velocity.x < 0 && transform.localScale.x > 0)
			{
				transform.localScale = new Vector3(transform.localScale.x * -1,
				                                   transform.localScale.y,
				                                   transform.localScale.z);
			}
		}
		else
		{
			if(velocity.x > 0 && transform.localScale.x > 0 || velocity.x < 0 && transform.localScale.x < 0)
			{
				transform.localScale = new Vector3(transform.localScale.x * -1,
				                                   transform.localScale.y,
				                                   transform.localScale.z);
			}
		}
	}

	public bool isLeftFacing
	{

		get
		{
			return _isLeftFacing;
		}

		set
		{
			_isLeftFacing = value;
		}
	}

}
