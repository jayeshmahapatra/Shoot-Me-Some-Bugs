using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{   
    private AudioSource gameOverAS;
    private GameManager gameManager;
    private void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameOverAS = GetComponent<AudioSource>();
        gameOverAS.ignoreListenerPause = true;
        gameOverAS.Play();
    }

    public void RestartGame()
    {
        gameManager.RestartGame();
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
