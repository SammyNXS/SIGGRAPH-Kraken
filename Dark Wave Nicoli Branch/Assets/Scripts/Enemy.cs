using UnityEngine;
using System.Collections;
/*This function serves as the base for all enemies
*/
public class Enemy : NPC 
{


	public GameObject[] targets;
	public Vector2 targetLastPosition;

	

	// Use this for initialization
	void Start () 
	{
		health = maxHealth;
		energy = maxEnergy;
		startPosition = transform.position;
	}
	//Updates the target variable every frame to track the player object
	void Update()
	{
		shotSpawnPosition = transform.position;

		if(target != null && Physics.Raycast (transform.position, target.transform.position - transform.position, sensorRange))
		{
			targetLastPosition = target.transform.position;
			inSight = true;
		}
		else inSight = false;

	}

	//Controls reactions to collisions
	void OnCollisionEnter(Collision col)
	{
		if((stun == 0) && (col.gameObject.layer == 8))
		{
			gameObject.GetComponent<Entity>().health -= col.gameObject.GetComponent<Entity>().touchDamage;
			stun = 20;
		}
	}
}
