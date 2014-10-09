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

	private Texture2D[] heartArray = new Texture2D[11];
	
	static public bool gameOver = false;
	private bool isPaused = false;

	GameObject PlayerBody;
	GameObject CameraBody;

	//Starts the level running.
	void Start()
	{
		Time.timeScale = 1.0f;
		CameraBody  = GameObject.FindWithTag("MainCamera");
		ArrayOfHearts();
	}

	private void ArrayOfHearts()
	{
		heartArray[10] = heartFull;
		heartArray[9] = heartNineTenths;
		heartArray[8] = heartEightTenths;
		heartArray[7] = heartSevenTenths;
		heartArray[6] = heartSixTenths;
		heartArray[5] = heartFiveTenths;
		heartArray[4] = heartFourTenths;
		heartArray[3] = heartThreeTenths;
		heartArray[2] = heartTwoTenths;
		heartArray[1] = heartOneTenths;
		heartArray[0] = heartEmpty;
	}

	//This function is called every update.
	void Update ()
	{
		//If the Escape key is pressed, the game will either pause or resume based on the isPaused status.
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(isPaused) resumeGame();
			else pauseGame();
			if(gameOver) EndGame();
		}
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
	
	private void EndGame()
	{
		gameOver = false;
		Application.LoadLevel (0);
	}

	//This function is called every update and deals with anything regarding the Graphical User Interface.
	private void OnGUI()
	{
		PlayerBody = GameObject.FindWithTag("Player");

		Rect r = new Rect(0,0,Screen.width, Screen.height);
		GUILayout.BeginArea(r);

		//This statement checks to see if the game is paused. If so, it will display the menu.
		if (isPaused)
		{
			//Menu GUI. Displays only if the game is paused.
			//Background of menu. A nice mix of GUI and GUILayout. 
			//Add about 20 to the height of the Box and Begin Area for each button added.
			GUI.Box(new Rect(Screen.width/2-60, Screen.height/3, 120, 90), "Menu");
			GUILayout.BeginArea(new Rect(Screen.width/2-50, Screen.height/3+20, 120, 90));


			//Menu Button 1, Starts or Pauses the game.
			if (GUILayout.Button ("Continue", GUILayout.Width (100), GUILayout.Height(20)))
			{
				//This will resume the game.
				resumeGame ();
			}

			if (GUILayout.Button ("Options", GUILayout.Width (100), GUILayout.Height(20)))
			{
				//Does not do anything right now.
			}

			if (GUILayout.Button ("End Level", GUILayout.Width (100), GUILayout.Height(20)))
			{
				//BUG: Some entities are not destroyed like they should be (music, old camera...).
				//This will end the level and return to the start screen.
				gameOver = true;
				resumeGame ();
				EndGame();
			}

			GUILayout.EndArea();
		}

		//Player Information GUI
		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();
		GUILayout.Label ("Level " + CharacterSpawner.level);
		GUILayout.Label ("Lives " + CharacterSpawner.lives);
		GUILayout.EndVertical();
		GUILayout.BeginVertical();
		for(i=0;i<PlayerBody.GetComponent<Player>().health;i++) GUI.Box(new Rect (50+i*11,10,2,10), "");

		//Player health
		GUI.Label (new Rect (200, 10, 150, 50), heartArray[i]);

		for(i=0;i<PlayerBody.GetComponent<Player>().energy;i++) GUI.Box(new Rect (50+i*11,25,2,10), "");
		GUILayout.Label ("Health " + PlayerBody.GetComponent<Entity>().health);
		GUILayout.Label ("Energy " + PlayerBody.GetComponent<Entity>().energy);
		GUILayout.EndVertical();
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		
		//Pause GUI
		/*
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
		*/
		GUILayout.EndArea();
	}
}
