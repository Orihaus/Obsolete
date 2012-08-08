using UnityEngine;
using System.Collections;

public class SixDoF : MonoBehaviour 
{
    public float speed = 6.0F;
    private Vector3 moveDirection = Vector3.zero;
	
    void FixedUpdate() 
	{
        CharacterController controller = GetComponent<CharacterController>();
        moveDirection = new Vector3( Input.GetAxis( "Horizontal" ), 0, Input.GetAxis( "Vertical" ) );
        moveDirection = transform.TransformDirection( moveDirection );
        moveDirection *= speed;
        controller.Move( moveDirection * Time.deltaTime );
    }
}
