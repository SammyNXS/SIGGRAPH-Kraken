using UnityEngine;
using System.Collections;

//This code serves as the base for floating enemies
public class ETurret : Enemy 
{
	// FixedUpdate is called at a set interval
	void FixedUpdate () 
	{
		EntityAI();
		
		//Debug.DrawRay (transform.position, target.transform.position - transform.position, rayColor);
		
		Movement();
		StatusUpdate();
	}

	//Controls the behavior of the enemy object
	void EntityAI()
	{	
		//if the player is in sight
		if(target != null && Physics.Raycast (transform.position, target.transform.position - transform.position, sensorRange))
		{
			direction = target.transform.position.x - transform.position.x > 0 ? 1:-1;
			
			//shotSpawnPosition = new Vector2(transform.position.x+(2.5F * facing), transform.position.y);
			//shotSpawnRotation = Quaternion.Euler(0,0,facing==1?0:180);
			
			shotSpawnPosition = transform.position;
			
			Quaternion newRotation = Quaternion.LookRotation(target.transform.position - transform.position, Vector3.up);
			newRotation *= Quaternion.FromToRotation(Vector3.forward, Vector3.left);
			shotSpawnRotation = newRotation;
			
			shotAttackType = 1;
			ShotAttack();
			
			startPosition = transform.position;
		}
		//if the player is not in sight
		else
			switch(behavior)
		{
			
			case 0:
			if((int)Vector2.Distance(startPosition, transform.position) > patrolSize)
			{
				direction *= -1;
			}
			break;
			default:
			direction = 0;
			break;
		}
	}
}
