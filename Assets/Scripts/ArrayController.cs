using System;
using System.Collections.Generic;
using UnityEngine;

public class ArrayController : MonoBehaviour, IFLandingMinos, IFGetMinoArray
{
    #region ïœêî

    private MinoData.E_MinoColor[,] _minoArray = new MinoData.E_MinoColor[20, 10];
    private List<int> DropedRowList;
    private List<int> DeleteLineList;
    private bool _doesDelete = false;
    private int _deleteLineCounter = default;
    private int[] _fallValueArray = new int[19];

    #endregion


    private void DeleteLine(int row)
    {
        for (int column = 0; column < _minoArray.GetLength(1); column++)
        {
            _minoArray[row, column] = MinoData.E_MinoColor.empty;
        }
        for (int addPoint = row; addPoint < _minoArray.GetLength(0); addPoint++)
        {
            _fallValueArray[addPoint] += 1;
        }
        _deleteLineCounter++;
    }

    private void FallArray()
    {
        int row = 1;
        foreach (int fallValue in _fallValueArray)
        {

        }
    }

    private void FallLine(int row, int fallValue)
    {
        int fallPosition = row - fallValue;
        for(int column = 0; column < _minoArray.GetLength(1); column++)
        {
            // Ç≈Ç´ÇÍÇŒemptyîªíËÇµÇΩÇ¢
            // óéÇ∆Ç∑
            // è¡Ç∑
        }
    }

    private void ClearArray()
    {
        for (int row = 0; row < _minoArray.GetLength(0); row++)
        {
            DeleteLine(row);
        }
    }

    private void SetMino(MinoData.minoState minoState)
    {
        _minoArray[minoState.Row, minoState.Column] = minoState.minoColor;
    }
    private void CheckLine(int row)
    {
        for (int Column = 0; Column < _minoArray.GetLength(1); Column++)
        {
            if(_minoArray[row,Column] == MinoData.E_MinoColor.empty)
            {
                return;
            }
            DeleteLineList.Add(row);
            _doesDelete = true;
        }
    }

    public void LandingMinos(List<MinoData.minoState> dropingMinos)
    {
        for(int minoNumber = 0;minoNumber < dropingMinos.Count; minoNumber++)
        {
            MinoData.minoState Mino = dropingMinos[minoNumber];
            SetMino(Mino);
            if (!DropedRowList.Contains(Mino.Row))
            {
                DropedRowList.Insert(0,Mino.Row);
            }
        }

        while(DropedRowList.Count != 0)
        {
            CheckLine(DropedRowList[0]);
            DropedRowList.RemoveAt(0);
        }

        while(DeleteLineList.Count != 0)
        {
            DeleteLine(DeleteLineList[0]);
            DeleteLineList.RemoveAt(0);
        }

        if ()
        {

        }
    }

    public MinoData.E_MinoColor[,] GetMinoArray()
    {
        return _minoArray;
    }
}
