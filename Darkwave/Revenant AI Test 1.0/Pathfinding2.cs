using UnityEngine;
using System.Collections;

public class Pathfinding2 : MonoBehaviour 
{
	public Transform destination;

	private NavMeshAgent agent;

	void Start () 
	{

	}

	void Update()
	{
		agent = gameObject.GetComponent<NavMeshAgent> ();

		agent.SetDestination (destination.position);
	}
}
