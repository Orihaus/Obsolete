using UnityEngine;
using System.Collections;

public class LookBob : MonoBehaviour 
{
	public float ViewHeight = 0.7f;
	public float BobSpeed = 4.0f;
	public float BobScale = 0.05f;
	
	private float KeyDownTime = 0.0f;
	
	void Update () 
	{
		if ( Input.GetKey( KeyCode.W ) || Input.GetKey( KeyCode.S ) || 
		    Input.GetKey( KeyCode.A ) || Input.GetKey( KeyCode.D ) ) 
		{
			KeyDownTime += Time.deltaTime;
			Vector3 newloc = new Vector3( 0.0f, ViewHeight + Mathf.Sin( KeyDownTime * BobSpeed ) * BobScale, 0.0f );
			transform.localPosition = newloc;
		} else { KeyDownTime = 0.0f; }
	}
}
