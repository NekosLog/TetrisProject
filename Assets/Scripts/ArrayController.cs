using System;
using System.Collections.Generic;
using UnityEngine;

public class ArrayController : MonoBehaviour, IFLandingMinos, IFGetMinoArray
{
    #region ïœêî

    private MinoData.E_MinoColor[,] _minoArray = new MinoData.E_MinoColor[20, 10];
    private List<int> DeleteLineList;
    private bool _doesDelete = false;

    #endregion


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

    private void CheckLine(int Row)
    {
        for (int Column = 0; Column < _minoArray.GetLength(1); Column++)
        {
            if(_minoArray[Row,Column] == MinoData.E_MinoColor.empty)
            {
                return;
            }
            DeleteLineList.Add(Row);
            _doesDelete = true;
        }
    }

    public void LandingMinos(List<MinoData.minoState> dropingMinos)
    {
        for(int minoNumber = 0;minoNumber < dropingMinos.Count; minoNumber++)
        {
            SetMino(dropingMinos[minoNumber]);
        }
    }
}
