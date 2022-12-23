using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool gameplay;
    public GameObject pauseUI, victoryUI, defeatUI;
    public TextMeshProUGUI lapText;
    int lap;
    private void Awake()
    {
        Time.timeScale = 1;

        instance = this;

        Application.targetFrameRate = 60;



    }

    private void Start()
    {
        if (gameplay)
        {
            Pause();
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P) || Input.GetKeyUp(KeyCode.Escape))
        {
            Pause();
        }
    }

    bool pause;
    public void Pause()
    {
        if (!pause)
        {
            pause = true;
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        else if (pause)
        {
            pause = false;
            pauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Mainmenu()
    {
        SceneManager.LoadScene("Mainmenu");
    }

    public void SelectMap()
    {
        SceneManager.LoadScene("SelectMap");
    }

    public void Map1()
    {
        SceneManager.LoadScene("Map1");
    }

    public void Map2()
    {
        SceneManager.LoadScene("Map2");
    }
    public void Defeat()
    {
        defeatUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void Victory()
    {
        victoryUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void Finis()
    {
        lap++;
        lapText.text = "Lap " + lap + "/3";
        if (lap == 3)
        {
            Victory();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
