using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	public Material UpMaterial;
	public Material LeftMaterial;
	public Material RightMaterial;
	
	private bool UpSelected = true;
	private bool LeftSelected = false;
	private bool RightSelected = false;

	void SetChildMaterials( Material nm )
	{
		Renderer[] r = GetComponentsInChildren<Renderer>();
		r[0].material = nm;
		r[1].material = nm;
		r[2].material = nm;
	}
	
	void Update() 
	{
		if( Input.GetKeyDown( KeyCode.UpArrow ) )
		{
			UpSelected = true;
			LeftSelected = false;
			RightSelected = false;
		}
		
		if( Input.GetKeyDown( KeyCode.LeftArrow ) )
		{
			UpSelected = false;
			LeftSelected = true;
			RightSelected = false;
		}
		
		if( Input.GetKeyDown( KeyCode.RightArrow ) || Input.GetKeyDown( KeyCode.DownArrow ) )
		{
			UpSelected = false;
			LeftSelected = false;
			RightSelected = true;
		}
		
		if( UpSelected )
			SetChildMaterials( UpMaterial );
		
		if( RightSelected )
			SetChildMaterials( RightMaterial );
		
		if( LeftSelected )
			SetChildMaterials( LeftMaterial );
		
		if( Input.GetKeyDown( KeyCode.Space ) )
		{
			if( UpSelected )
				Application.LoadLevel( "Docks" ); 
			
			//if( RightSelected )
			//	SetChildMaterials( RightMaterial );
			
			if( LeftSelected )
				Application.Quit();
		}
	}
}
