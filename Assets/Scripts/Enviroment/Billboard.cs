using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour 
{
	public Transform Target;
	
	void FixedUpdate() 
	{
	    Quaternion targetRotation = Quaternion.LookRotation ( Target.position - transform.position, Vector3.right );
	   	transform.rotation = Quaternion.Slerp( transform.rotation, targetRotation, Time.deltaTime * 128.0f );
	}
}
