using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour 
{
	public int weaponType; // 0 Melee, 1 Ranged
	bool mainAttacking;
	bool secondaryAttacking;
	public int cooldown = 50;
	public int currentCooldown = 0;
	
	public int energy = 100;
	public int currentEnergy;
	public int energyDrain = 0;
	public GameObject shot;

	// Use this for initialization
	void Start () 
	{
		currentEnergy = energy;
	}

	void FixedUpdate()
	{
		if(currentEnergy < energy) currentEnergy++;
		if(currentCooldown > 0) currentCooldown--;

		if(mainAttacking)MainAttack();
		else if(secondaryAttacking)SecondaryAttack();
	}

	void MainAttack()
	{
		AttackAnimation();
		if(currentCooldown == 0)
		{
			if(weaponType == 0)
			{
				//Weapon swing stub
			}
			if(weaponType == 1)
			{
				//Shot spawn position temporarily changed until correct model can be imported
				//Vector3 shotSpawnPosition = gameObject.transform.position;
				Vector3 shotSpawnPosition = new Vector3(
					gameObject.transform.position.x, 
					gameObject.transform.position.y+.4f,
					gameObject.transform.position.z);
				Quaternion shotSpawnRotation = Camera.mainCamera.transform.rotation;

				Instantiate(shot, shotSpawnPosition, shotSpawnRotation);
				currentCooldown = cooldown;
				energy -= energyDrain;
			}
		}
	}

	void SecondaryAttack()
	{
	}

	void AttackAnimation()
	{
		//Trigger animation built into weapon object
	}

	public void MainAttackController(bool value)
	{
		mainAttacking = value;
	}

	public void SecondaryAttackController(bool value)
	{
		secondaryAttacking = value;
	}
}
