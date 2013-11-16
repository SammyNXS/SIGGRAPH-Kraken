using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public Rigidbody shot;
	public float moveSpeed = 0.5F;    
	public float jumpHeight = 10.0F;
	float jumpCounter = 0;

	public float gravity = -10.0F;
	static public int direction = 1;
	
	int attack = 0;
	float cooldown = 0;

	private Vector2 movement = Vector2.zero;
	
    
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		PlayerMoveController();
		PlayerAttackController();
	}
	
	void PlayerMoveController()
	{
		CharacterController controller = GetComponent<CharacterController>();
   
		movement.x = Input.GetAxis("Horizontal");
		if(movement.x < 0) direction = -1;
		else if(movement.x > 0) direction = 1;
		
		if(Input.GetKey(KeyCode.Space) && controller.isGrounded) 
		{
			movement.y = jumpHeight * Time.deltaTime;
			jumpCounter = 0.5F;
			Debug.Log("up");
		}
		
		if(jumpCounter > 0) 
		{
			jumpCounter-= 1*Time.deltaTime;
			Debug.Log("float");
		}
		else 
		{
			movement.y = gravity * Time.deltaTime;
			Debug.Log("grav");
		}
		
		controller.Move(movement);
	}
	void PlayerAttackController()
	{
		if(Input.GetKey(KeyCode.Q) && cooldown == 0)
		{
			Vector3 PlayerBody = new Vector3(gameObject.transform.position.x * direction, gameObject.transform.position.y, gameObject.transform.position.z);
			Rigidbody clone;
			if(direction == 1) clone = (Instantiate(shot, PlayerBody, transform.rotation) as Rigidbody);
			cooldown = 30;
		}
		if(cooldown > 0) cooldown -= 1;
	
	}
	
}
