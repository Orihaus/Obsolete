using UnityEngine;
using System.Collections;

public class Hurt : MonoBehaviour 
{
	public int HurtAmount = 50;
	public float Frequency = 4.0f;
	
	private float Cooldown;
	
	void Start()
	{
		Cooldown = Frequency;
	}
	
    void OnTriggerStay( Collider other ) 
	{
		if( Cooldown > 0.0f ) return;
		Cooldown = Frequency;
		
		Heart h = other.GetComponent<Heart>();
        if( h ) h.TakeLympha( HurtAmount );
    }
	
	void Update()
	{
		Cooldown -= Time.deltaTime;
	}
}
