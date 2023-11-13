/// <summary>
/// ArrayControllerから配列を取得するためのインターフェース
/// </summary>
interface IFGetMinoArray
{
    /// <summary>
    /// 停止中のミノの配列を取得するメソッド
    /// </summary>
    /// <returns>停止中のミノの配列</returns>
    MinoData.E_MinoColor[,] GetMinoArray();
}