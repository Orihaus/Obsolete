using UnityEngine;
using System.Collections;

public class Bob : MonoBehaviour 
{
	public float Speed = 2.0f;
	public float Amount = 1.0f;
	public float Offset = 0.0f;
	
	void Update() 
	{
		transform.Translate( Vector3.forward * Mathf.Sin( Offset + Time.time * Speed ) * Amount );
	}
}
