using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject[] m_CanvasList;
    private string m_sCurrentCanvas;

    public Text m_Highscore, m_LongestPlayTime, m_AvgGrade;
	public Sprite[] CharacterChoices;
	public int CharacterChoice;
	public Image PlayerChoice;


    void Start()
    {
 //      if (!GlobalData.GetGlobalData().RenderedSplashScreens)
 //          ActivateCanvas("Splash Screen");
 //      else
 //          ActivateCanvas("Main Menu");

        GameObject soundManager = GameObject.FindGameObjectWithTag("SoundManager");

        if (soundManager) {
			soundManager.SendMessage ("StopAllMusic");
			soundManager.SendMessage ("PlayMusic", "MainMenu");
		}

		PlayerPrefs.SetInt ("PlayerChoice", 0);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            switch (m_sCurrentCanvas)
            {
                default:
                case "Main Menu":
                    ExitGame();
                    break;
                case "Stats":
                case "Credits":
                    ActivateCanvas("Main Menu");
                    break;
            }
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

    public void ActivateCanvas(string name)
    {
        for (int i = 0; i < m_CanvasList.Length; i++)
        {
            m_CanvasList[i].SetActive(false);

            if (m_CanvasList[i].name == name)
            {
                m_CanvasList[i].SetActive(true);
                m_sCurrentCanvas = name;
            }
        }
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

    public void ResetStats()
    {
        PlayerPrefs.SetInt("Highscore", 0);
        PlayerPrefs.SetInt("LongestPlayTime", 0);
		PlayerPrefs.SetInt("TotalGrade", 0);
		PlayerPrefs.SetInt("TotalPlaythroughs", 0);

		m_Highscore.text = "0";
        m_LongestPlayTime.text = "0";
		m_AvgGrade.text = "0";
    }

	//When called, moves the character selection one step forward through the array, with loop around.

}
