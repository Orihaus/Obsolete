using UnityEngine;
using System.Collections;

public class ScrollY : MonoBehaviour 
{
	public float StartY = -2.0f;
	public float EndY = 2.0f;
	public float Speed = 0.1f;
	
	void FixedUpdate () 
	{
		float InY = transform.localPosition.y;
		float OutY = InY;
		
		if( InY > EndY ) OutY = StartY;
		else OutY += Time.deltaTime * Speed;

		Vector3 OutV = transform.localPosition;
		OutV.y = OutY;
		transform.localPosition = OutV;
	}
}
