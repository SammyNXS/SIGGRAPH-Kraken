using UnityEngine;
using System.Collections;

public class Architect : Character
{
	int Marksmanship = 0;
	bool HuntersMomentum = false;

	int Structures = 0;
	int Perception = 0;

	// Use this for initialization
	void Update()
	{

	}

	// Called when the player adjusts ability points.
	void AbilityUnlockMinor( int weapon, int prep, int spirit)
	{
		// Sets abilities to true or false depending on how many points each trait line has.
		if (weapon >= 1)
		{
			HuntersMomentum = true;
		}
		else
		{
			HuntersMomentum = false;
		}
	}

	// Called when the level starts
	void AbilitySet()
	{
		if (HuntersMomentum == true)
		{
			// Adjusts touch damage of attack1, attack2, attack3, attack4
		}
	}
}
