using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MinoLooks : MonoBehaviour,IFDropMinoLooksUpdata,IFStayMinoLooksUpdata
{
    // �C���^�[�t�F�[�X�̃C���X�^���X�p�ϐ�
    IFGetMinoArray _iGetMinoArray = default;

    // �~�m�̗�
    private const int ARRAY_ROW = 10;

    // �~�m�̍s��
    private const int ARRAY_COLUMN = 22;

    // �����Ă���~�m�̌�
    private const int MINO_MAXVALUE = 4;

    [SerializeField, ToolTip("�~�m�̕\���p�I�u�W�F�N�g")]
    GameObject _minoBlockObject = default;

    // �~�m�̕\���p�I�u�W�F�N�g�z��@���ꂼ���_minoBlockObject���i�[����
    GameObject[,] _minoBlockArray = new GameObject[22, 10];

    // �~�m�̌��ݐF�̕ۑ��p�z��@�e�ʒu���Ƃ̐F��ۑ�
    private MinoData.E_MinoColor[,] _nowMinoColor = new MinoData.E_MinoColor[22, 10];

    // ���ݗ������̃~�m�̈ʒu
    private int[,] _nowDropingMinoPosition = new int[4, 2];

    // �~�m�̊e�s�̈ʒu�@�������w�肷�邽�߂Ɏg�p
    private float[] _positionHeight = { -11,-10,-9,-8,-7,-6,-5,-4,-3,-2,-1,0,1,2,3,4,5,6,7,8,9,10};

    // �~�m�̊e��̈ʒu�@���E�̈ʒu���w�肷�邽�߂Ɏg�p
    private float[] _positionWidth = { -5,-4,-3,-2,-1,0,1,2,3,4};

    // �~�m�̐F -------------------------------------------
    private readonly Color _cyan = Color.cyan;
    private readonly Color _purple = new Color(1,0,1);
    private readonly Color _red = Color.red;
    private readonly Color _green = Color.green;
    private readonly Color _yellow = Color.yellow;
    private readonly Color _orange = new Color(1,1,0);
    private readonly Color _blue = Color.blue;
    // ----------------------------------------------------

    // �������̃~�m�̐F�@�������̃~�m��\�����邽�߂Ɏg�p
    private MinoData.E_MinoColor _dropingMinoColor = MinoData.E_MinoColor.empty;

    // �������̃~�m�����݂��Ȃ��Ƃ��ɑ������l
    private const int ENPTY_VALUE = -99;

    private void Awake()
    {
        /* �����ݒ� */

        // �C���^�[�t�F�[�X�̃C���X�^���X
        _iGetMinoArray = GameObject.FindWithTag("GameManager").GetComponent<IFGetMinoArray>();


        // �\���p�I�u�W�F�N�g�𐶐� ----------------------------------------------------------------------
        // �����ʒu
        Vector3 blockPosition = default;

        // �\���p�I�u�W�F�N�g�𐶐����Ĕz��Ɋi�[
        for (int i = 0; i < ARRAY_COLUMN;i++)
        {
            for (int k = 0; k < ARRAY_ROW;k++)
            {
                blockPosition.x = _positionWidth[k];    // ��̈ʒu�ݒ�
                blockPosition.y = _positionHeight[i];   // �s�̈ʒu�ݒ�
                _minoBlockArray[i, k] = Instantiate(_minoBlockObject,blockPosition,Quaternion.identity);
            }
        }
        // -----------------------------------------------------------------------------------------------
    }

    /// <summary>
    /// �������̃~�m�̕\�����X�V���郁�\�b�h
    /// </summary>
    /// <param name="dropingMinoPosition">�������̃~�m�̊e�u���b�N���̈ʒu</param>
    public void DropMinoLooksUpdata(int[,] dropingMinoPosition)
    {
        // �O��̕\�����폜
        DeleteLastDrop();

        // �������̃~�m�̃u���b�N�̌�
        int positionLength = dropingMinoPosition.GetLength(0) * dropingMinoPosition.GetLength(1);

        // �������̃~�m�̈ʒu���X�V
        Array.Copy(dropingMinoPosition, _nowDropingMinoPosition, positionLength);

        // �\�����X�V
        for (int i = 0; i < MINO_MAXVALUE; i++)
        {
            ChengeBlockColor(_minoBlockArray[_nowDropingMinoPosition[i,MinoData.COLUMN], _nowDropingMinoPosition[i,MinoData.ROW]], _dropingMinoColor) ;
        }
    }

    /// <summary>
    /// �������̃~�m�̐F��ݒ肷�郁�\�b�h
    /// </summary>
    /// <param name="minoColor">�ݒ肷��~�m�̐F</param>
    public void SetDropingMinoColor(MinoData.E_MinoColor minoColor)
    {
        // �������̃~�m�̐F��ݒ�
        _dropingMinoColor = minoColor;
    }

    /// <summary>
    /// �O��̗������̃~�m�̕\�����폜���郁�\�b�h
    /// </summary>
    public void DeleteLastDrop()
    {
        // ��\���ɍX�V -----------------------------------------------------------------------------------------------------------------------------------------------
        // �������̃~�m�����݂���ꍇ�̂ݎ��s
        if (_nowDropingMinoPosition[0,0] != ENPTY_VALUE)
        {
            for (int i = 0; i < MINO_MAXVALUE; i++)
            {
                // �\���̍X�V
                ChengeBlockColor(_minoBlockArray[_nowDropingMinoPosition[i, MinoData.COLUMN],_nowDropingMinoPosition[i, MinoData.ROW]], MinoData.E_MinoColor.empty);
            }
        }
        else
        {
            return;
        }
        // ------------------------------------------------------------------------------------------------------------------------------------------------------------
    }

    /// <summary>
    /// ��~���̃~�m�̕\�����X�V���郁�\�b�h
    /// </summary>
    public void StayMinoLooksUpdata()
    {
        // ��~���̃~�m�̔z����擾���A�\�����X�V ---------------------------------------
        // ��~���̃~�m�̔z����ꎞ�ۊǂ���ϐ�
        int[,] staticMinoArray = _iGetMinoArray.GetMinoArray();

        // �e�u���b�N���X�V
        for (int i = 0; i < ARRAY_COLUMN; i++)
        {
            for (int k = 0; k<ARRAY_ROW; k++)
            {
                // �\���ɕύX������ꍇ�̂ݍX�V
                if(_nowMinoColor[i,k] != staticMinoArray[i, k])
                {
                    // �F�̐ݒ�
                    _nowMinoColor[i, k] = staticMinoArray[i, k];
                    // �\���̍X�V
                    ChengeBlockColor(_minoBlockArray[i, k], staticMinoArray[i, k]);
                }
            }
        }
        // �������̃~�m���폜
        _nowDropingMinoPosition = _nowDropingMinoPosition.Select(x => 0).ToArray();
        // ------------------------------------------------------------------------------
    }

    private void ChengeBlockColor(GameObject block,MinoData.E_MinoColor minoColor)
    {
        switch (minoColor)
        {
            case MinoData.E_MinoColor.cyan:
                block.GetComponent<Renderer>().material.color = _cyan;
                block.SetActive(true);
                break;
            case MinoData.E_MinoColor.purple:
                block.GetComponent<Renderer>().material.color = _purple;
                block.SetActive(true);
                break;
            case MinoData.E_MinoColor.red:
                block.GetComponent<Renderer>().material.color = _red;
                block.SetActive(true);
                break;
            case MinoData.E_MinoColor.green:
                block.GetComponent<Renderer>().material.color = _green;
                block.SetActive(true);
                break;
            case MinoData.E_MinoColor.yellow:
                block.GetComponent<Renderer>().material.color = _yellow;
                block.SetActive(true);
                break;
            case MinoData.E_MinoColor.orange:
                block.GetComponent<Renderer>().material.color = _orange;
                block.SetActive(true);
                break;
            case MinoData.E_MinoColor.blue:
                block.GetComponent<Renderer>().material.color = _blue;
                block.SetActive(true);
                break;
            case MinoData.E_MinoColor.empty:
                block.SetActive(false);
                break;
        }
    }
}
