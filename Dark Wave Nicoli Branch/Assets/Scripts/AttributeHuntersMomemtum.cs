using UnityEngine;
using System.Collections;

public class AttributeHuntersMomemtum : MonoBehaviour {

	public int cooldown;
	GameObject player;
	public bool run;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.Find("Character");
		StartCoroutine (Effect()); // Reference the game clock instead of using coroutine.
	}
	
	public IEnumerator Effect()
	{
		while(true)
		{
			if (run)
			{
				Architect architect = player.GetComponent<Architect>();
				architect.hasFocus = +1000;
			}
			yield return new WaitForSeconds (cooldown);
		}
	}
}
