using UnityEngine;
using System.Collections;

public class AttributeHuntersMomemtum : MonoBehaviour {

	public float cooldown; // The trait's base cooldown.
	private float cooldownCounter; // Uses cooldown to calculate periodic activation.

	public float focusIncrease; // How many seconds of focus is granted per periodica activation.

	private GameObject player; // The Architect, in this case. Set in Start();
	private Architect architect; // Set to the Architect's Architect.cs script.

	// Use this for initialization
	void Start ()
	{
		cooldownCounter = cooldown;
		player = GameObject.Find("Character");
		architect = player.GetComponent<Architect>();
	}

	// Called every frame.
	void Update()
	{
		// Activates the trait effect if 
		if (Time.time >= cooldownCounter)
		{
			Effect();
		}
	}

	void Effect()
	{
		architect.hasFocus += focusIncrease; // Increases the Architect's focus by focusIncrease.
		cooldownCounter += cooldown; // Increases cooldownCounter by cooldown.
	}
}
