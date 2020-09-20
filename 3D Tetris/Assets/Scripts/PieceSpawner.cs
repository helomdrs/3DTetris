using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    //Referências das peças existentes e do GridManager
    [SerializeField] GameObject[] pieces;
    [SerializeField] GridManager grid;
    
    //Referência do ponto de criação das peças
    public Transform spawnPoint;

    Piece currentPiece = null;

    void Start()
    {
        //Começa a partida criando uma peça
        SpawnPiece();
    }

    void Update()
    {
        
    }

    //Seleciona uma peça aleatória conforme o array de peças disponíveis
    GameObject GetRandomPiece(GameObject[] _pieces)
    {
        if (_pieces.Length > 0)
        {
            int pieceIndex = Random.Range(0, _pieces.Length);
            return _pieces[pieceIndex];
        }
        else
            return null;
    }

    //Cria uma peça
    public void SpawnPiece()
    {
        GameObject newPiece = GetRandomPiece(pieces);
        Vector2 newPos = RoundVector.RoundedVector(spawnPoint.position);
        Piece piece = Instantiate(newPiece, newPos, Quaternion.identity).GetComponent<Piece>();
        piece.Setup(grid);
        currentPiece = piece;
    }
}
