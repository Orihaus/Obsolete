using UnityEngine;
using System.Collections;

public class Heart : MonoBehaviour 
{
	public int Lympha = 50;
	public int MaxLympha = 100;
	public float LymphaCycleRate = 4.0f;
	public bool isMaster = true;
	
	private float LymphaCycle;
	
	void Start()
	{
		LymphaCycle = LymphaCycleRate;
	}
	
	public void GiveLympha( int amount )
	{
		Lympha += amount;
		if( Lympha > MaxLympha ) Lympha = MaxLympha;
	}
	
	public void TakeLympha( int amount )
	{
		Lympha -= amount;
	}

	void Update() 
	{
		if( !isMaster ) return;
		
		LymphaCycle -= Time.deltaTime;
		if( LymphaCycle < 0.0 ) 
		{
			LymphaCycle	= LymphaCycleRate;
			if( Lympha < 1 ) { Application.LoadLevel( Application.loadedLevel ); }
		}
	}
}
