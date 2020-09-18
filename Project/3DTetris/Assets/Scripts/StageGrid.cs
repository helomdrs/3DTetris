using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGrid : MonoBehaviour
{
    public static int gridWidth = 15;
    public static int gridHeigth = 10;

    public Transform topRight, topLeft, botRight, botLeft;

    void Start()
    {

    }

    void Update()
    {

    }

    public bool IsInsideGrid(Vector2 pos)
    { 
        if ((int)pos.x > topLeft.position.x && (int)pos.x < topRight.position.x && (int)pos.y >= botLeft.transform.position.y)
        {
            return true;
        }
        else
            return false;
    }

    public Vector2 Round(Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWidth, gridHeigth, 1));
    }
}
