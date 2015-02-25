using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour 
{
	public int weaponType; // 0 Melee, 1 Ranged
	public int secondaryAction; //0 Zoom, 1 Secondary Attack
	public bool hasScope;
	bool mainAction;
	bool SecondaryAction;
	public int cooldown = 50;
	public int currentCooldown = 0;
	public int energy = 100;
	public int currentEnergy;
	public int energyDrain = 0;
	public GameObject shot;
	Vector3 defaultPosition;

	// Use this for initialization
	void Start () 
	{
		defaultPosition = this.transform.localPosition;
		currentEnergy = energy;
	}

	void FixedUpdate()
	{
		if(currentEnergy < energy) currentEnergy++;
		if(currentCooldown > 0) currentCooldown--;

		MainAttack();
		SecondaryAttack();
	}

	void MainAttack()
	{
		if(mainAction)
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
	}

	void SecondaryAttack()
	{
		if(SecondaryAction)
		{
			if(secondaryAction==0)
			{
				gameObject.transform.localPosition = new Vector3(0,-0.7f,0);
				if(hasScope)
					;
			}
			
			if(secondaryAction==1)
			{
				if(!mainAction)
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
			}
		}
		else
		{
			if(secondaryAction==0)
			{
				gameObject.transform.localPosition = defaultPosition;
			}
		}
	}

	void AttackAnimation()
	{
		//Trigger animation built into weapon object
	}

	public void MainActionController(bool value)
	{
		mainAction = value;
	}

	public void SecondaryActionController(bool value)
	{
		SecondaryAction = value;
	}
}
