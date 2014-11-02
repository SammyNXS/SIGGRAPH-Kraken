using UnityEngine;
using System.Collections;

public class Pathfinding2 : MonoBehaviour 
{
	public Transform destination;
	private NavMeshAgent agent;

	Transform _player;
	Transform _transform;
	float attackRange = 10f;

	void Start () 
	{
		agent = gameObject.GetComponent<NavMeshAgent> ();

		_transform = GetComponent<Transform> ();
		_player = GameObject.Find("Player").GetComponent<Transform>();
		if (_player == null)
			Debug.LogError ("No Player on scene.");
	}

	void Update()
	{
		if (RadTest ()) 
		{
			agent.SetDestination (_player.position); //Move towards the player.
		} 
		else
		{
			DefAttack ();
		}


	}

	void DefAttack() //Return to the default attack destination of the enemy. 
	{

			agent.SetDestination (destination.position); //This is the initial destination of the enemy.
	}

	bool RadTest() //Tests the radius of the enemy for any players.
	{
		if (Vector3.Distance (_player.position, _transform.position) < attackRange) 
		{
			print ("Returning True"); //Debuging
			return true;
		}
		else 
		{
			print ("Returning False"); //Debuging
			return false;
		}
	}
}
