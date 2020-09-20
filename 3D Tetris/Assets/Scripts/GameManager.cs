using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Objeto de texto de pausar/despausar o jogo
    public Text pauseTxt;

    void Start()
    {
        //Certifica-se de que o jogo esteja rodando no início da cena
        Time.timeScale = 1f;
    }

    void Update()
    {
        
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

    //Sai para o Menu Inicial
    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        //SceneManager.LoadScene("MainMenu");
    }
}
