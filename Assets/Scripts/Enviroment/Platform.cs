using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour 
{
	public bool Active = false;
	public float Speed = 0.4f;
	public bool UseStopHeight = true;
	public float StopHeight = 10.0f;
	public float StopTime = 2.0f;
	public float StartDelay = 1.0f;
	public Vector3 Direction = Vector3.up;
	
	private Vector3 position;
	private bool StartDelayOn = false;
	private float StartCountdown;
	private float CurrentTime = 0.0f;
	
	void Start() 
	{
		position = transform.position;
		StartCountdown = StartDelay;
	}
	
	public void Activate()
	{
		StartDelayOn = true;
	}
	
	void Update() 
	{
		if( StartDelayOn ) 
		{
			StartCountdown -= Time.deltaTime;
			if( StartCountdown < 0.0f )
			{
				StartDelayOn = false;
				Active = true;
			}
		}
		
		if( Active ) 
		{
			position += Direction * Time.deltaTime * Speed;
			CurrentTime += Time.deltaTime;
			
			if( position.y > StopHeight && UseStopHeight )
			{
				Active = false;
				position.y = StopHeight;
			}
			
			if( CurrentTime > StopTime && !UseStopHeight )
				Active = false;
			
			transform.position = position;
		}
	}
	
	void OnTriggerEnter()
	{
		StartDelayOn = true;
	}
}
