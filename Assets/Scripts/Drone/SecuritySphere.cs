using UnityEngine;
using System.Collections;

public class SecuritySphere : MonoBehaviour 
{
	public DroneExit PowerNode;
	public GameObject Shield;
	public AudioSource Hum;
	
	public AudioClip Warning_01;
	public AudioClip Warning_02;
	public AudioClip Warning_03;
	
	void Start()
	{
		StartCoroutine( Warn() );
	}
	
    void FixedUpdate()
	{
		if( !PowerNode ) return;
		
		if( !PowerNode.IsPowered ) 
		{
			Shield.GetComponent<MeshRenderer>().enabled = false;
			Shield.GetComponent<SphereCollider>().enabled = false;
			Hum.enabled = false;
		}
		else
		{
			Shield.GetComponent<MeshRenderer>().enabled = true;
			Shield.GetComponent<SphereCollider>().enabled = true;
			Hum.enabled = true;
		}
	}
	
	IEnumerator Warn()
	{
		while( true ) {
			if( PowerNode.IsPowered && PowerNode.PowerStage == 3 ) { audio.clip = Warning_01; audio.Play(); yield return new WaitForSeconds( 1.0f ); }
			if( PowerNode.IsPowered && PowerNode.PowerStage == 2 ) { audio.clip = Warning_02; audio.Play(); yield return new WaitForSeconds( 1.0f ); }
			if( PowerNode.IsPowered && PowerNode.PowerStage == 1 ) { audio.clip = Warning_03; audio.Play(); yield return new WaitForSeconds( 1.0f ); }
			yield return new WaitForSeconds( 1.0f );
		}
	}
}
