using UnityEngine;
using System.Collections;
/*This function serves as the base for all enemies
*/
public class Enemy : Entity 
{
	//Behavior variables (set in editor)
	public int behavior;
	public int patrolSize;
	public int sensorRange;
	public int distance;
	public GameObject target;
	public Vector2 startPosition;


	// Use this for initialization
	protected void Start () 
	{
		base.Start();
		health = maxHealth;
		energy = maxEnergy;
		startPosition = transform.position;
	}
	//Updates the target variable every frame to track the player object
	void Update()
	{
		target = GameObject.FindWithTag("Player");
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
