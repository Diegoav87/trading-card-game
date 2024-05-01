using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{

    [SerializeField] Button playButton;

    AudioManager audioManager;

    void Awake()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        audioManager.Play("Menu");
    }

    void PlayGame()
    {
        SceneManager.LoadScene("DeckSelect");
    }
}
