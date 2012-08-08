using UnityEngine;

using System.Collections;



public class ExitAny : MonoBehaviour 
{
	void Update() 
	{
		if( Input.anyKeyDown && Time.timeSinceLevelLoad > 5.0f ) 
			Application.Quit();
	}

}
