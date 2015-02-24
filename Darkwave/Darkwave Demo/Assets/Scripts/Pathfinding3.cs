using UnityEngine;
using System.Collections;

public class Pathfinding3 : MonoBehaviour
{
	public Transform destination;
	public double agroDistance;
	public double attackDistance;
	//public double startHealth;
	public float actualRadius;
	public float normalSpeed;

	private Transform target;
	private NavMeshAgent agent;
	//private double health;
	private float targetDistance;

	GameObject[] players;
	Transform _self;
	
	void Start ()  //Initialization...
	{
		agent = gameObject.GetComponent<NavMeshAgent> ();
		_self = GetComponent<Transform> ();
		players = GameObject.FindGameObjectsWithTag ("Player");

		target = destination;

		//health = startHealth;

	}
	
	void Update() //Updates the code.
	{

		targetDistance = CheckTargetDistance ();

		agent.SetDestination (target.position);

		FindPlayer ();
		double distance = CheckTargetDistance ();
		if (distance < attackDistance) 
		{
			Hit ();
		} 
		else
			agent.speed = normalSpeed;

		//LifeCheck ();

		//RadiusChange ();

	}


	void FindPlayer() //Finds players and calls IsAgro to determine if the entity should change targets.
	{
		foreach (GameObject player in players)
		{
			if(IsAgro(player))
			{
				target = player.transform;
				return;
			}
		}

		target = destination;
	}

	bool IsAgro(GameObject player) //Does the entity Agro?
	{
		if (Vector3.Distance(player.transform.position, _self.position) < agroDistance)
			return true;
		else
			return false;
	}

	float CheckTargetDistance()
	{
		float distance;
		distance = Vector3.Distance (_self.position, target.position);
		return distance;
	}

	void Hit()
	{
		agent.speed = 0;
		//Play "hit" sound
		//Play "hit" animation

		//agent.speed = 2;
	}

	//void die()
	//{
	//	Destroy(gameObject);

		// Dying animation (based on how killed???
		// Dying Sound (based on animation)
		// Any last effects that are triggered by death.
	//}

	//void LifeCheck()
	//{
	//	if (health <= 0) 
	//	{
	//		die ();
	//	} 
	//}

	//void DealDamage(double dealt)
	//{
	//	health -= dealt;
	//}

	//void RadiusChange()
	//{
		//Check Distance to target
		//Algorithem to make agent "radius" get closer to actual radius based on distance.

	//	float distanceFactor;
	//	float speedFactor;
	//	float newRadius;

	//	distanceFactor = (0.1f)*(Mathf.Sqrt (Mathf.Sqrt (Mathf.Abs (targetDistance))));
	//		if (distanceFactor > 1)
	//			distanceFactor = 1;
	//	speedFactor = (distanceFactor * ((normalSpeed - agent.velocity.magnitude) / normalSpeed));
	//		if (speedFactor < 0.001)
	//			speedFactor = 0;
	//	newRadius = actualRadius + distanceFactor - speedFactor;
	//
	//	//print ("Distanc Factor: " + distanceFactor); // DEBUGING
	//	//print ("Speed Factor: " + speedFactor); // DEBUGING
	//	// print ("New Radius: " + newRadius); // DEBUGING

	//	agent.radius = newRadius;
	//}
}
