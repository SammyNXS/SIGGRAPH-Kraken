using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        // Make a background box
        GUI.Box(new Rect(10, 10, 130, 90), "Loader Menu");

        // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
        if (GUI.Button(new Rect(20, 40, 110, 20), "Turret Placement"))
        {
            Application.LoadLevel("Test");
        }

        // Make the second button.
        if (GUI.Button(new Rect(20, 70, 110, 20), "Just a Button"))
        {
            //Application.LoadLevel(2);
        }
    }
}
