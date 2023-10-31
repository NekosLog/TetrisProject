using System;
using UnityEngine;
public class DropMinoData : MonoBehaviour, IFGetDropMinoData, IFDropMino
{
    private MinoData.E_MinoColor _nextMino = MinoData.E_MinoColor.red;

    private MinoData.E_MinoColor _secondMino = MinoData.E_MinoColor.blue;

    private MinoData.E_MinoColor _thirdMino = MinoData.E_MinoColor.green;

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

    MinoData.E_MinoColor IFDropMino.GetDropMino()
    {
        MinoData.E_MinoColor dropMino = _nextMino;
        PrepareMino();
        return dropMino;
    }

    private void PrepareMino()
    {
        _nextMino = _secondMino;
        _secondMino = _thirdMino;
        _thirdMino = (MinoData.E_MinoColor)random.Next(0,Enum.GetValues(typeof(MinoData.E_MinoColor)).Length);
    }
}
