using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject PauseUI;

    private bool paused = false;

	// Use this for initialization
	void Start () {

        PauseUI.SetActive(false);
   
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
        }

        if (paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }

        if (!paused)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1;
        }
	}

    public void Resume()
    {
        paused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void MainMenu()
    {

        // 他のシーンに遷移する（File -> BuildSetting のシーンの番号で設定できるっぽい？）
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
