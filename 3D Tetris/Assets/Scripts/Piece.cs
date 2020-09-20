using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    //Velocidade de queda da peça
    [SerializeField] float fallVelocity = 1f;
    float fallCounter;

    //Blocos que compõem a peça
    public GameObject[] blocks;
    GridManager grid;
    bool isActive = true;

    //Faz a referência do GridManager
    public void Setup(GridManager _grid)
    {
        grid = _grid;
    }

    void Start()
    {
        fallCounter = fallVelocity;
        //Faz a referência dos blocos que compõem a peça
        FillBlocks();
    }

    //Fomenta o vetor com todos os objetos que sejam filhos deste
    void FillBlocks()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            blocks[i] = transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        if(gameObject != null)
        {
            if (isActive)
            {
                //Movimentação padrão da peça
                fallCounter -= Time.deltaTime;
                if (fallCounter <= 0f)
                {
                    MovePiece(Vector2.down);
                    if (!IsValidPosition())
                    {
                        MovePiece(Vector2.up);
                        //Se não está mais numa posição válida, aterrissa a peça no grid
                        grid.LandPiece(this);

                        //Desativa para que não possa ser mais manipulada
                        isActive = false;
                    }

                    fallCounter = fallVelocity;
                }

                //Checa os inputs de movimentação do usuário
                CheckPlayerInput();
            } 
        }
    }

    //Checa e valida os inputs do usuário
    void CheckPlayerInput()
    {
        //Esquerda
        if (Input.GetKeyDown(KeyCode.A))
        {
            MovePiece(Vector2.left);

            if (!IsValidPosition())
            {
                MovePiece(Vector2.right);
            }
        }

        //Direita
        if (Input.GetKeyDown(KeyCode.D))
        {
            MovePiece(Vector2.right);

            if (!IsValidPosition())
            {
                MovePiece(Vector2.left);
            }
        }

        //Para baixo
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (IsValidPosition())
            {
                MovePiece(Vector2.down);
            }
            else
            {
                MovePiece(Vector2.up);
                //Verifca se a peça aterrissou
                grid.LandPiece(this);
                isActive = false;
            }
        }

        //Rotação da peça
        if (Input.GetKeyDown(KeyCode.W))
        {
            RotatePiece(90);
        }
    }

    //Verifica se a posição da peça é valida por estar dentro do grid e em um espaço vazio
    bool IsValidPosition()
    {
        //Faz a verificação em cada bloco que compõe a peça
        for (int i = 0; i < blocks.Length; i++)
        {
            Vector2 blockPos = new Vector2(blocks[i].transform.position.x, blocks[i].transform.position.y);
            blockPos = RoundVector.RoundedVector(blockPos);

            if (!grid.IsInsideAndEmpty(blockPos))
            {
                return false;
            }
        }

        return true;
    }

    //Move a peça inteira na direção passada por parâmetro
    void MovePiece(Vector2 direction)
    {
        transform.position += new Vector3(direction.x, direction.y, 0f);
        RoundPosition();
    }

    //Arredonda a posição da peça para valores inteiros
    void RoundPosition()
    {
        Vector2 newPos = RoundVector.RoundedVector(new Vector2(transform.position.x, transform.position.y));
        transform.position = new Vector3(newPos.x, newPos.y, 0f);
    }

    //Faz a rotação da peça inteira conforme valor passado por parâmetro
    void RotatePiece(int rotateAmount)
    {
        transform.Rotate(0f, 0f, rotateAmount);
        RotateChildren(-rotateAmount);

        if (!IsValidPosition())
        {
            transform.Rotate(0f, 0f, -rotateAmount);
            RotateChildren(rotateAmount);
        }

        RoundPosition();
    }

    //Rotaciona também todos os blocos que compõem a peça
    void RotateChildren (int amount)
    {
        for(int i = 0; i < blocks.Length; i++)
        {
            blocks[i].transform.Rotate(0f, 0f, amount);
        }
    }
}
