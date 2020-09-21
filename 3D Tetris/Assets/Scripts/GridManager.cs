using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //Dimensões da Grid
    [SerializeField] int width = 12;
    [SerializeField] int height = 20;
    [SerializeField] GameObject[,] grid;
    [SerializeField] PieceSpawner pieceSpawner;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] GameManager gameManger;
    [SerializeField] SoundsManager soundsManager;

    void Start()
    {
        //Inicia a matriz da grid conforme as dimensões passadas
        grid = new GameObject[width, height];
    }

    //GERENCIAMENTO DAS PEÇAS

    //Verifica se os blocos que compõem a peça estão dentro da grid
    bool IsInsideGrid(Vector2 blocksPosition)
    {
        if(blocksPosition.x >= 0 && blocksPosition.x < width)
        {
            if(blocksPosition.y >= 0 && blocksPosition.y < height)
            {
                return true;
            }
        }
        return false;
    }

    //Verifica se o quadrante da grid está vazio
    bool IsSpotEmpty(Vector2 blocksPosition)
    {
        if (grid[(int)blocksPosition.x, (int)blocksPosition.y] == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Verifica se os blocos estão dentro da grid e em um lugar vazio
    public bool IsInsideAndEmpty(Vector2 blocksPoisiton)
    {
        if (IsInsideGrid(blocksPoisiton) && IsSpotEmpty(blocksPoisiton))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Aterrisa a peça 
    public void LandPiece(Piece piece)
    {
        soundsManager.PlayLandPiece();
        
        //Guarda definitivamente cada bloco da peça no grid
        for(int i = 0; i < piece.blocks.Length; i++)
        {
            StoreBlockInGrid(piece.blocks[i]);
        }

        //Faz a verificação se há linhas completas para serem deletadas
        DeleteFullRows();

        //Chama a próxima peça
        if (pieceSpawner)
        {
            pieceSpawner.SpawnPiece();
        }

        //Verifica se houve o GameOver ao aterrisar uma peça
        CheckGameOver();
    }

    //Guarda os blocos da peça no grid
    void StoreBlockInGrid(GameObject block)
    {
        Vector2 intPos = RoundVector.RoundedVector(new Vector2(block.transform.position.x, block.transform.position.y));
        grid[(int)intPos.x, (int)intPos.y] = block;
    }

    //GERENCIAMENTO DAS LINHAS DE PEÇAS

    //Veficia se a linha está cheia de blocos
    bool IsRowFull(int row)
    {
        //Percorre todos os quadrantes dada a largura da grid
        for(int i = 0; i < width; i++)
        {
            if (!grid[i, row])
            {
                return false;
            }
        }
        return true;
    }

    //Destrói os objetos daquela linha
    void DeleteRow(int row)
    {
        for(int i = 0; i < width; i++)
        {
            Destroy(grid[i, row].gameObject);
            grid[i, row] = null;
        }
    }

    //Chama a destruição de mais de uma linha de blocos
    void DeleteFullRows()
    {
        //Desta vez percorre os quadrantes dada a altura do grid
        for(int i = 0; i < height; i++)
        {
            //Verifica se aquela linha está cheia
            if (IsRowFull(i))
            {
                DeleteRow(i);
                DecreaseRowsAbove(i + 1);
                i--;

                //Adiciona o score a cada linha pronta
                scoreManager.AddScore();
            }
        }
    }

    //Tira a linha da matriz da grid
    void DecreaseRow(int row)
    {
        for(int i = 0; i < width; i++)
        {
            if(grid[i, row])
            {
                //Pega as peças desta fileira e coloca no espaço debaixo
                grid[i, row - 1] = grid[i, row];
                //Esvazia o espaço anterior
                grid[i, row] = null;
                //Move as peças de lugar
                grid[i, row - 1].gameObject.transform.position += Vector3.down;
            }
        }
    }

    //Desce todas as linhas acima
    void DecreaseRowsAbove(int row)
    {
        for(int i = row; i < height; i++)
        {
            DecreaseRow(i);
        }
    }

    //Verifica se houve o GameOver
    void CheckGameOver()
    {
        for(int i = 0; i < width; i++)
        {
            //Se há blocos na última fileira, chama o GameOver
            if(grid[i, 16] != null)
            {
                gameManger.GameOver();
            }
        }
    }
}
