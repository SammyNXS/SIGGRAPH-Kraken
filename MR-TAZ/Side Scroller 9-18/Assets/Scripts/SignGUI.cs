using UnityEngine;
using System.Collections;

public class SignGUI : MonoBehaviour
{
	private bool isNearPlayer = false;
	private int proximityCount = 0;
	public string text = "ERROR, NO TEXT";
	private int signTextWidth = 90;

	// Called when an object collides with the Sphere Collider.
	void OnTriggerStay(Collider other)
	{
		// If the object that collided is not the player, this function doesn't do anything.
		if (other.gameObject.name == "Player")
		{
			isNearPlayer = true; // This allows the OnGUI function to bring up the Gui.Box
			proximityCount = 50; // A timer that counts down in the Update function when the object leaves the Sphere Collider.
		}
	}

	// Called every tick.
	void Update()
	{
		// If the proximityCount integer is greater than zero, it will count down every tick. 
		// If it is zero, the OnGUI box is disabled until the Sphere Collider triggers again.
		if (proximityCount > 0)
		{
			proximityCount -= 1;
		}
		else
		{
			isNearPlayer = false;
		}
	}

	// Called every tick. Deals with GUI.
	private void OnGUI()
	{
		GUI.skin.box.wordWrap = true;
		int height = 1 + text.Length/signTextWidth;
		// Only activates when the player collides with the Sphere Collider, prompting a message in a box.
		if (isNearPlayer == true)
		{
			GUI.Box (new Rect (30, Screen.height-100, 400, 28*height), new GUIContent (text));
		}
	}
}
