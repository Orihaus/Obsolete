using UnityEngine;
using System.Collections;

public class RandomPlaybackOffset : MonoBehaviour 
{
	public float Multiplier = 2.0f;
	
	void Start() 
	{
		audio.time += Random.value * Multiplier;
	}
}
