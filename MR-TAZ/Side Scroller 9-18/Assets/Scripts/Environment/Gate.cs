using UnityEngine;
using System.Collections;
/*Connected switch identifies which switch number will affect the gate. Speed determines how fast the gate will move between set positions.
 * GatePosition determines the positions the gate will move to when it recieves a signal from the connected switch.
 * 
 */
public class Gate : MonoBehaviour 
{
	public int connectedSwitch, speed;
	public Vector2[] gatePosition = new Vector2[4];

	int currentPosition, nextPosition, finalPosition, distanceTravelled;


	// Use this for initialization
	void Start () 
	{
		currentPosition = nextPosition = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(finalPosition > currentPosition) nextPosition = currentPosition+1;
		else if(finalPosition < currentPosition) nextPosition = currentPosition-1;
		
		transform.position = Vector3.Lerp(gatePosition[currentPosition], gatePosition[nextPosition], (speed*Time.deltaTime)*distanceTravelled++);

		if((Vector2)transform.position==gatePosition[nextPosition]) 
		{
			currentPosition = nextPosition;
			distanceTravelled=0;
		}
	}

	//Recieves messages from all switches but only reacts to ones sent by the connectedSwitch
	void GateControl(int[] identifiers)
	{
		Debug.Log ("Message recieved-Switch:" + identifiers[0] + " Position:" + identifiers[1]);
		if(identifiers[0] == connectedSwitch)
		{
			finalPosition = identifiers[1];
		}

	}
}
