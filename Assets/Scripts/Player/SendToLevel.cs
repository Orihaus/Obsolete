using UnityEngine;
using System.Collections;

public class SendToLevel : MonoBehaviour 
{
	public string LevelName = "";
	
	public DroneExit PowerNode;
	public Material PowerMat;
	
    void OnTriggerEnter( Collider other ) 
	{
		if( !PowerNode )
			if( other.tag == "Player" ) Application.LoadLevel( LevelName );
		if( PowerNode && PowerNode.IsPowered )
			if( other.tag == "Player" ) Application.LoadLevel( LevelName );
	}
	
    void FixedUpdate()
	{
		if( !PowerNode ) return;
		
		if( !PowerNode.IsPowered ) 
		{
			Color c = PowerMat.GetColor( "_TintColor" );
			c.a = 0.03f;
			PowerMat.SetColor( "_TintColor", c );
		}
		else
		{
			Color c = PowerMat.GetColor( "_TintColor" );
			c.a = 0.25f;
			PowerMat.SetColor( "_TintColor", c );
		}
	}
}
