using UnityEngine;
using System.Collections;

public class PulseBrightness : MonoBehaviour 
{
	public float Offset = 0.1f;
	public float Speed = 1.5f;
	
	void Update( )
	{ 
		Color newcolor = gameObject.renderer.material.GetColor( "_TintColor" );
		newcolor.a = Offset + ( ( Mathf.Sin( Time.time * Speed ) * 0.5f ) + 0.5f ) * ( 0.5f - Offset );
		gameObject.renderer.material.SetColor( "_TintColor", newcolor );
	}
}
