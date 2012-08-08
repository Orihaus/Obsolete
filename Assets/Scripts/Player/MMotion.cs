using UnityEngine;
using System.Collections;

public class MMotion : MonoBehaviour 
{
	public float Cooldown = 0.0f;
	
	void Update() 
	{
		Cooldown -= Time.deltaTime;
		
		if( Input.GetKeyDown( KeyCode.M ) && Cooldown < 0.0f ) { GetComponent<MotionBlur>().enabled = !GetComponent<MotionBlur>().enabled; Cooldown = 4.0f; }
	}
}
