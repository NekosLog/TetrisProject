using UnityEngine;

public class ArrayController : MonoBehaviour
{
    private MinoData.E_MinoColor[,] _minoArray = new MinoData.E_MinoColor[20,10];

    private void DeleteLine(int Row)
    {
        for (int Column = 0; Column < _minoArray.GetLength(1); Column++)
        {
            _minoArray[Row, Column] = MinoData.E_MinoColor.empty;
        }
    }

    private void ClearArray()
    {
        for (int Row = 0; Row < _minoArray.GetLength(0); Row++)
        {
            DeleteLine(Row);
        }
    }

    private void SetMino(MinoData.minoState minoState)
    {
        _minoArray[minoState.Row, minoState.Column] = minoState.minoColor;
    }
}
