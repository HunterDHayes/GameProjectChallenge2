using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlsController : MonoBehaviour {

	public Text m_Text;
	public float m_RenderTime;
	private float m_Timer;

	// Use this for initialization
	void Start () {
		m_Timer = m_RenderTime;

#if UNITY_ANDROID
		m_Text.text = "Tap     Left     side     for     Jump\nTap     Right     side     for     Attack";
#endif
	}
	
	// Update is called once per frame
	void Update () {
		m_Timer -= Time.deltaTime;

		if (m_Timer <= 0.0f || Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
			Destroy (gameObject);
	}
}
