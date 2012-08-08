using UnityEngine;
using System.Collections;

public class Predation : MonoBehaviour 
{
	public float PredationCycle = 0.1f;
	public float PredationDistance = 0.5f;
	public float PredateCooldown = 0.5f;
	public int PredateDamage = 5;
	
	private Transform target;
	
	public void Target( Transform tar )
	{
		target = tar;
		
		StartCoroutine( Predate() ); 
	}
	
	public void Untarget()
	{
		target = null;
	}

	IEnumerator Predate()
	{
		while( target != null ) 
		{ 
			float cycle = PredationCycle;
			
			RaycastHit hit;
			if( Physics.Raycast( transform.position, transform.forward, out hit, PredationDistance ) ) 
			{
				Heart hrt;
				hrt = hit.collider.GetComponent<Heart>();
				if( hrt )
				{	
					hrt.TakeLympha( PredateDamage );
					audio.Play();
					cycle = PredateCooldown;
				}
			}
			
			yield return new WaitForSeconds( cycle );
		}
	}
}
