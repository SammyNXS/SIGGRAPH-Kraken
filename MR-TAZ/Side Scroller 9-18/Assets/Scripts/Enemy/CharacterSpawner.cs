using UnityEngine;
using System.Collections;
/*Spawns player object if there is no object in the scene with the player tag
 */
public class CharacterSpawner : MonoBehaviour 
{
	public GameObject playerBody;//set in editor
	
	//Status Variables
	static public int level = 0;
	static public int lives = 3;
	static public int currentLevel = 0;
	static public int checkpoint = 0;
	static public int destination = 0;
	static public bool levelChange = false;
	
	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad(transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(levelChange == true)levelSetter();
		PlayerSpawn();
	}

	//Spawns player object and reduces lives count by 1.  If lives is already zero it instead loads the gameover scene.
	void PlayerSpawn()
	{
		Vector2 startPosition = new Vector2(checkpoint, 0);
		if(!GameObject.FindWithTag("Player"))
		{
			if(!levelChange) 
				lives--;

			if(lives >= 0)
				Instantiate(playerBody, startPosition, transform.rotation);
			else
			{
				//Application.LoadLevel("GameOver");
				HUD.gameOver = true;
			}
			
			levelChange = false;
		}
	}
	//if levelChange is true this function is activated to change the level
	void levelSetter()
	{	
		string nextLevel;
		switch(destination)
		{
		case 0:
			nextLevel = "IslandZone-Demo";
			break;
		case 1:
			nextLevel = "IslandZone-Demo_Cave";
			break;
		case 2:
			nextLevel = "Level 2";
			break;
		default:
			Debug.Log("error");
			nextLevel = "Level 1";
			break;
		}
		Debug.Log (nextLevel);
		Application.LoadLevel(nextLevel);
		levelChange = false;
		//PlayerSpawn();
	}
}
