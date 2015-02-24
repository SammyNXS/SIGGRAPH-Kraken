using UnityEngine;
using System.Collections;

public class Character : Entity 
{
	float jumpCounter = 0.0F;//Used for MoveController()
	float hRotation = 0F, vRotation = 0F;//Used in CameraController()

	int weaponChoice = 0;
	public GameObject[] weapons;

	void Start()
	{
		EntityStart();
	}

	void Update() 
	{
		EntityUpdate();
		CameraController();

		if(health>0)
		{
			MoveController();
			WeaponController();
		}
	}

	void MoveController()
	{
		float jumpSpeed = 20.0F;
		float jumpPower = .5F;

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

	void CameraController()
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

	void WeaponController()
	{
		//Weapon chooser
		if(Input.GetKeyDown(KeyCode.Alpha0)) 
		{
			weapons[weaponChoice].SetActive(false);
			weaponChoice=0;
			weapons[weaponChoice].SetActive(true);

		}
		else if(Input.GetKeyDown(KeyCode.Alpha1)) 
		{
			weapons[weaponChoice].SetActive(false);
			weaponChoice=1;
			weapons[weaponChoice].SetActive(true);
		}
		else if(Input.GetKeyDown(KeyCode.Alpha2)) 
		{
			weapons[weaponChoice].SetActive(false);
			weaponChoice=2;
			weapons[weaponChoice].SetActive(true);

		}
		else if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			weapons[weaponChoice].SetActive(false);
			weaponChoice=3;
			weapons[weaponChoice].SetActive(true);
		}

		//Attack controller
		if(Input.GetButtonDown("Fire1")) weapons[weaponChoice].SendMessage("MainAttackController", true);
		else if(Input.GetButtonDown("Fire2")) weapons[weaponChoice].SendMessage("SecondaryAttackController", true);
		
		if(Input.GetButtonUp("Fire1")) weapons[weaponChoice].SendMessage("MainAttackController", false);
		else if(Input.GetButtonUp("Fire2")) weapons[weaponChoice].SendMessage("SecondaryAttackController", false);
	}
}
