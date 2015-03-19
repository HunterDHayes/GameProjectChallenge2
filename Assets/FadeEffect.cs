using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeEffect : MonoBehaviour {
	private float timer = 0.0f;
	public float fadeTimer = 0.0f;
	public bool fadeIn;
	public bool fadeOut;

	// Use this for initialization
	void Start () 
	{
		timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (fadeIn) 
		{
			timer += Time.deltaTime;
			float percentLife = timer/fadeTimer;
			Image image = GetComponent<Image>();

			Color color = image.color;

			color.a = 1.0f * percentLife;

			if(color.a > 1.0f)
				color.a = 1.0f;

			image.color = color;
		}
		else if (fadeOut) 
		{
			timer += Time.deltaTime;
			float percentLife = timer/fadeTimer;

			Image image = GetComponent<Image>();
			Color color = image.color;

			color.a = 1.0f - (1.0f * percentLife);
			if(color.a < 0)
				color.a = 0;

			image.color = color;
		}
	}

	public void FadeIn()
	{
		Image image = GetComponent<Image>();
		Color color = image.color;
		
		color.a = 0;
		
		image.color = color;

		fadeIn = true;
		timer = 0.0f;
	}
	public void FadeOut()
	{
		Image image = GetComponent<Image>();
		Color color = image.color;
		
		color.a = 1.0f;

		image.color = color;

		fadeOut = true;
		timer = 0.0f;
	}
}
