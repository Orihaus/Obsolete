using UnityEngine;
using System.Collections;

public class AIRotate : MonoBehaviour 
{
	public float Speed = 2.0f;
	public Transform Target;
	public Vector3 Offset;
	public float AvoidFoV = 45.0f;
	public float AvoidDistance = 1.0f;
	public float PreferedHeight = 0.5f;
	public float PlayerVisDistance = 10.0f;
	public bool LockYRotation = true;
	public bool LookAway = false;
	public Vector3 ForwardDirection = Vector3.forward;
	
	private RaycastHit hit;
	
	public void Turn( Vector3 targetDir, bool AllowLockY = false )
	{
		if( LockYRotation && AllowLockY ) targetDir.y = 0.0f;
		
	    Quaternion targetRotation = Quaternion.LookRotation ( targetDir, Vector3.up );
	    Quaternion offsetRotation = targetRotation;
	    offsetRotation.eulerAngles += Offset;
	   	transform.rotation = Quaternion.Slerp( transform.rotation, offsetRotation, Time.deltaTime * Speed );
	}
	
	bool RayTest( Vector3 rayDirection, float distance )
	{
	    if( Physics.Raycast( transform.position, rayDirection, distance ) ) 
			return false;
		
		return true;
	}
	
	void FixedUpdate() 
	{
		//if( RayTest( Vector3.down, PreferedHeight ) ) { Turn( Vector3.down ); return; }
		
		if( !RayTest( ForwardDirection, AvoidDistance ) )
		{
			if( RayTest( Vector3.up, AvoidDistance ) ) { Turn( Vector3.up ); return; }
			if( RayTest( Vector3.down, AvoidDistance ) ) { Turn( Vector3.down ); return; }
			if( RayTest( Vector3.left, AvoidDistance ) ) { Turn( Vector3.left ); return; }
			if( RayTest( Vector3.right, AvoidDistance ) ) { Turn( Vector3.right ); return; }
			
			//print ( " Help meeee" );
			
			//return;
		}
		
		
		if( Target != null ) 
		{
			Vector3 TurnDir = Target.position - transform.position;
			if( LookAway ) TurnDir = -TurnDir;
			Turn( TurnDir, true );
		}
	}
}
