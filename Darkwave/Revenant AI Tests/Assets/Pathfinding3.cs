using UnityEngine;
using System.Collections;

public class Pathfinding3 : MonoBehaviour 
{
	public Transform destination;
	public double agroDistance;
	public double attackDistance;

	private Transform target;
	private NavMeshAgent agent;

	GameObject[] players;
	Transform _self;
	
	void Start ()  //Initialization...
	{
		agent = gameObject.GetComponent<NavMeshAgent> ();
		_self = GetComponent<Transform> ();
		players = GameObject.FindGameObjectsWithTag ("Player");

		target = destination;
	}
	
	void Update() //Updates the code.
	{
		agent.SetDestination (target.position);

		FindPlayer ();
		double distance = CheckTargetDistance ();
		if (distance < attackDistance) 
		{
			Hit ();
		} 
		else
			agent.speed = 2;


	}


	void FindPlayer() //Finds players and calls IsAgro to determine if the entity should change targets.
	{
		foreach (GameObject player in players)
		{
			if(IsAgro(player))
			{
				target = player.transform;
				print ("Agro");
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

	double CheckTargetDistance()
	{
		double distance;
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
}
