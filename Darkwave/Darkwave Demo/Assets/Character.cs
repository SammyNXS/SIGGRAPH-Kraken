using UnityEngine;
using System.Collections;

public class Character : Entity 
{
	float jumpCounter = 0.0F;//Used for MoveExample()
	float hRotation = 0F, vRotation = 0F;//Used in CameraControlExample()

	int weaponChoice = 1;
	public GameObject weapon1;
	public GameObject weapon2;
	public GameObject weapon3;
	public GameObject weapon4;

	void Start()
	{
		EntityStart();
	}

	void Update() 
	{
		EntityUpdate();
		CameraControlExample();
		MoveController();
		if(Input.GetButtonDown("Fire1")) AttackController(0, true);
		else if(Input.GetButtonDown("Fire2")) AttackController(1, true);

		if(Input.GetButtonUp("Fire1")) AttackController(0, false);
		else if(Input.GetButtonUp("Fire2")) AttackController(1, false);
	}

	void MoveController()
	{
		float jumpSpeed = 20.0F;
		float jumpPower = 1.0F;

		float gravity = 500.0F;
		Vector3 moveDirection = Vector3.zero;

		CharacterController controller = GetComponent<CharacterController>();

		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= baseSpeed;

		if (controller.isGrounded) 
		{
			jumpCounter = jumpPower;
			moveDirection *= 2;
		}
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
		hRotation = (hRotation + horizontalSpeed * Input.GetAxis("Mouse X"))%360;
		transform.localEulerAngles = new Vector3(0, hRotation, 0);
		
		//Rotates Player on "Y" Axis Acording to Mouse Input
		vRotation = Mathf.Clamp(vRotation - verticalSpeed * Input.GetAxis("Mouse Y"), -90,90);
		Camera.mainCamera.transform.localEulerAngles = new Vector3(vRotation, 0, 0);

	}

	void AttackController(int attackType, bool attacking)
	{
		switch(weaponChoice)
		{
		case 1:
			if(attackType == 0)
				weapon1.SendMessage("MainAttackController", attacking);
			else weapon1.SendMessage("SecondaryAttackController", attacking);
			break;
		case 2:

			break;
		case 3:

			break;
		case 4:

			break;
		}
	}
}
