using UnityEngine;

public static class RoundVector
{
    //Classe estática usada para sempre arredondar as posições das peças para valores inteiros, facilitando assim a programação e posicionamento
    public static Vector2 RoundedVector(Vector2 input)
    {
        return new Vector2(Mathf.Round(input.x), Mathf.Round(input.y));
    }
}
