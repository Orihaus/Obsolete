using UnityEngine;
using System.Collections;

public class FlickerMaterial : MonoBehaviour
{
	public Material MaterialOne;
	public Material MaterialTwo;
	public float CoolDown = 0.1f;
	
	private float CurrentCoolDown = 0.0f;
	private bool CurrentMaterialOne = false;

	void Update()
	{
		CurrentCoolDown -= Time.deltaTime;
		
		if( CurrentCoolDown < 0.0f )
		{
			CurrentMaterialOne = !CurrentMaterialOne;
			if( CurrentMaterialOne )
				gameObject.renderer.material = MaterialOne;
			else
				gameObject.renderer.material = MaterialTwo;
			
			Random.seed++;
			
			CurrentCoolDown = Random.value * CoolDown;
		}
	}
}
