using UnityEngine;
using System.Collections;

public class Architect : Character
{
	GameObject player; // For referencing the Architect player.

	public int marksmanship; // Value from 0 to 9 determines how many attribute points are in Marksmanship.
	private bool huntersMomentum = false; // First Marksman trait.

	public int structures; // Value from 0 to 9 determines how many attribute points are in Structures.
	public int perception; // Value from 0 to 9 determines how many attribute points are in Perception.

	// Called on script initialization to setup the Architect player in the game world.
	void Start()
	{
		player = GameObject.Find("Character"); // Sets the player GameObject to the Architect.

		AbilityUnlockMinor(); // Sets the boolean minor traits to true or false based on attribute points.
		AbilitySet(); // Activates the trait abilities.
	}

	// Called when the player adjusts ability points.
	void AbilityUnlockMinor()
	{
		// Sets abilities to true or false depending on how many points each trait line has.
		if (marksmanship >= 1)
		{
			huntersMomentum = true;
		}
		else
		{
			huntersMomentum = false;
		}
	}

	// Called when the level starts
	void AbilitySet()
	{

		/* Current implementation: Also uses the AttributeHuntersMomemtum.cs script to give the Architect
		 * an initial 10 seconds of focus, and 10 seconds of focus every 10 seconds.
		 * 
		 * Final implementation: When the Architect's shot strikes an enemy's head,
		 * the Architect gains focus for 3? seconds.
		 */
		if (huntersMomentum == true)
		{
			hasFocus += 1000;
			AttributeHuntersMomemtum attributeHuntersMomemtum = player.GetComponent<AttributeHuntersMomemtum>();
			attributeHuntersMomemtum.run = true;
		}
	}
}
