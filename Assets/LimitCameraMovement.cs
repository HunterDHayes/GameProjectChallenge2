using UnityEngine;
using System.Collections;

public class LimitCameraMovement : MonoBehaviour 
{

	public float MaxHeight;
	public float MinHeight;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (transform.position.y > MaxHeight) {
			Vector3 pos = transform.position;
			pos.y = MaxHeight;
			transform.position = pos;
		} else if (transform.position.y < MinHeight) {
			Vector3 pos = transform.position;
			pos.y = MinHeight;
			transform.position = pos;
		}
	}
}
