using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour 
{
	float jumpCounter = 0.0F;//Used for MoveExample()
	public float hRotation = 0F, vRotation = 0F;//Used in CameraControlExample()
	public int health=0, energy=0;

	// Use this for initialization
	void Start () 
	{
	
	}
	

	void Update() 
	{
		MoveExample();
		CameraControlExample();
	}

	void SimpleMoveExample()
	{
		float speed = 3.0F;
		float rotateSpeed = 3.0F;

		CharacterController controller = GetComponent<CharacterController>();
		transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
		Vector3 forward = transform.TransformDirection(Vector3.forward);
		float curSpeed = speed * Input.GetAxis("Vertical");
		controller.SimpleMove(forward * curSpeed);
	}

	void MoveExample()
	{
		float speed = 6.0F;
		float jumpSpeed = 8.0F;
		float jumpPower = 10.0F;

		float gravity = 80.0F;
		Vector3 moveDirection = Vector3.zero;

		CharacterController controller = GetComponent<CharacterController>();

		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;

		if (controller.isGrounded) jumpCounter = jumpPower;
		else if(!controller.isGrounded && !Input.GetButton("Jump")) jumpCounter = 0;
		else if(jumpCounter > 0) jumpCounter -= 1*Time.deltaTime;

		if (Input.GetButton("Jump") && jumpCounter > 0) moveDirection.y = jumpSpeed;
			
		

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
		
	}

	void CameraControlExample()
	{
		float horizontalSpeed = 7.0F;
		float verticalSpeed = 7.0F;

		//Rotates Player on "X" Axis Acording to Mouse Input
		hRotation += horizontalSpeed * Input.GetAxis("Mouse X");
		transform.localEulerAngles = new Vector3(0, hRotation, 0);
		
		//Rotates Player on "Y" Axis Acording to Mouse Input
		vRotation = Mathf.Clamp(vRotation - verticalSpeed * Input.GetAxis("Mouse Y"), -90,90);
		Camera.mainCamera.transform.localEulerAngles = new Vector3(vRotation, 0, 0);
	}

}
