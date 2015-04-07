using UnityEngine;
using System.Collections;

public class AttributeHuntersMomemtum : MonoBehaviour {

	public int cooldown; // The trait's cooldown.
	private GameObject player; // The Architect, in this case. Set in Start();
	public bool run; // Allows the trait to function. True if unlocked by the Architect.
	private Architect architect; // Set to the Architect's Architect.cs script.

	// Use this for initialization
	void Start ()
	{
		player = GameObject.Find("Character");
		architect = player.GetComponent<Architect>();
		StartCoroutine (Effect());
	}


	/*
	 * If this trait is unlocked for the Architect,
	 * it gives the Architect ten seconds of Focus every ten seconds.
	 */
	public IEnumerator Effect()
	{
		// Endless loop that pauses every ten seconds. The pause keeps the script from crashing the game.
		while(true)
		{
			//Debug.Log ("Hunter try");
			// True only if the Architect unlocks the trait, setting run to true in Architect.cs.
			if (run)
			{
				//Debug.Log ("Hunter Refresh");

				architect.hasFocus += 10; // Increases the Architect's focus by ten.
			}
			yield return new WaitForSeconds (cooldown); // Pauses the script for ten seconds.
		}
	}
}
