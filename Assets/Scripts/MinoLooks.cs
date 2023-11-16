using System;
using UnityEngine;

/// <summary>
/// �~�m�̕\���𐧌䂷��N���X
/// </summary>
public class MinoLooks : MonoBehaviour,IFDropMinoLooksUpdata,IFStayMinoLooksUpdata
{
    #region �ϐ���

    #region �N���X�^�ϐ�
    // �C���^�[�t�F�[�X�̃C���X�^���X�p�ϐ�
    private IFGetMinoArray _iGetMinoArray = default;
    #endregion

    [SerializeField, Tooltip("�~�m�̕\���p�I�u�W�F�N�g")]
    private GameObject _minoBlockObject = default;


    // �~�m�̍s��
    private const int FIELD_ROW = 10;

    // �~�m�̗�
    private const int FIELD_COLUMN = 22;

    // �����Ă���~�m�̌�
    private const int MINO_MAX_VALUE = 4;

    // �������̃~�m�����݂��Ȃ��Ƃ��ɑ������l
    private const int ENPTY_VALUE = -99;

    // �~�m�̕\���p�I�u�W�F�N�g�z��@���ꂼ���_minoBlockObject���i�[����
    private GameObject[,] _minoBlockArray = new GameObject[22, 10];

    // �~�m�̌��ݐF�̕ۑ��p�z��@�e�ʒu���Ƃ̐F��ۑ�
    private MinoData.E_MinoColor[,] _nowMinoColor = new MinoData.E_MinoColor[22, 10];

    // �������̃~�m�̐F�@�������̃~�m��\�����邽�߂Ɏg�p
    private MinoData.E_MinoColor _dropingMinoColor = MinoData.E_MinoColor.empty;

    // ���ݗ������̃~�m�̈ʒu
    private int[,] _nowDropingMinoPosition = new int[4, 2];

    // �~�m�̐F -------------------------------------------
    private readonly Color _cyan     = new Color( 0,1,1);
    private readonly Color _purple   = new Color( 1,0,1);
    private readonly Color _red      = new Color( 1,0,0);
    private readonly Color _green    = new Color( 0,1,0);
    private readonly Color _yellow   = new Color( 1,1,0);
    private readonly Color _orange   = new Color( 1,0.6f,0);
    private readonly Color _blue     = new Color( 0,0,1);
    // ----------------------------------------------------

    #endregion

    private void Awake()
    {
        /* �����ݒ� */

        // �C���^�[�t�F�[�X�̃C���X�^���X
        _iGetMinoArray = GameObject.FindWithTag("GameManager").GetComponent<IFGetMinoArray>();


        // �����ʒu
        Vector3 blockPosition = default;

        // �~�m�̈ʒu��ݒ肷�錴�_�̈ʒu
        Vector2 _positionOrigin = new Vector2(-5f, -11f);

        // �~�m�̐ݒu�Ԋu
        const float POSITION_INTERVAL = 1f;

        // �\���p�I�u�W�F�N�g�̐���
        for (int Row = 0; Row < FIELD_COLUMN; Row++)
        {
            for (int Column = 0; Column < FIELD_ROW; Column++)
            {
                blockPosition.x = _positionOrigin.x + (POSITION_INTERVAL * Column); // ��̈ʒu�ݒ�
                blockPosition.y = _positionOrigin.y + (POSITION_INTERVAL * Row);    // �s�̈ʒu�ݒ�
                _minoBlockArray[Row, Column] = Instantiate(_minoBlockObject,blockPosition,Quaternion.identity);
            }
        }
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
        DropUpdata(_dropingMinoColor);
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
        // �������̃~�m�����݂���ꍇ�̂ݎ��s
        if (_nowDropingMinoPosition[0,0] != ENPTY_VALUE)
        {
            // �~�m�����݂���ꍇ

            // �\�����X�V
            DropUpdata(MinoData.E_MinoColor.empty);
        }
        else
        {
            // �~�m�����݂��Ȃ��ꍇ
            return;
        }
    }

    /// <summary>
    /// ��~���̃~�m�̕\�����X�V���郁�\�b�h
    /// </summary>
    public void StayMinoLooksUpdata()
    {
        // ��~���̃~�m�̔z����ꎞ�ۊǂ���ϐ�
        MinoData.E_MinoColor[,] staticMinoArray = _iGetMinoArray.GetMinoArray();

        // �e�u���b�N���X�V
        for (int i = 0; i < FIELD_COLUMN; i++)
        {
            for (int k = 0; k<FIELD_ROW; k++)
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
        _nowDropingMinoPosition[0, 0] = ENPTY_VALUE;
    }

    /// <summary>
    /// �������̃~�m�̐F��ݒ肷�郁�\�b�h
    /// </summary>
    /// <param name="minoColor">�ݒ肷��F</param>
    private void DropUpdata(MinoData.E_MinoColor minoColor)
    {
        // �s��w��p�̍�ƕϐ�
        int row = default;
        int column = default;

        // �e�u���b�N���X�V
        for (int i = 0; i < MINO_MAX_VALUE; i++)
        {
            row = _nowDropingMinoPosition[i, MinoData.ROW];         // �s�ݒ�
            column = _nowDropingMinoPosition[i, MinoData.COLUMN];   // ��ݒ�

            // �\���̍X�V
            ChengeBlockColor(_minoBlockArray[row, column], minoColor);
        }
    }

    /// <summary>
    /// �\���p�I�u�W�F�N�g���X�V���郁�\�b�h
    /// </summary>
    /// <param name="block">�X�V����\���p�I�u�W�F�N�g</param>
    /// <param name="minoColor">�X�V����F</param>
    private void ChengeBlockColor(GameObject block,MinoData.E_MinoColor minoColor)
    {
        // �\������\��������
        if(minoColor != MinoData.E_MinoColor.empty)
        {
            // �F�̐ݒ�
            block.GetComponent<Renderer>().material.color = ChengeColor(minoColor);
            // �\������
            block.SetActive(true);
        }
        else
        {
            // ��\���ɂ���
            block.SetActive(false);
        }
    }

    /// <summary>
    /// E_MinoColor��Color�ɕϊ�����
    /// </summary>
    /// <param name="minoColor">�ϊ�����E_MinoColor</param>
    /// <returns>�ϊ����ꂽColor</returns>
    private Color ChengeColor(MinoData.E_MinoColor minoColor)
    {
        // �Ԃ�l�p�ϐ�
        Color returnColor = default;

        // �F���Ƃɕ���
        switch (minoColor)
        {
            // ���F
            case MinoData.E_MinoColor.cyan:
                returnColor = _cyan;
                break;

            // ���F
            case MinoData.E_MinoColor.purple:
                returnColor = _purple;
                break;

            // �ԐF
            case MinoData.E_MinoColor.red:
                returnColor = _red;
                break;

            // �ΐF
            case MinoData.E_MinoColor.green:
                returnColor = _green;
                break;

            // ���F
            case MinoData.E_MinoColor.yellow:
                returnColor = _yellow;
                break;

            // �I�����W�F
            case MinoData.E_MinoColor.orange:
                returnColor = _orange;
                break;

            // �F
            case MinoData.E_MinoColor.blue:
                returnColor = _blue;
                break;
        }
        //�F��Ԃ�
        return returnColor;
    }
}
