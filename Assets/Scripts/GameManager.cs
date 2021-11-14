using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{   
    [Header("Score Attributes")]
    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;

    [Header("Counting Attributes")]
    public int soldierCount = 0;
    public int beetleCount = 0;
    public int healthPickupCount = 0;
    public int armourPickupCount = 0;

    [Header("Menu Attributes")]
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject gameOverText;
    public AudioClip victoryClip;
    public GameObject HUD;
    public static bool isGamePaused;

    [Header("Victory Condition")]
    public int maxWaveNumber = 10;
    public GameObject victoryEffect;

    private float gameOverDelay = 0.25f; 
    private SpawnManager spawnManager;

    [Header("Restart and Tutorial")]
    private int isRestarted; // 0 if no, 1 if yes
    private bool displayTutorial;
    private int tutorialIndex = 0;
    public GameObject[] tutorialPanels = new GameObject[3];
    
    void Awake()
    {   
        isRestarted = PlayerPrefs.GetInt("isRestarted", 0);
        
        if(isRestarted == 0)
        {
            //Display Tutorial
            tutorialIndex = 0;
            isGamePaused = true;
            displayTutorial = true;
            Tutorial();

        }
        else
        {
        //Unpause
        HUD.SetActive(true);
        Time.timeScale = 1;
        AudioListener.pause = false;
        isGamePaused = false;
        }
        

        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        //SetCursor();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGamePaused = !isGamePaused;
            PauseGame();
        }

        if(displayTutorial)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                tutorialIndex++;
                Tutorial();

            }
        }
    }

    public void UpdateScore(int scoreToAdd)
    {   
        score += scoreToAdd;
        scoreText.text = "Score : " + score;
    }

    public int getSoldierCount()
    {
        return soldierCount;
    }

    public void UpdateSoldierCount(int newSoldierCount)
    {
        soldierCount = newSoldierCount;
    }
    
    public int getBeetleCount()
    {
        return beetleCount;
    }
    public void UpdateBeetleCount(int newBeetleCount)
    {
        beetleCount = newBeetleCount;
    }

    public void UpdateHealthPickupCount(int newHealthPickupCount)
    {
        healthPickupCount = newHealthPickupCount;
    }

    public void UpdateArmourPickupCount(int newArmourPickupCount)
    {
        armourPickupCount = newArmourPickupCount;
    }

    public void PauseGame ()
    {
        if(isGamePaused)
        {
            Time.timeScale = 0f;
            AudioListener.pause = true;
            pauseMenu.SetActive(true);
        }
        else 
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
            pauseMenu.SetActive(false);
        }
    }

    private void Tutorial()
    {   
        for(int i = 0; i < tutorialPanels.Length; i++)
        {
            if(i == tutorialIndex)
            {
                //Show Tutorial
                tutorialPanels[i].SetActive(true);
            }
            else
            {
                tutorialPanels[i].SetActive(false);
            }
        }

        //If tutorialIndex is more than the length of tutorialPanels, tutorial is over
        if(tutorialIndex >= tutorialPanels.Length)
        {
            //UnPause the game
            displayTutorial = false;
            HUD.SetActive(true);
            Time.timeScale = 1;
            AudioListener.pause = false;
            isGamePaused = false;

        }
    }

    public void GameOver(bool won = false)
    {   //Disable the Player HUD
        HUD.SetActive(false);
        //If Player Won change the Game Over Audio and Text to Victory
        if(won)
        {   for(int i = 0; i < 3; i++)
            spawnManager.SpawnObjectRandomly(victoryEffect);
            gameOverText.GetComponent<TextMeshProUGUI>().text = "You Won!!";
            gameOverMenu.GetComponent<AudioSource>().clip = victoryClip;
        }
        StartCoroutine(DelayedGameOver(gameOverDelay));
    }

    public void RestartGame()
    {   
        PlayerPrefs.SetInt("isRestarted",1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator DelayedGameOver(float gameOverDelay)
    {   
        //Slow down time by slowDownFactor
        isGamePaused = true;
        yield return new WaitForSecondsRealtime(gameOverDelay);
        Time.timeScale = 0f;
        AudioListener.pause = true;
        gameOverMenu.SetActive(true);
        finalScoreText.text = "Score : " + score;
    }
}