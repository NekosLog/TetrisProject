using System;
using UnityEngine;
using System.Collections.Generic;
public class DropMinoData : MonoBehaviour, IFGetDropMinoData, IFDropMino
{
    static Vector2[] a = { new Vector2 { }, new Vector2 { } };

    private MinoData.E_MinoColor _nextMino = MinoData.E_MinoColor.empty;

    private MinoData.E_MinoColor _secondMino = MinoData.E_MinoColor.empty;

    private MinoData.E_MinoColor _thirdMino = MinoData.E_MinoColor.empty;

    private MinoData.E_MinoColor _holdingMino = MinoData.E_MinoColor.empty;

    private readonly List<int> _MinoList = new List<int> { 0, 1, 2, 3, 4, 5, 6 };

    private List<int> _NextMinoList = new List<int> { 0, 1, 2, 3, 4, 5, 6 };

    MinoData.E_MinoColor IFGetDropMinoData.GetNextMino()
    {
        return _nextMino;
    }

    MinoData.E_MinoColor IFGetDropMinoData.GetSecondMino()
    {
        return _secondMino;
    }

    MinoData.E_MinoColor IFGetDropMinoData.GetThirdMino()
    {
        return _thirdMino;
    }

    public MinoData.E_MinoColor GetDropMino()
    {
        if(_nextMino == MinoData.E_MinoColor.empty)
        {
            PrepareMino();
            return GetDropMino();
        }
        else
        {
            MinoData.E_MinoColor returnMinoColor = _nextMino;
            PrepareMino();
            return returnMinoColor;
        }
    }

    public MinoData.E_MinoColor ChangeHold(MinoData.E_MinoColor dropingMino)
    {
        MinoData.E_MinoColor returnMinoColor;
        if (_holdingMino != MinoData.E_MinoColor.empty)
        {
            returnMinoColor = _holdingMino;
        }
        else
        {
            returnMinoColor = GetDropMino();
        }
        _holdingMino = dropingMino;
        return returnMinoColor;
    }

    private void PrepareMino()
    {
        _nextMino = _secondMino;
        _secondMino = _thirdMino;
        _thirdMino = (MinoData.E_MinoColor)GetNextMinoNumber();
    }

    private int GetNextMinoNumber()
    {
        int nextNumber = _NextMinoList[UnityEngine.Random.Range(0, _NextMinoList.Count)];
        _NextMinoList.Remove(nextNumber);

        if (_NextMinoList.Count <= 0)
        {
            _NextMinoList = new List<int>(_MinoList);
        }
        return nextNumber;
    }
}
