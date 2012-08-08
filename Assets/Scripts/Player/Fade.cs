using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour 
{
	public float Amount = 1.0f;
	public Material FadeMaterial;

	void Update () 
	{
		Amount -= Time.deltaTime;
		if(Amount < 0.0f )return;
		
		float f = FadeMaterial.GetFloat( "_Fade" );
		f = 1.0f - Amount;
		FadeMaterial.SetFloat( "_Fade", f );
	}
}
