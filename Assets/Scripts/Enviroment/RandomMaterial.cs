using UnityEngine;
using System.Collections;

public class RandomMaterial : MonoBehaviour 
{
	public Material[] Materials;
	
	void Start () 
	{
		Random.seed++;
		gameObject.renderer.material = Materials[ Random.Range( 0, Materials.Length ) ];
	}
}
