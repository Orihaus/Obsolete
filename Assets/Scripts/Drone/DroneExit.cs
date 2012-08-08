using UnityEngine;
using System.Collections;

public class DroneExit : MonoBehaviour 
{
	public float ThresholdDistance;
	public float DronePowerCycle = 5.0f;
	public bool IsPowered = false;
	
	public AudioClip OnSound;
	public AudioClip OffSound;
	
	public int PowerStage = 4;
	
	public float InitalPowerCooldown = 25.0f;
	private float PowerCooldown = 25.0f;

	
	void Start()
	{
		PowerCooldown = InitalPowerCooldown;
	}
	
    void FixedUpdate()
	{
		if( GetComponent<TargetOverride>().IsCorrupt ) 
		{ 
			if( IsPowered ) { IsPowered = false; audio.clip = OffSound; audio.Play(); }
			return; 
		}
		
		PowerCooldown -= Time.deltaTime;
		
		if( PowerCooldown < ( DronePowerCycle * 0.75f ) && PowerStage == 4 ) PowerStage = 3;
		if( PowerCooldown < ( DronePowerCycle * 0.5f ) && PowerStage == 3 ) PowerStage = 2;
		if( PowerCooldown < ( DronePowerCycle * 0.25f ) && PowerStage == 2 ) PowerStage = 1;
		
		if( PowerCooldown < 0.0f && IsPowered == true ) { IsPowered = false; audio.clip = OffSound; audio.Play(); PowerStage = 0; }
		else if( PowerCooldown > 0.0f && IsPowered == false ) { IsPowered = true; audio.clip = OnSound; audio.Play(); }		
		
		GameObject[] ds = GameObject.FindGameObjectsWithTag( "Drone" );
		foreach( GameObject d in ds ) 
		{
			Drone dt = d.GetComponent<Drone>();
       		if( dt && Vector3.Distance( d.transform.position, transform.position ) < Random.value * ThresholdDistance ) 
			{
				dt.Spawner.Spawned--;
				Destroy( dt.gameObject );
				PowerCooldown = DronePowerCycle;
				PowerStage = 4;
			}
			Random.seed++;
		}
    }
}
