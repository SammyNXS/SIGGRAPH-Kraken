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
	public int stun=0;

	//Status effects.
	public float hasFocus = 0; //Currently reduces weapon cooldowns. Will change to improving accuracy and melee cleave.
	public float hasRegen = 0; //Grants health.
	private float heal = 0; // Used with hasRegen to grant health.
	public float hasDegen = 0;
	private float drain = 0;

	//Movement variables
	public float baseSpeed;	//set in editor

	public bool terrainTouch = false;
	public float yMove = 0;
	public float environmentMoveX = 0;
	public float environmentMoveY = 0;
	public Vector2 movement;

	//Attack variables
	public int weaponChoice = 1;

	public float cooldown1 = 2;
	public int energyDrain1 = 0;
	public float currentCooldown1 = 0;
	public GameObject attack1;

	public float cooldown2 = 2;
	public int energyDrain2 = 5;
	public float currentCooldown2 = 0;
	public GameObject attack2;

	public float cooldown3 = 2;
	public int energyDrain3 = 10;
	public float currentCooldown3 = 0;
	public GameObject attack3;

	public float cooldown4 = 2;
	public int energyDrain4 = 20;
	public float currentCooldown4 = 0;
	public GameObject attack4;
	
	public Vector3 shotSpawnPosition;
	public Quaternion shotSpawnRotation;

	//private GameObject arch;

	//Function used to update entity status. Called from the fixed update of the child object
	public void StatusUpdate()
	{
		if(stun > 0) stun--;
		Cooldowns(); // Manages weapon rate of fire. Affected by focus buff until accuracy code is up.
		if(health < 1) Destroy(gameObject);
		//if(health < 1) Stub for destruction animation control

		// Countdown status effects.
		EffectsTimer(); // Allows effects to run their time and expire.

		/* Debugging code to test if the Architect has a focus value of zero.
		arch = GameObject.Find("Character");
		if (arch.GetComponent<Entity>().hasFocus <= 0)
		{
			Debug.Log("No Focus!");
		}
		*/
	}

	// Subtracts the timer on status effects by the timer value, simulating a countdown.
	void EffectsTimer()
	{
		// Count down focus if focus is active.
		if (hasFocus > 0)
		{
			hasFocus -= Time.deltaTime;
			if (hasFocus < 0) // Prevents hasFocus from dropping below 0.
			{
				hasFocus = 0;
			}
		}
		/* Counts down regeneraion while counting up heal at the same rate.
		 * If heal is greater than or equal to 1 while the entity is injured,
		 * the entity heals 1 health and heal is set to heal minus the same value but rounded down.
		 * Regen and heal are set to zero if regen is zero or negative.
		 */
		if (hasRegen > 0)
		{
			hasRegen -= Time.deltaTime;
			heal += Time.deltaTime;
			if (heal >= 1 && health < maxHealth) // Heals entity
			{
				health += 1;
				heal -= Mathf.Floor(heal);
			}
			if (hasRegen <= 0) // Prevents hasRegen from dropping below 0.
			{
				hasRegen = 0;
				heal = 0;
			}
		}
		if (hasDegen > 0)
		{
			hasDegen -= Time.deltaTime;
			drain += Time.deltaTime;
			if (drain >= 1) // Heals entity
			{
				health --;
				drain -= Mathf.Floor(drain);
			}
			if (hasDegen <= 0) // Prevents hasRegen from dropping below 0.
			{
				hasDegen = 0;
				drain = 0;
			}
		}
	}

	/* Called every StatusUpdate() call. Reduces current cooldowns of weapons by an average of 1 second every second.
	 * Reduced by 2 per second if the entity has the focus buff.
	 */
	void Cooldowns ()
	{
		// Essentially halves cooldowns.
		if (hasFocus > 0)
		{
			if(currentCooldown1 > 0) currentCooldown1-=2*Time.deltaTime;
			if(currentCooldown2 > 0) currentCooldown2-=2*Time.deltaTime;
			if(currentCooldown3 > 0) currentCooldown3-=2*Time.deltaTime;
			if(currentCooldown4 > 0) currentCooldown4-=2*Time.deltaTime;
		} 
		// Regular cooldowns.
		else if (hasFocus <= 0)
		{
			if(currentCooldown1 > 0) currentCooldown1-=Time.deltaTime;
			if(currentCooldown2 > 0) currentCooldown2-=Time.deltaTime;
			if(currentCooldown3 > 0) currentCooldown3-=Time.deltaTime;
			if(currentCooldown4 > 0) currentCooldown4-=Time.deltaTime;
		}
	}

	//Function controlling the usage of shot attacks. May eventually be expanded control of melee attacks.
		//Called by the child function when the conditions have been.
	public void Attack()
	{
		switch(weaponChoice)
		{
			case 1:
				if(currentCooldown1 <= 0)
				{
					Instantiate(attack1, shotSpawnPosition, shotSpawnRotation);
					currentCooldown1 = cooldown1;
					energy -= energyDrain1;
				}
				break;
			case 2:
				if(currentCooldown2 <= 0)
				{
					Instantiate(attack2, shotSpawnPosition, shotSpawnRotation);
					currentCooldown2 = cooldown2;
					energy -= energyDrain2;
				}
				break;
			case 3:
				if(currentCooldown3 <= 0)
				{
					Instantiate(attack3, shotSpawnPosition, shotSpawnRotation);
					currentCooldown3 = cooldown3;
					energy -= energyDrain3;
				}
				break;
			case 4:
				if(currentCooldown1 <= 0)
				{
					Instantiate(attack4, shotSpawnPosition, shotSpawnRotation);
					currentCooldown4 = cooldown4;
					energy -= energyDrain4;
				}
				break;
		}
	}

	void weaponSwitch(int newWeapon)
	{
		//Weapon switch animation
		weaponChoice = newWeapon;
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
		if(col.gameObject.tag == "Death")
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
