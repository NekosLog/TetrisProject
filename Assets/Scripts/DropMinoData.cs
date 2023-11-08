using System;
using UnityEngine;
public class DropMinoData : MonoBehaviour, IFGetDropMinoData, IFDropMino
{
    static Vector2[] a = { new Vector2 { }, new Vector2 { } };

    private MinoData.E_MinoColor _nextMino = MinoData.E_MinoColor.red;

    private MinoData.E_MinoColor _secondMino = MinoData.E_MinoColor.blue;

    private MinoData.E_MinoColor _thirdMino = MinoData.E_MinoColor.green;

    private MinoData.E_MinoColor _holdingMino = MinoData.E_MinoColor.empty;

    System.Random random = new System.Random();

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
        MinoData.E_MinoColor returnMinoColor = _nextMino;
        PrepareMino();
        return returnMinoColor;
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
        _thirdMino = (MinoData.E_MinoColor)random.Next(0,Enum.GetValues(typeof(MinoData.E_MinoColor)).Length - 1);
    }
}
