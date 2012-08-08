using UnityEngine;
using System.Collections;

public class Phase : MonoBehaviour 
{
	public float JumpDistance = 2.0f;
	public float CooldownTime = 2.5f;
	
	private float Cooldown = 0.0f;
	
	void Update() 
	{
		Cooldown -= Time.deltaTime;
		
		if( Input.GetKey( KeyCode.E ) && Cooldown < 0.0f ) 
		{
			Vector3 fwd = transform.TransformDirection( Vector3.forward );
			transform.position =  transform.position + fwd * JumpDistance;
			
			Cooldown = CooldownTime;
		}
	}
}
