using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    public float addFallSpeed = 0f;
    public float fallSpeed = 1f;

    StageGrid grid;
    bool isValidPosition;

    void Start()
    {
        grid = GameObject.FindGameObjectWithTag("StageGrid").GetComponent<StageGrid>();
    }

    void Update()
    {
        if (IsValidPosition())
            transform.position += new Vector3(0, -fallSpeed, 0);

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (IsValidPosition())
                transform.position += new Vector3(1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (IsValidPosition())
                transform.position += new Vector3(-1, 0, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (IsValidPosition())
                transform.position += new Vector3(0, -(fallSpeed + addFallSpeed), 0);
        }
        //Piece Rotation
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (IsValidPosition())
                transform.Rotate(0, 0, 90);
        }
    }

    bool IsValidPosition()
    {
        Vector2 pos = grid.Round(transform.position);

        if (grid.IsInsideGrid(pos))
        {
            return true;
        }
        else
            return false;
    }
}