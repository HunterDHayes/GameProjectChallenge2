using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayValues : MonoBehaviour {

	public Text Distance;
	public Text Deaths;
	public Text EnemiesKilled;

	// Use this for initialization
	void Start () 
	{
	 
	}
	
	// Update is called once per frame
	void Update () 
	{

		float distance = PlayerPrefs.GetFloat("TotalMeters");
		int distToDisplay = (int)distance;
		Distance.text = distToDisplay.ToString ();

		int defeated = PlayerPrefs.GetInt ("TotalKillCount");
		EnemiesKilled.text = defeated.ToString ();

		int deaths = PlayerPrefs.GetInt ("TotalDeaths");
		Deaths.text = deaths.ToString ();

	}
}
