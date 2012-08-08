using UnityEngine;
using System.Collections;

public class NextMapOnKeydown : MonoBehaviour 
{
	void Update() 
	{
		if( Input.anyKeyDown ) Application.LoadLevel( "Obedear" );
	}
}
