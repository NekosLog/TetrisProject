/// <summary>
/// ArrayController����z����擾���邽�߂̃C���^�[�t�F�[�X
/// </summary>
interface IFGetMinoArray
{
    /// <summary>
    /// ��~���̃~�m�̔z����擾���郁�\�b�h
    /// </summary>
    /// <returns>��~���̃~�m�̔z��</returns>
    MinoData.E_MinoColor[,] GetMinoArray();
}