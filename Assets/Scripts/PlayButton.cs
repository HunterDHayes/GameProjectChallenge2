using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {

	public string levelToLoad;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if (Input.GetKeyUp (KeyCode.Escape)) {
			Application.LoadLevel("MainMenu");
		}
	}
	
	public void SwitchToGameplay()
	{
		if(levelToLoad == "Exit")
		{
			Application.Quit();
			return;
		}
	
		Application.LoadLevel(levelToLoad)	;
	}
}
