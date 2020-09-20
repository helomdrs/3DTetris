using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Objeto de texto de pausar/despausar o jogo
    public Text pauseTxt;

    public GameObject gameOverMessage;

    void Start()
    {
        InitializeGame();
    }

    void Update()
    {
        
    }

    void InitializeGame()
    {
        //Certifica-se de que o jogo esteja rodando no início da cena
        Time.timeScale = 1f;

        //Inicializa a HUD da forma adequada
        gameOverMessage.SetActive(false);
        pauseTxt.gameObject.SetActive(true);
    }

    //Faz a variação entre pausar e despausar o jogo
    public void TogglePauseGame()
    {
        if (Time.timeScale == 1f)
        {
            pauseTxt.text = "unpause";
            Time.timeScale = 0f;
        } else
        {
            pauseTxt.text = "pause";
            Time.timeScale = 1f;
        }
    }

    //Comportamento da HUD durante o game over
    public void GameOver()
    {
        gameOverMessage.SetActive(true);
        pauseTxt.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    //Restarta o jogo carregando a cena de game
    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    //Sai para o Menu Inicial
    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        //SceneManager.LoadScene("MainMenu");
    }
}
