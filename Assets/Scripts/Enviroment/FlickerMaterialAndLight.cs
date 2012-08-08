using UnityEngine;
using System.Collections;

public class FlickerMaterialAndLight : MonoBehaviour
{
	public Material MaterialOne;
	public Material MaterialTwo;
	public float BrightnessOne = 2.0f;
	public float BrightnessTwo = 0.0f;
	public GameObject TargetLight;
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
			{
				TargetLight.light.intensity = BrightnessOne;
				gameObject.renderer.material = MaterialOne;
			}
			else 
			{
				TargetLight.light.intensity = BrightnessTwo;
				gameObject.renderer.material = MaterialTwo;
			}
			
			Random.seed++;
			CurrentCoolDown = Random.value * CoolDown;
		}
	}
}
