using UnityEngine;
using System.Collections;

/*
 * This code serves as the base for all game assets that will change dynamically during play.
 * Any variables created but not assigned in this script are either set in the child scripts or in the editor.
 * At the moment energy and max energy are not implemented but the plan is to have each use of an attack drain 
 * energy that will be replenished over time by the status update function.
*/
public class Entity : MonoBehaviour 
{
	//Status Variables
	public int health;
	public int maxHealth;	//set in editor
	public int energy;
	public int maxEnergy;	//set in editor
	public int touchDamage;	//set in editor
	public int stun = 0;
	public int facing = 1;

	//Movement variables
	public float baseSpeed;	//set in editor
	public float jumpHeight;// set in editor
	public float falling = -20;
	public int direction;
	public bool isJumping;

	public bool terrainTouch = false;
	public float yMove = 0;
	public float environmentMoveX = 0;
	public float environmentMoveY = 0;
	public Vector2 movement;

	//Shot attack variables
	public int shotAttackType;

	public int shotCooldown1 = 50;
	public int energyDrain1 = 0;
	public int currentCooldown1 = 0;
	public GameObject shot1;

	public int shotCooldown2 = 50;
	public int energyDrain2 = 5;
	public int currentCooldown2 = 0;
	public GameObject shot2;

	public int shotCooldown3 = 50;
	public int energyDrain3 = 10;
	public int currentCooldown3 = 0;
	public GameObject shot3;

	public int shotCooldown4 = 50;
	public int energyDrain4 = 20;
	public int currentCooldown4 = 0;
	public GameObject shot4;
	
	public Vector2 shotSpawnPosition;
	public Quaternion shotSpawnRotation;

	// Animation
	public bool walkFlag;
	public int columns = 8;
	public int rows = 2;
	protected Vector2 size; 
	protected Vector2 offset; 
	public int walk;
	public int standIndex;

	protected void Start()
	{

	}
	
	protected void setOffset(int index){
		//Debug.Log(index);
		int columnIndex = index % columns;
		int rowIndex = index / columns;
		offset = new Vector2(size.x * columnIndex,1f-size.y-rowIndex*size.y);
	}


	//Function used to update entity status. Called from the fixed update of the child object
	protected void StatusUpdate()
	{
		if(stun > 0) stun--;
		if(currentCooldown1 > 0) currentCooldown1--;
		if(currentCooldown2 > 0) currentCooldown2--;
		if(currentCooldown3 > 0) currentCooldown3--;
		if(currentCooldown4 > 0) currentCooldown4--;
		if(health < 1) Destroy(gameObject);
		//if(health < 1) Stub for destruction animation control
	}

	//Function used to move entities on screen. Called from the fixed update of the child object
	public void Movement()
	{
		float finalSpeedX;
		float finalSpeedY;
		
		if(direction != 0)
			facing = direction;
		
		finalSpeedX = (baseSpeed * direction) + environmentMoveX;
		
		if(terrainTouch)
			if(isJumping)
				yMove = jumpHeight;
		else
			yMove = 0;
		else if(yMove > falling)
			yMove--;
		
		finalSpeedY = yMove + environmentMoveY;
		
		movement = new Vector2(finalSpeedX * Time.deltaTime, finalSpeedY * Time.deltaTime);
		
		transform.Translate(movement);
		AnimationController();
	}

	//Function controlling the usage of shot attacks. May eventually be expanded control of melee attacks.
		//Called by the child function when the conditions have been.
	public void ShotAttack()
	{
		switch(shotAttackType)
		{
			case 1:
				if(currentCooldown1 == 0)
				{
					Instantiate(shot1, shotSpawnPosition, shotSpawnRotation);
					currentCooldown1 = shotCooldown1;
					energy -= energyDrain1;
				}
				break;
			case 2:
				if(currentCooldown2 == 0)
				{
					Instantiate(shot2, shotSpawnPosition, shotSpawnRotation);
					currentCooldown2 = shotCooldown2;
					energy -= energyDrain2;
				}
				break;
			case 3:
				if(currentCooldown3 == 0)
				{
					GameObject b = (GameObject)Instantiate(shot3, shotSpawnPosition, shotSpawnRotation);
					b.gameObject.SendMessage("TheStart",facing);
					currentCooldown3 = shotCooldown3;
					energy -= energyDrain3;
				}
				break;
			case 4:
				if(currentCooldown1 == 0)
				{
					Transform b = (Transform)Instantiate(shot4, shotSpawnPosition, shotSpawnRotation);
					currentCooldown4 = shotCooldown4;
					energy -= energyDrain4;
				}
				break;
		}
	}

	//Stub function for implementation of an animation controller
	protected virtual void AnimationController()
	{
		//if(stun) this.gameObject.
	}

	/*Function designed to track if an entity is touching the ground (terrainTouch) in order to control jump commands, 
	 * falling, and being affected by terrain movement.  Also recognizes if an entity hits a Death object
	 * Collisions don't occur between objects if they are both on the player or enemy layers.
	 * There may be a better way to implement entities being affected by terrain movement
	*/
	void OnCollisionStay(Collision col)
	{
		if(col.gameObject.tag == "Standable")
		{
			terrainTouch = true;
			if(col.gameObject.GetComponent<MovingPanel>() != null)
		    {
				environmentMoveX = col.gameObject.GetComponent<MovingPanel>().xMoveSpeed;
				environmentMoveY = col.gameObject.GetComponent<MovingPanel>().yMoveSpeed;
			}
		}
		else if(col.gameObject.tag == "Death")
		{
			health = 0;
		}
	}

	//Function resets terrainTouch and enviromental movement variables
	void OnCollisionExit(Collision col)
	{
		terrainTouch = false;
		environmentMoveX = 0;
		environmentMoveY = 0;
	}
}
