using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour 
{
	int i;
	int j;
	public Texture2D heartFull;
	public Texture2D heartNineTenths;
	public Texture2D heartEightTenths;
	public Texture2D heartSevenTenths;
	public Texture2D heartSixTenths;
	public Texture2D heartFiveTenths;
	public Texture2D heartFourTenths;
	public Texture2D heartThreeTenths;
	public Texture2D heartTwoTenths;
	public Texture2D heartOneTenths;
	public Texture2D heartEmpty;
	
	static public bool gameOver = false;
	bool isPaused = false;

	//Starts the game paused.
	void Start()
	{
		Time.timeScale = 1.0f;
	}

	//This function is called every update.
	void Update ()
	{
		//If the Escape key is pressed, the game will either pause or resume based on the isPaused status.
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(isPaused) resumeGame();
			else pauseGame();
		}
	}

	public bool getPauseState()
	{
		return isPaused;
	}

	//This resumes the game when called.
	private void resumeGame()
	{
		Time.timeScale = 1.0f;
		isPaused = false;
	}

	//This pauses the game when called.
	private void pauseGame()
	{
		Time.timeScale = 0.0f;
		isPaused = true;
	}
	
	//This function is called every update and deals with anything regarding the Graphical User Interface.
	private void OnGUI()
	{

		Rect r = new Rect(0,0,Screen.width, Screen.height);
		GUILayout.BeginArea(r);

		//This statement checks to see if the game is paused. If so, it will display the menu.
		if (isPaused)
		{
			//Menu GUI. Displays only if the game is paused.
			//Background of menu.
			GUI.Box (new Rect (440, 250, 120, 60), new GUIContent ("Menu"));
			//Menu Button 1, Starts or Pauses the game.
			if (GUI.Button (new Rect (450, 270, 100, 20), new GUIContent ("Start")))
			{
				//This will resume the game.
				resumeGame ();
			}
		}

		//Character Information GUI
		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();
		//GUILayout.Label ("Level " + CharacterSpawner.level);
		//GUILayout.Label ("Lives " + CharacterSpawner.lives);
		GUILayout.EndVertical();
		GUILayout.BeginVertical();
		for(i=0;i<this.GetComponentInParent<Character>().health;i++) GUI.Box(new Rect (50+i*11,10,2,10), "");

		//Character health
		if (i == 10)
		{
			GUI.Label (new Rect (200, 10, 150, 50), heartFull);
		}
		else if (i == 9)
		{
			GUI.Label (new Rect (200, 10, 150, 50), heartNineTenths);
		}
		else if (i == 8)
		{
			GUI.Label (new Rect (200, 10, 150, 50), heartEightTenths);
		}
		else if (i == 7)
		{
			GUI.Label (new Rect (200, 10, 150, 50), heartSevenTenths);
		}
		else if (i == 6)
		{
			GUI.Label (new Rect (200, 10, 150, 50), heartSixTenths);
		}
		else if (i == 5)
		{
			GUI.Label (new Rect (200, 10, 150, 50), heartFiveTenths);
		}
		else if (i == 4)
		{
			GUI.Label (new Rect (200, 10, 150, 50), heartFourTenths);
		}
		else if (i == 3)
		{
			GUI.Label (new Rect (200, 10, 150, 50), heartThreeTenths);
		}
		else if (i == 2)
		{
			GUI.Label (new Rect (200, 10, 150, 50), heartTwoTenths);
		}
		else if (i == 1)
		{
			GUI.Label (new Rect (200, 10, 150, 50), heartOneTenths);
		}
		else if (i == 0)
		{
			GUI.Label (new Rect (200, 10, 150, 50), heartEmpty);
		}

		for(i=0;i<this.GetComponentInParent<Character>().energy;i++) GUI.Box(new Rect (50+i*11,25,2,10), "");
		GUILayout.Label ("Health " + this.GetComponentInParent<Character>().health);
		GUILayout.Label ("Energy " + this.GetComponentInParent<Character>().energy);
		GUILayout.EndVertical();
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		
		//Pause GUI
		if(isPaused)
		{
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();
			GUILayout.Label ("Paused");
			GUILayout.FlexibleSpace();
			GUILayout.EndVertical();
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
		}
		
		//Gameover GUI
		if(gameOver)
		{
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();
			GUILayout.Label ("GAME OVER");
			GUILayout.FlexibleSpace();
			GUILayout.EndVertical();
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
		}
		
		GUILayout.EndArea();
	}
}
