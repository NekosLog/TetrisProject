using System;
public class DropMinoData : IFGetDropMinoData, IFDropMino
{
    private MinoData.E_MinoColor _nextMino = MinoData.E_MinoColor.red;

    private MinoData.E_MinoColor _secondMino = MinoData.E_MinoColor.blue;

    private MinoData.E_MinoColor _thirdMino = MinoData.E_MinoColor.green;

    Random random = new Random();

    public MinoData.E_MinoColor GetNextMino()
    {
        return _nextMino;
    }

    public MinoData.E_MinoColor GetSecondMino()
    {
        return _secondMino;
    }

    public MinoData.E_MinoColor GetThirdMino()
    {
        return _thirdMino;
    }

    public MinoData.E_MinoColor GetDropMino()
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
