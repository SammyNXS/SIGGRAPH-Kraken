using UnityEngine;
using System.Collections;
/*Switch script. Each switch is numbered in the editor and given a type and trigger as well as a key object or layer. 
 * The constant boolean decides if the contact has to remain to keep the switch flipped and the contactFlag controls this unswitching behavior
 * Cooldown and current cooldown are designed to limit accidental multihits to a switch
 * Gates collects the list of all gates in the level to send the message to.
 * The types are
 * 0 - One way(can be switched on but not off)
 * 1 - Multiposition(can be switched to multiple positions)
 * The triggers are 
 * 0 - triggered by contact with anything
 * 1 - triggered by contact with something in a specified layer
 * 2 - triggered by contact with a specified key object(can be a specific entity)
 */
public class Switch : MonoBehaviour 
{
	public int switchNumber, switchType, positions, currentPosition = 0, switchTrigger, keyLayer;
	public GameObject keyObject;
	public bool constant;
	bool contactFlag=false;

	int cooldown = 30, currentCooldown = 0, i;
	GameObject[] gates;

	int[] identifiers;

	// Use this for initialization
	void Start () 
	{
		gates = GameObject.FindGameObjectsWithTag("Gate");
		identifiers = new int[2];
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(currentCooldown > 0)currentCooldown --;
	}

	void OnCollisionEnter(Collision col)
	{
		if(currentCooldown == 0)
		{
			currentCooldown = cooldown;

			switch(switchType)
			{
				case 0: //one way
					switch(switchTrigger)
					{
						case 0: //anything
							if(currentPosition < positions-1) 
							{
								currentPosition += 1;
								contactFlag=true;
							}
							break;
						case 1: //specific layer
							if((currentPosition < positions-1) && (col.gameObject.layer == keyLayer))
							{
								currentPosition += 1;
								contactFlag=true;
							}
							break;
						case 2: //specific object
							if((currentPosition < positions-1) && (col.gameObject == keyObject))
							{
								currentPosition += 1;
								contactFlag=true;
							}
							break;
					}

				break;
				case 1: //multiposition
					switch(switchTrigger)
					{
					case 0: //anything
						currentPosition = (currentPosition + 1) % positions;
						contactFlag=true;
						break;
					case 1: //specific layer
						if(col.gameObject.layer == keyLayer) 
						{
							currentPosition = (currentPosition + 1) % positions;
							contactFlag=true;
						}
						break;
					case 2: //specific object
						if(col.gameObject == keyObject)
						{
							currentPosition = (currentPosition + 1) % positions;
							contactFlag=true;
						}
						break;
					}
				break;
			}
			Debug.Log("Enter");
			SendNewMessage(switchNumber, currentPosition);
		}
	}

	void OnCollisionExit(Collision col)
	{
		if (constant && contactFlag)
		{
			switch(switchTrigger)
			{
			case 0: //anything
				currentPosition--;
				contactFlag=false;
				break;
			case 1: //specific layer
				if(col.gameObject.layer == keyLayer) 
				{
					currentPosition--;
					contactFlag=false;
				}
				break;
			case 2: //specific object
				if(col.gameObject == keyObject) 
				{
					currentPosition--;
					contactFlag=false;
				}
				break;
			}
			Debug.Log("Exit");
			SendNewMessage(switchNumber, currentPosition);
		}

	}

	void SendNewMessage(int switchNumber, int currentPosition)
	{
		identifiers[0] = switchNumber;
		identifiers[1] = currentPosition;

		Debug.Log ("SEND MESSAGE Switch:" + identifiers[0] + "Position: " + identifiers[1]);

		for(i = 0; i<gates.Length; i++)
		{
				gates[i].SendMessage("GateControl", identifiers);
		}
	}
}
	