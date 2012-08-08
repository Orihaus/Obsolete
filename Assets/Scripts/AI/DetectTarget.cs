
using UnityEngine;
using System.Collections;

public class DetectTarget : MonoBehaviour 
{
	public float MaxDetectDistance = 4.0f;
	public float FieldOfView = 45.0f;
	public float SearchCooldownRate = 5.0f;
	public float SearchRate = 0.1f;
	public Vector3 ForwardDirection = Vector3.forward;
	public bool TargetPredators = false;
	public bool canOverride = false;
	
	public Transform SecondaryTarget;
	
	private bool Targeted = false;
	private float SearchCooldown = 5.0f;
	
	Idle idle;
	AIRotate rotate;
	Hunt hunt;
	Predation pred;
	  
	void Start() 
	{
		SearchCooldown = SearchCooldownRate;
		
		idle = GetComponent<Idle>();
		rotate = GetComponent<AIRotate>();
		hunt = GetComponent<Hunt>();
		if( GetComponent<Predation>() ) pred = GetComponent<Predation>();
		
	   // StartCoroutine( SearchForTarget() );
		Targetify( SecondaryTarget );
	}
	
	public void Targetify( Transform Target, bool overrideOverride = true )
	{
		if( !canOverride && !overrideOverride ) return;
		//print ( "DetectTarget: Found Target" );
		
		Targeted = true;
		
		idle.Deactivate();
		rotate.Target = Target;
		hunt.Target = Target;
		if( pred ) pred.Target( Target );
		
		//SearchCooldown = SearchCooldownRate;
	}
	
	/*private void LostTarget( )
	{
		//print ( "DetectTarget: Lost Target" );
		
		Targeted = false;
		
		idle.Activate();
		rotate.Target = null;
		hunt.Target = null;
		if( pred ) pred.Untarget();
		
	    StartCoroutine( SearchForTarget() );
	}
	
	private bool CanSeeIntrestingThing( Transform Target )
	{
	    RaycastHit hit;
		
		if( Target.GetComponent<Predation>() && !TargetPredators ) return false;
		
		float distance = Vector3.Distance( Target.transform.position, transform.position );
		if( distance > MaxDetectDistance ) return false;
		
	    Vector3 rayDirection = Target.transform.position - transform.position;
	    if( ( Vector3.Angle( rayDirection, ForwardDirection ) ) > FieldOfView ) return false;
		
	    if( Physics.Raycast( transform.position, rayDirection, out hit ) ) 
		{
	    	if( hit.transform != Target.transform ) return false;
		} else return false;
		
	    return true;
	}
	
	IEnumerator SearchForTarget()
	{
		while( !Targeted ) 
		{ 
			GameObject[] p = GameObject.FindGameObjectsWithTag( "Player" );
			GameObject[] l = GameObject.FindGameObjectsWithTag( "Lympha" );
			GameObject[] rits = new GameObject[ p.GetLength( 0 ) + l.GetLength( 0 ) ];
			p.CopyTo( rits, 0 );
			l.CopyTo( rits, p.GetLength( 0 ) );
	
			foreach( GameObject rit in rits ) 
			{
				if( CanSeeIntrestingThing( rit.transform ) )
					Targetify( rit.transform );
			}
			
			yield return new WaitForSeconds( SearchRate );
		}
	}*/
	
	void FixedUpdate( ) 
	{
		/*if( Targeted )
		{
			SearchCooldown -= Time.deltaTime;
			if( SearchCooldown < 0.0f ) Targetify( SecondaryTarget );
		} */
	}
}
