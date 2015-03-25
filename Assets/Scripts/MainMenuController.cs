using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    void Start()
    {
        GameObject soundManager = GameObject.FindGameObjectWithTag("SoundManager");

        if (soundManager) {
			soundManager.SendMessage ("StopAllMusic");
			soundManager.SendMessage ("PlayMusic", "MainMenu");
		}

		PlayerPrefs.SetInt ("PlayerChoice", 0);
    }

    void Update()
    {
        if (Input.GetKeyUp (KeyCode.Escape)) {
			ExitGame ();
		}
    }

    public void ChangeScene(string name)
    {
        Application.LoadLevel(name);
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlayMusic(string name)
    {
        GameObject soundManager = GameObject.FindGameObjectWithTag("SoundManager");

        if (soundManager)
            soundManager.SendMessage("PlayMusic", name);
    }

    public void PlaySFX(string name)
    {
        GameObject soundManager = GameObject.FindGameObjectWithTag("SoundManager");

        if (soundManager)
            soundManager.SendMessage("PlaySfx", name);
    }

    public void StopAllMusic()
    {
        GameObject soundManager = GameObject.FindGameObjectWithTag("SoundManager");

        if (soundManager)
            soundManager.SendMessage("StopAllMusic");
    }

    public void StopAllSfx()
    {
        GameObject soundManager = GameObject.FindGameObjectWithTag("SoundManager");

        if (soundManager)
            soundManager.SendMessage("StopAllSfx");
    }

    public void MuteAllMusic()
    {
        GameObject soundManager = GameObject.FindGameObjectWithTag("SoundManager");

        if (soundManager)
            soundManager.SendMessage("MuteAllMusic");
    }

    public void MuteAllSfx()
    {
        GameObject soundManager = GameObject.FindGameObjectWithTag("SoundManager");

        if (soundManager)
            soundManager.SendMessage("MuteAllSfx");
    }

    public void UnmuteAllMusic()
    {
        GameObject soundManager = GameObject.FindGameObjectWithTag("SoundManager");

        if (soundManager)
            soundManager.SendMessage("UnmuteAllMusic");
    }

    public void UnmuteAllSfx()
    {
        GameObject soundManager = GameObject.FindGameObjectWithTag("SoundManager");

        if (soundManager)
            soundManager.SendMessage("UnmuteAllSfx");
    }
}