using UnityEngine;
using System.Collections;

public class Idle : MonoBehaviour 
{
	public float Speed = 0.1f;
	public float RotateSpeed = 0.1f;
	public Vector3 ForwardDirection = Vector3.forward;
	public Vector3 RotateDirection = new Vector3( 0.1f, 0.0f, 0.0f );
	
	private bool Active = true;
	
	public void Activate() { Active = true; }
	public void Deactivate() { Active = false; }
	
	void FixedUpdate() 
	{
		if( Active )
		{
			transform.Rotate( RotateDirection * RotateSpeed * Time.deltaTime );
			transform.Translate( ForwardDirection * Speed * Time.deltaTime );
		}
	}
}
