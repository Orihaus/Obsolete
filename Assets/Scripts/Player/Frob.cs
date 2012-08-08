using UnityEngine;
using System.Collections;

public class Frob : MonoBehaviour 
{
	public float FrobCooldown = 0.1f;
	private float Cooldown = 0.0f;
	
	public void Activate()
	{
	    RaycastHit hit;
	    if( Physics.Raycast( transform.position, transform.TransformDirection( Vector3.forward ), out hit, 5.0f ) ) 
		{
			FrobbableObject f = hit.collider.GetComponent<FrobbableObject>();
			if( f != null ) f.Frob( gameObject );
		}
		
		Cooldown = FrobCooldown;
	}
	
	void Update() 
	{
		if( !Input.GetKeyDown( KeyCode.E ) && !Input.GetMouseButtonDown( 0 ) ) Cooldown -= Time.deltaTime;
		
		if( Cooldown < 0.0f )
			if( Input.GetKeyDown( KeyCode.E ) || Input.GetMouseButtonDown( 0 ) ) 
				Activate();
	}
}
