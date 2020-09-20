using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //Referência dos objetos de texto da HUD
    public Text scoreText, highScoreText;

    //Quantidade de pontos por linha feita
    [SerializeField] int pointsPerRow;
    int score, highscore;
    
    void Start()
    {
        //No começo atualiza os valores de score e highscore
        UpdateScoreText();
        UpdateHighScoreText();
    }

    //Atualiza o texto do score para o valor atual
    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    //Atualiza o texto de highscore para o valor atual
    void UpdateHighScoreText()
    {
        highScoreText.text = PlayerPrefs.GetInt("highscore").ToString();
    }

    //Adiciona uma quantidade pré-determinada de pontos para o score
    public void AddScore()
    {
        score += pointsPerRow;
        //Checa se é um novo highscore e atualiza o texto de score
        CheckHighScore();
        UpdateScoreText();
    }

    //Checa se a nova pontuação é um highscore
    void CheckHighScore()
    {
        //Usando PlayerPrefs, verifica se a key existe
        if (PlayerPrefs.HasKey("highscore"))
        {
            //Se sim, vê se seu valor é menor que o score atual
            if(PlayerPrefs.GetInt("highscore") < score)
            {
                //Se sim, atualiza seu valor para o score atual e chama a atualização de texto
                PlayerPrefs.SetInt("highscore", score);
                UpdateHighScoreText();
            }
        } else
        {
            //Se não, cria a key de Highscore e inicializa seu valor
            PlayerPrefs.SetInt("highscore", score);
            UpdateHighScoreText();
        }
    }
}
