using UnityEngine;
using System.Collections;
/*Spawns player object if there is no object in the scene with the player tag
 */
public class CharacterSpawner : MonoBehaviour 
{
	public GameObject playerBody;//set in editor
	
	//Status Variables
	public int deathCounter = 0;
	public Vector3 spawnPoint = new Vector3(0,0,0);

	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad(transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{
		PlayerSpawn();
	}

	//Spawns player object and reduces lives count by 1.  If lives is already zero it instead loads the gameover scene.
	void PlayerSpawn()
	{
		if(!GameObject.FindWithTag("Player"))
		{
			deathCounter++;

			Instantiate(playerBody, spawnPoint, transform.rotation);

		}
	}
}