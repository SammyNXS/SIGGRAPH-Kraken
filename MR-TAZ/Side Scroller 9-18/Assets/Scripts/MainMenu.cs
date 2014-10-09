using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnGUI()
	{
		//Background of menu. A nice mix of GUI and GUILayout. 
		//Add about 20 to the height of the Box and Begin Area for each button added.
		GUI.Box(new Rect(Screen.width/2-60, Screen.height*2/3, 120, 90), "Main Menu");
		GUILayout.BeginArea(new Rect(Screen.width/2-50, Screen.height*2/3+20, 120, 90));
		
		//Menu Button 1, Starts or Pauses the game.
		if (GUILayout.Button ("Start", GUILayout.Width (100), GUILayout.Height(20)))
		{
			//This will start the first level.
			Application.LoadLevel(1);
		}

		if (GUILayout.Button ("Options", GUILayout.Width (100), GUILayout.Height(20)))
		{
			//Does not do anything right now.
		}
		
		if (GUILayout.Button ("Quit Game", GUILayout.Width (100), GUILayout.Height(20)))
		{
			//This will close the application. Only works with the executable, NOT in Unity.
			Application.Quit();
		}
		
		GUILayout.EndArea();
	}
}
