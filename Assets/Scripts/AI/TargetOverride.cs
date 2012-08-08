using UnityEngine;
using System.Collections;

public class TargetOverride : FrobbableObject 
{
	public Transform Target;
	public float ThresholdDistance;
	public MeshRenderer Show;
	public Material ShowMaterial;
	public Material NormalMaterial;
	
	public AudioClip StartBind;
	public AudioClip EndBind;
	public AudioClip NoBind;
	
	public bool IsStartNode;
	public bool IsEndNode;
	
	public bool IsCorrupt = false;
	public AudioClip BecomeCorrupt;
	public ParticleSystem Particles;
	
	public bool TryBind()
	{
		if( IsCorrupt || IsStartNode ) return false;
		if( !ObsoleteGlobals.IsBinding && IsEndNode ) return false;
		
		if( !ObsoleteGlobals.IsBinding )
		{
			audio.clip = StartBind; audio.Play();
			ObsoleteGlobals.IsBinding = true;
			ObsoleteGlobals.Bind = gameObject;
			Show.material = ShowMaterial;
			return true;
		} 
		else
		{
			audio.clip = EndBind; audio.Play();
			ObsoleteGlobals.IsBinding = false;
			ObsoleteGlobals.Bind.GetComponent<TargetOverride>().Target = transform;
			ObsoleteGlobals.Bind.GetComponent<TargetOverride>().Show.material = ObsoleteGlobals.Bind.GetComponent<TargetOverride>().NormalMaterial;
			return true;
		}	
	}
	
	public override void Frob( GameObject Frobber )
	{
		if( !TryBind() ) audio.clip = NoBind; audio.Play();
	}
	
	void Corrupt()
	{
		Show.enabled = false;
		audio.clip = BecomeCorrupt; audio.Play();
		if( Particles ) Particles.enableEmission = false;
	}
	
    void FixedUpdate()
	{
		GameObject[] ds = GameObject.FindGameObjectsWithTag( "Drone" );
		
		foreach( GameObject d in ds ) 
		{
			DetectTarget dt = d.GetComponent<DetectTarget>();
       		if( dt && Vector3.Distance( d.transform.position, transform.position ) < Random.value * ThresholdDistance ) 
			{
				dt.Targetify( Target, false );
				if( d.GetComponent<Corrupt>() && !IsCorrupt ) { IsCorrupt = true; Corrupt(); }
			}
			Random.seed++;
			
		}
		
		if( !IsEndNode ) 
		{
			GetComponent<LineRenderer>().SetPosition( 0, transform.position );
			GetComponent<LineRenderer>().SetPosition( 1, Target.transform.position );
		}
    }
}
