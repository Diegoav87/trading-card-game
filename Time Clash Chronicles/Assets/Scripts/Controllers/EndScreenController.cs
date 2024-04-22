using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndScreenController : MonoBehaviour
{
    // Se crean dos objetos para cada botón, uno para volver al menu y otro para volver a jugar
    [SerializeField] Button playButton;
    [SerializeField] Button menuButton;

    void Start()
    {
        // Se empiezan dos listener para esperar el click del botón
        playButton.onClick.AddListener(PlayGame);
        menuButton.onClick.AddListener(BacktoMenu);
    }

    // Se crea la función que cambia la escena a Deck Select
    void PlayGame()
    {
        SceneManager.LoadScene("DeckSelect");
    }
    
    // Se crea la función que cambia la escena al Menú
    void BacktoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}