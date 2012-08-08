using UnityEngine;
using System.Collections;

public class DroneSpawn : MonoBehaviour 
{
	public float SpawnRadius = 1.0f;
	public int SpawnCount = 5;
	public float SpawnCycleRate = 5.0f;
	public int SpawnMax = 50;
	
	public GameObject DronePrefab;
	public Transform DroneTarget;
	
	private float DroneCycle;
	public int Spawned = 0;
	
	void Start() 
	{
		DroneCycle = SpawnCycleRate;
	}
		
	void SpawnDrones()
	{
		if( GetComponent<TargetOverride>().IsCorrupt ) return;
		
		for( int i = 0; i < SpawnCount; i++ )
		{
			GameObject prefab = DronePrefab;
			GameObject o = (GameObject)Instantiate( prefab, transform.position + Random.onUnitSphere * SpawnRadius, transform.rotation );
			o.GetComponent<DetectTarget>().SecondaryTarget = DroneTarget;
			o.GetComponent<Drone>().Spawner = this;
			Random.seed++;
			Spawned++;
		}
	}
	
	void Update() 
	{
		DroneCycle -= Time.deltaTime;
		if( DroneCycle < 0.0 && Spawned < SpawnMax ) 
		{
			SpawnDrones();
			DroneCycle = SpawnCycleRate;
		}
		
		//DronesInWorld = Resources.FindObjectsOfTypeAll( typeof( Drone ) ).Length;
	}
}
