using UnityEngine;
using System.Collections;

public class Hunt : MonoBehaviour 
{
	public float Speed = 0.01f;
	public Transform Target;
	public float StopDistance = 0.5f;
	public Vector3 ForwardDirection = Vector3.up;

	bool RayTest( Vector3 rayDirection, float distance )
	{
	    if( Physics.Raycast( transform.position, rayDirection, distance ) ) 
			return false;
		
		return true;
	}
	
	void FixedUpdate() 
	{
		 transform.Translate( ForwardDirection * Speed * Time.deltaTime ); //if( RayTest( ForwardDirection, StopDistance ) )
	}
}
