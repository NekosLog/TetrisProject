interface IFDropMinoLooksUpdata
{
    void DropMinoLooksUpdata(int[,] dropingMinoPosition);

    void DeleteLastDrop();

    void SetDropingMinoColor(MinoData.E_MinoColor minoColor);
}