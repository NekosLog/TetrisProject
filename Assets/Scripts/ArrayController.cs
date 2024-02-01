using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ArrayController : MonoBehaviour, IFLandingMinos, IFGetMinoArray
{
    #region ïœêî
    private IFStayMinoLooksUpdata _iStayMinoLooksUpdata;
    private IFDropStart _iDropStart;

    private MinoData.E_MinoColor[,] _minoArray = new MinoData.E_MinoColor[ArrayData.ROW, ArrayData.COLUMN];
    private List<int> DropedRowList = new List<int>();
    private List<int> DeleteLineList = new List<int>();
    private int _deleteLineCounter = default;
    private int[] _fallValueArray = new int[ArrayData.ROW - 1];

    #endregion

    private void Awake()
    {
        _iStayMinoLooksUpdata = GameObject.FindWithTag("GameManager").GetComponent<IFStayMinoLooksUpdata>();
        _iDropStart = GameObject.FindWithTag("GameManager").GetComponent<IFDropStart>();
        ClearArray();
        _iStayMinoLooksUpdata.StayMinoLooksUpdata();
    }

    private void DeleteArray()
    {
        while (DeleteLineList.Count > 0)
        {
            DeleteLine(DeleteLineList[0]);
            DeleteLineList.RemoveAt(0);
            if (DeleteLineList.Count == 0)
            {
                FallArray();
            }
        }
    }

    private void DeleteLine(int row)
    {
        for (int column = 0; column < _minoArray.GetLength(1); column++)
        {
            _minoArray[row, column] = MinoData.E_MinoColor.empty;
        }
        for (int addPoint = row; addPoint < _minoArray.GetLength(0) - 1; addPoint++)
        {
            _fallValueArray[addPoint]++;
        }
        _deleteLineCounter++;
    }

    private void FallArray()
    {
        int row = 1;
        foreach (int fallValue in _fallValueArray)
        {
            if (fallValue > 0)
            {
                FallLine(row, fallValue);
            }
            row++;
        }
        _fallValueArray = _fallValueArray.Select(x => 0).ToArray();
    }

    private void FallLine(int row, int fallValue)
    {
        int landingRow = row - fallValue;
        for(int column = 0; column < _minoArray.GetLength(1); column++)
        {
            if(_minoArray[row,column] != MinoData.E_MinoColor.empty)
            {
                _minoArray[landingRow, column] = _minoArray[row, column];
                _minoArray[row, column] = MinoData.E_MinoColor.empty;
            }
        }
    }

    private void ClearArray()
    {
        for (int row = 0; row < _minoArray.GetLength(0); row++)
        {
            for (int column = 0; column < _minoArray.GetLength(1); column++)
            {
                _minoArray[row, column] = MinoData.E_MinoColor.empty;
            }
        }
    }

    private void SetMino(MinoData.minoState minoState)
    {
        _minoArray[minoState.Row, minoState.Column] = minoState.minoColor;
    }

    private void CheckArray()
    {
        while (DropedRowList.Count > 0)
        {
            CheckLine(DropedRowList[0]);
            DropedRowList.RemoveAt(0);
            if(DropedRowList.Count == 0)
            {
                DeleteArray();
            }
        }
    }

    private void CheckLine(int row)
    {
        for (int Column = 0; Column < _minoArray.GetLength(1); Column++)
        {
            if(_minoArray[row,Column] == MinoData.E_MinoColor.empty)
            {
                return;
            }
        }
        DeleteLineList.Add(row);
    }

    public void LandingMinos(List<MinoData.minoState> dropingMinos)
    {
        for(int minoNumber = 0;minoNumber < dropingMinos.Count; minoNumber++)
        {
            MinoData.minoState Mino = dropingMinos[minoNumber];
            SetMino(Mino);
            if (!DropedRowList.Contains(Mino.Row))
            {
                DropedRowList.Add(Mino.Row);
            }
        }

        CheckArray();
        _iStayMinoLooksUpdata.StayMinoLooksUpdata();

        int OverRow = 20;

        for (int column = 0; column < ArrayData.COLUMN; column++)
        {
            if(_minoArray[OverRow, column] != MinoData.E_MinoColor.empty)
            {

                return;
            }
        }

        _iDropStart.ReFresh();
        _iDropStart.DropStart();
    }

    public MinoData.E_MinoColor[,] GetMinoArray()
    {
        return _minoArray;
    }
}
