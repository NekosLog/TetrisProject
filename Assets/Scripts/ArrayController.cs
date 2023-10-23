using UnityEngine;

public class ArrayController : MonoBehaviour
{
    private MinoData.MinoColor[,] _minoArray = new MinoData.MinoColor[20,10];

    private void DeleteLine(int Row)
    {
        for (int Column = 0; Column < _minoArray.GetLength(1); Column++)
        {
            _minoArray[Row, Column] = MinoData.MinoColor.empty;
        }
    }

    private void ClearArray()
    {
        for (int Row = 0; Row < _minoArray.GetLength(0); Row++)
        {
            DeleteLine(Row);
        }
    }

    private void SetMino(int[] position)
    {
        
    }
}
