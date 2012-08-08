using UnityEngine;
using System.Collections;

public class LocalTeleport : MonoBehaviour 
{
	public bool Active = true;
	public float RandomizeCooldownMultiplier = 5.0f;
	public float TeleportRange = 5.0f;
	private float TeleportCooldown;
	
	void Start()
	{
		RandomizeCooldown();
	}
	
	void Teleport()
	{
		gameObject.transform.position += Random.insideUnitSphere * TeleportRange;
		Random.seed++;
	}
	
	void RandomizeCooldown()
	{
		TeleportCooldown = Random.value * RandomizeCooldownMultiplier;
	}
	
	bool isOutside()
	{
		RaycastHit hit;
		Vector3 dwn = transform.TransformDirection( Vector3.down );
		Vector3 up = transform.TransformDirection( Vector3.up );
		
		if( Physics.Raycast( transform.position + up * 100.0f, dwn, out hit ) ) { Debug.DrawLine(transform.position, hit.point);
			return hit.collider.transform == transform; }
		
		return true;
	}
	
	void FixedUpdate() 
	{
		if( !Active ) return;
		
		TeleportCooldown -= Time.deltaTime;
		
		if( TeleportCooldown < 0.0f )
		{
			Teleport();
			while( !isOutside() ) {
						print(!isOutside());
				Teleport();
			}
			
			RandomizeCooldown();
		}
	}
}
