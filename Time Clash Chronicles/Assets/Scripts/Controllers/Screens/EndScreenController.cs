using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndScreenController : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button menuButton;

    AudioManager audioManager;

    // Add listener to buttons and activate music
    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        playButton.onClick.AddListener(PlayGame);
        menuButton.onClick.AddListener(BacktoMenu);

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "VictoryScreen")
        {
            audioManager.Play("Victory");
        }
        else
        {
            audioManager.Play("Defeat");
        }


    }

    void PlayGame()
    {
        SceneManager.LoadScene("DeckSelect");
    }

    void BacktoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}