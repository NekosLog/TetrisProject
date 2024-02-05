using System.Collections.Generic;
using UnityEngine;

public class DropingMino : MonoBehaviour, IFInputMainGame, IFDropStart
{
    #region 変数
    private IFDropMino _iDropMino;
    private IFDropInterval _iGetDropInterval;
    private IFGetMinoArray _iGetMinoArray;
    private IFLandingMinos _iLandingMinos;
    private IFDropMinoLooksUpdata _iDropMinoLooksUpdata;
    private SoundData _sound = default;

    private MinoData.E_MinoColor _dropingMinoColor = MinoData.E_MinoColor.empty;
    private float[] _dropingMinoOrigin = new float[2];
    private float[,] _dropingminoPositions = new float[4, 2];
    private List<MinoData.minoState> _landingMinoList;
    private float _minoFallInterval = 1f;
    private float _minoFallTime = 0f;

    private int _nowRotate = default;

    private float _rightInterval = default;
    private float _leftInterval = default;
    private float _downInterval = default;

    private enum E_RotationVector { right = 0, left = 1 }
    private E_RotationVector _rotationRight = E_RotationVector.right;
    private E_RotationVector _rotationLeft = E_RotationVector.left;

    private const float ORIGIN_STRANGE_ROW = 20.5f;
    private const float ORIGIN_STRANGE_COLUMN = 4.5f;

    private const float ORIGIN_NORMAL_ROW = 20f;
    private const float ORIGIN_NORMAL_COLUMN = 4f;

    private const int ROTATE_VALUE = 90;

    private const int MAX_COLUMN = ArrayData.COLUMN - 1;
    private const int MIN_COLUMN = 0;
    private const int MIN_ROW = 0;

    private readonly int[] up    = { 1, 0};
    private readonly int[] down  = {-1, 0};
    private readonly int[] right = { 0, 1};
    private readonly int[] left  = { 0,-1};


    private readonly float[,] INITIAL_POSITIONS_CYAN = 
    {
        {0.5f,-1.5f},
        {0.5f,-0.5f},
        {0.5f,0.5f},
        {0.5f,1.5f}
    };

    private readonly float[,] INITIAL_POSITIONS_PURPLE = 
    { 
        {0f,-1f},
        {0f, 0f},
        {0f, 1f},
        {1f, 0f} 
    };

    private readonly float[,] INITIAL_POSITIONS_RED =
    {
        {1f,-1f},
        {1f, 0f},
        {0f, 0f},
        {0f, 1f}
    };

    private readonly float[,] INITIAL_POSITIONS_GREEN =
    {
        {0f,-1f},
        { 0f,0f},
        {1f, 0f},
        {1f, 1f}
    };

    private readonly float[,] INITIAL_POSITIONS_YELLOW =
    {
        {-0.5f,-0.5f},
        {-0.5f, 0.5f},
        { 0.5f,-0.5f},
        { 0.5f, 0.5f}
    };

    private readonly float[,] INITIAL_POSITIONS_ORANGE =
    {
        {0f,-1f},
        { 0f,0f},
        { 0f,1f},
        { 1f,1f}
    };

    private readonly float[,] INITIAL_POSITIONS_BLUE =
    {
        {0f,-1f},
        { 0f,0f},
        { 0f,1f},
        { 1f,-1f}
    };

    #endregion

    private void Awake()
    {
        _iDropMino = GameObject.FindWithTag("GameManager").GetComponent<IFDropMino>();
        _iGetDropInterval = GameObject.FindWithTag("GameManager").GetComponent<IFDropInterval>();
        _iGetMinoArray = GameObject.FindWithTag("GameManager").GetComponent<IFGetMinoArray>();
        _iLandingMinos = GameObject.FindWithTag("GameManager").GetComponent<IFLandingMinos>();
        _iDropMinoLooksUpdata = GameObject.FindWithTag("GameManager").GetComponent<IFDropMinoLooksUpdata>();
        // SEデータの取得
        _sound = GameObject.FindWithTag("GameManager").GetComponent<SoundData>();
    }

    private void Update()
    {
        if (_rightInterval > 0)
        {
            _rightInterval -= Time.deltaTime;
        }

        if (_leftInterval > 0)
        {
            _leftInterval -= Time.deltaTime;
        }

        if (_downInterval > 0)
        {
            _downInterval -= Time.deltaTime;
        }

        if (_dropingMinoColor != MinoData.E_MinoColor.empty)
        {
            _minoFallTime += Time.deltaTime;
            if(_minoFallTime > _iGetDropInterval.GetMinoDropInterval())
            {
                _minoFallTime = 0f;
                if (CanMove(down))
                {
                    int moveDown = -1;
                    MoveMino(moveDown, 0);
                    _iDropMinoLooksUpdata.DropMinoLooksUpdata(GetAllDropingMinoPosition());
                }
                else
                {
                    _sound._SEspeaker.PlayOneShot(_sound._minoLandingSE);
                    _iLandingMinos.LandingMinos(LandingMinoList());
                }
            }
        }
    }

    #region Input関係
    public void InputHoldRight()
    {
        if(_dropingMinoColor != MinoData.E_MinoColor.empty && _rightInterval <= 0)
        {
            if (CanMove(right))
            {
                _sound._SEspeaker.PlayOneShot(_sound._minoTranslateSE);
                int moveRight = 1;
                MoveMino(0,moveRight);
                _rightInterval = _iGetDropInterval.GetMinoMoveInterval();
                _iDropMinoLooksUpdata.DropMinoLooksUpdata(GetAllDropingMinoPosition());
            }
        }
    }

    public void InputHoldLeft()
    {
        if (_dropingMinoColor != MinoData.E_MinoColor.empty && _leftInterval <= 0)
        {
            if (CanMove(left))
            {
                _sound._SEspeaker.PlayOneShot(_sound._minoTranslateSE);
                int moveLeft = -1;
                MoveMino(0, moveLeft);
                _leftInterval = _iGetDropInterval.GetMinoMoveInterval();
                _iDropMinoLooksUpdata.DropMinoLooksUpdata(GetAllDropingMinoPosition());
            }
        }
    }

    public void InputDownUp()
    {
        if (_dropingMinoColor != MinoData.E_MinoColor.empty)
        {
            while (CanMove(down))
            {
                int moveDown = -1;
                MoveMino(moveDown, 0);
            }
            _sound._SEspeaker.PlayOneShot(_sound._minoLandingSE);
            _iLandingMinos.LandingMinos(LandingMinoList());
        }
    }

    public void InputHoldDown()
    {
        if(_dropingMinoColor != MinoData.E_MinoColor.empty && _downInterval <= 0)
        {
            if (CanMove(down))
            {
                _minoFallTime = 0f;
                int moveDown = -1;
                MoveMino(moveDown, 0);
                _downInterval = _iGetDropInterval.GetMinoMoveDownInterval();
                _iDropMinoLooksUpdata.DropMinoLooksUpdata(GetAllDropingMinoPosition());
            }
            else
            {
                _sound._SEspeaker.PlayOneShot(_sound._minoLandingSE);
                _iLandingMinos.LandingMinos(LandingMinoList());
            }
        }
    }

    public void InputDownDecision()
    {
        _sound._SEspeaker.PlayOneShot(_sound._minoRotateSE);
        int nextRotate = _nowRotate - ROTATE_VALUE;

        if (nextRotate == -180)
        {
            nextRotate = 180;
        }

        float[,] nextPositions = RotationMino(_dropingminoPositions,_rotationLeft);

        if (CanMove(nextPositions))
        {
            _nowRotate = nextRotate;
            _dropingminoPositions = nextPositions;
            _iDropMinoLooksUpdata.DropMinoLooksUpdata(GetAllDropingMinoPosition());
        }
        else
        {
            SuperRotate(nextRotate, nextPositions);
        }
    }

    public void InputDownCancel()
    {
        _sound._SEspeaker.PlayOneShot(_sound._minoRotateSE);
        int nextRotate = _nowRotate + ROTATE_VALUE;

        if (nextRotate == 270)
        {
            nextRotate = -90;
        }

        float[,] nextPositions = RotationMino(_dropingminoPositions, _rotationRight);

        if (CanMove(nextPositions))
        {
            _nowRotate = nextRotate;
            _dropingminoPositions = nextPositions;
            _iDropMinoLooksUpdata.DropMinoLooksUpdata(GetAllDropingMinoPosition());
        }
        else
        {
            SuperRotate(nextRotate, nextPositions);
        }
    }

    public void InputDownHold()
    {
        if(_dropingMinoColor != MinoData.E_MinoColor.empty)
        {
            _dropingMinoColor = _iDropMino.ChangeHold(_dropingMinoColor);
            DropStart();
        }
    }

    #endregion

    private void TimeReSet()
    {
        _minoFallTime = 0f;
    }

    public void DropStart()
    {
        TimeReSet();
        if(_dropingMinoColor == MinoData.E_MinoColor.empty)
        {
            _dropingMinoColor = _iDropMino.GetDropMino();
        }
        _iDropMinoLooksUpdata.SetDropingMinoColor(_dropingMinoColor);

        switch (_dropingMinoColor)
        {
            case MinoData.E_MinoColor.cyan:
                _dropingMinoOrigin[MinoData.COLUMN_NUMBER] = ORIGIN_STRANGE_COLUMN;
                _dropingMinoOrigin[MinoData.ROW_NUMBER] = ORIGIN_STRANGE_ROW;
                _dropingminoPositions = INITIAL_POSITIONS_CYAN;
                _iDropMinoLooksUpdata.DropMinoLooksUpdata(GetAllDropingMinoPosition());
                break;


            case MinoData.E_MinoColor.purple:
                _dropingMinoOrigin[MinoData.COLUMN_NUMBER] = ORIGIN_NORMAL_COLUMN;
                _dropingMinoOrigin[MinoData.ROW_NUMBER] = ORIGIN_NORMAL_ROW;
                _dropingminoPositions = INITIAL_POSITIONS_PURPLE;
                _iDropMinoLooksUpdata.DropMinoLooksUpdata(GetAllDropingMinoPosition());
                break;


            case MinoData.E_MinoColor.red:
                _dropingMinoOrigin[MinoData.COLUMN_NUMBER] = ORIGIN_NORMAL_COLUMN;
                _dropingMinoOrigin[MinoData.ROW_NUMBER] = ORIGIN_NORMAL_ROW;
                _dropingminoPositions = INITIAL_POSITIONS_RED;
                _iDropMinoLooksUpdata.DropMinoLooksUpdata(GetAllDropingMinoPosition());
                break;


            case MinoData.E_MinoColor.green:
                _dropingMinoOrigin[MinoData.COLUMN_NUMBER] = ORIGIN_NORMAL_COLUMN;
                _dropingMinoOrigin[MinoData.ROW_NUMBER] = ORIGIN_NORMAL_ROW;
                _dropingminoPositions = INITIAL_POSITIONS_GREEN;
                _iDropMinoLooksUpdata.DropMinoLooksUpdata(GetAllDropingMinoPosition());
                break;


            case MinoData.E_MinoColor.yellow:
                _dropingMinoOrigin[MinoData.COLUMN_NUMBER] = ORIGIN_STRANGE_COLUMN;
                _dropingMinoOrigin[MinoData.ROW_NUMBER] = ORIGIN_STRANGE_ROW;
                _dropingminoPositions = INITIAL_POSITIONS_YELLOW;
                _iDropMinoLooksUpdata.DropMinoLooksUpdata(GetAllDropingMinoPosition());
                break;


            case MinoData.E_MinoColor.orange:
                _dropingMinoOrigin[MinoData.COLUMN_NUMBER] = ORIGIN_NORMAL_COLUMN;
                _dropingMinoOrigin[MinoData.ROW_NUMBER] = ORIGIN_NORMAL_ROW;
                _dropingminoPositions = INITIAL_POSITIONS_ORANGE;
                _iDropMinoLooksUpdata.DropMinoLooksUpdata(GetAllDropingMinoPosition());
                break;


            case MinoData.E_MinoColor.blue:
                _dropingMinoOrigin[MinoData.COLUMN_NUMBER] = ORIGIN_NORMAL_COLUMN;
                _dropingMinoOrigin[MinoData.ROW_NUMBER] = ORIGIN_NORMAL_ROW;
                _dropingminoPositions = INITIAL_POSITIONS_BLUE;
                _iDropMinoLooksUpdata.DropMinoLooksUpdata(GetAllDropingMinoPosition());
                break;
        }
    }

    private void MoveMino(float row, float column)
    {
        _dropingMinoOrigin[MinoData.ROW_NUMBER] += row;
        _dropingMinoOrigin[MinoData.COLUMN_NUMBER] += column;
    }

    private void RotateReSet()
    {
        _nowRotate = 0;
    }

    private bool CanMove()
    {
        for (int minoNumber = 0; minoNumber < 4; minoNumber++)
        {
            int dropingMinoRow = GetDropingMinoRow(minoNumber);
            int dropingMinoColumn = GetDropingMinoColumn(minoNumber);

            if(CheckOut(dropingMinoRow, dropingMinoColumn) || CheckStack(dropingMinoRow, dropingMinoColumn))
            {
                return false;
            }
        }
        return true;
    }

    private bool CanMove(int[] addValue)
    {
        for (int minoNumber = 0; minoNumber < 4; minoNumber++)
        {
            int dropingMinoRow = GetDropingMinoRow(minoNumber) + addValue[MinoData.ROW_NUMBER];
            int dropingMinoColumn = GetDropingMinoColumn(minoNumber) + addValue[MinoData.COLUMN_NUMBER];

            if (CheckOut(dropingMinoRow, dropingMinoColumn) || CheckStack(dropingMinoRow, dropingMinoColumn))
            {
                return false;
            }
        }
        return true;
    }

    private bool CanMove(float[,] positions)
    {
        for (int minoNumber = 0; minoNumber < 4; minoNumber++)
        {
            int dropingMinoRow = GetDropingMinoRow(minoNumber);
            int dropingMinoColumn = GetDropingMinoColumn(minoNumber);

            if (CheckOut(dropingMinoRow, dropingMinoColumn) || CheckStack(dropingMinoRow, dropingMinoColumn))
            {
                return false;
            }
        }
        return true;
    }

    private bool CanMove(float[,] positions ,int[] addValue)
    {
        for (int minoNumber = 0; minoNumber < 4; minoNumber++)
        {
            int dropingMinoRow = GetDropingMinoRow(minoNumber) + addValue[MinoData.ROW_NUMBER];
            int dropingMinoColumn = GetDropingMinoColumn(minoNumber) + addValue[MinoData.COLUMN_NUMBER];

            if (CheckOut(dropingMinoRow, dropingMinoColumn) || CheckStack(dropingMinoRow, dropingMinoColumn))
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckStack(int row, int column)
    {
        if(_iGetMinoArray.GetMinoArray()[row ,column]!= MinoData.E_MinoColor.empty)
        {
            print("stack");
            return true;
        }
        print("noStack");
        return false;
    }

    private bool CheckOut(int row, int column)
    {
        if (column < MIN_COLUMN || column > MAX_COLUMN || row < MIN_ROW)
        {
            print("out" + "  row = " + row + "    column = " + column);
            return true;
        }
        else
        {
            print("noOut");
            return false;
        }
    }


    private List<MinoData.minoState> LandingMinoList()
    {
        List<MinoData.minoState> list = new List<MinoData.minoState>();
        MinoData.minoState mino = default;

        mino.minoColor = _dropingMinoColor;
        for (int i = 0; i < 4; i++)
        {
            mino.Row = GetDropingMinoRow(i);
            mino.Column = GetDropingMinoColumn(i);
            list.Insert(0,mino);
        }
        return list;
    }

    public void ReFresh()
    {
        _dropingMinoColor = MinoData.E_MinoColor.empty;
        //_dropingMinoOrigin = default;
        //_dropingminoPositions = default;
        _downInterval = 0;
        _minoFallTime = 0;
        RotateReSet();
    }

    private float[,] RotationMino(float[,] positions , E_RotationVector rotationVector)
    {
        for(int i = 0; i < 4; i++)
        {
            float posX = positions[i, MinoData.COLUMN_NUMBER];
            float posY = positions[i, MinoData.ROW_NUMBER];

            switch (rotationVector)
            {
                case E_RotationVector.right:
                    positions[i, MinoData.COLUMN_NUMBER] = posY;
                    positions[i, MinoData.ROW_NUMBER] = -posX;
                    break;

                case E_RotationVector.left:
                    positions[i, MinoData.COLUMN_NUMBER] = -posY;
                    positions[i, MinoData.ROW_NUMBER] = posX;
                    break;
            }
        }
        return positions;
    }

    private enum E_RotatePattern
    {
        Other_A,
        Other_B,
        Other_C,
        Other_D,
        T_A,
        T_B,
        T_C,
        T_D,
        T_E,
        I_A,
        I_B,
        I_C,
        I_D,
        I_E,
        I_F,
        I_G,
        I_H
    }


    private void SuperRotate(int nextRotate, float[,] nextPosition)
    {
        E_RotatePattern rotatePattern = default;

        // 回転パターンの判別

        // Iミノ（シアン）とTミノ（パープル）以外の場合
        if (_dropingMinoColor != MinoData.E_MinoColor.cyan && _dropingMinoColor != MinoData.E_MinoColor.purple)
        {
            if (nextRotate == -90)
            {
                rotatePattern = E_RotatePattern.Other_A;
            }
            else if (nextRotate == 90)
            {
                rotatePattern = E_RotatePattern.Other_B;
            }
            else if (_nowRotate == 90)
            {
                rotatePattern = E_RotatePattern.Other_C;
            }
            else if (_nowRotate == -90)
            {
                rotatePattern = E_RotatePattern.Other_D;
            }
            else
            {
                Debug.LogError("想定外の回転");
            }
        }
        // Tミノ（パープル）の場合
        else if (_dropingMinoColor == MinoData.E_MinoColor.purple)
        {
            if (nextRotate == -90)
            {
                rotatePattern = E_RotatePattern.T_A;
            }
            else if (nextRotate == 90)
            {
                rotatePattern = E_RotatePattern.T_B;
            }
            else if (_nowRotate == 90)
            {
                rotatePattern = E_RotatePattern.T_C;
            }
            else if (_nowRotate == -90 && nextRotate == 180)
            {
                rotatePattern = E_RotatePattern.T_D;
            }
            else if (_nowRotate == -90 && nextRotate == 0)
            {
                rotatePattern = E_RotatePattern.T_E;
            }
            else
            {
                Debug.LogError("想定外の回転");
            }
        }
        // Iミノ（シアン）の場合
        else
        {
            if (_nowRotate == 0 && nextRotate == -90)
            {
                rotatePattern = E_RotatePattern.I_A;
            }
            else if (_nowRotate == 0 && nextRotate == 90)
            {
                rotatePattern = E_RotatePattern.I_B;
            }
            else if (_nowRotate == 90 && nextRotate == 0)
            {
                rotatePattern = E_RotatePattern.I_C;
            }
            else if (_nowRotate == 90 && nextRotate == 180)
            {
                rotatePattern = E_RotatePattern.I_D;
            }
            else if (_nowRotate == 180 && nextRotate == 90)
            {
                rotatePattern = E_RotatePattern.I_E;
            }
            else if(_nowRotate == 180 && nextRotate == -90)
            {
                rotatePattern = E_RotatePattern.I_F;
            }
            else if (_nowRotate == -90 && nextRotate == 180)
            {
                rotatePattern = E_RotatePattern.I_G;
            }
            else if (_nowRotate == -90 && nextRotate == 0)
            {
                rotatePattern = E_RotatePattern.I_H;
            }
            else
            {
                Debug.LogError("想定外の回転");
            }
        }

        // 回転パターンに応じて判定

        // 試行回数の上限を設定
        int trialEnd = 4;

        // パターンを網羅するため繰り返し
        for (int trialCount = 0; trialCount <= trialEnd; trialCount++)
        {
            // 試行回数が上限になったか判定
            if (trialCount == trialEnd)
            {
                // 回転できなかった場合
                return;
            }

            // パターンごとに回転可能か判定
            int[] addPosition = GetPatternValue(rotatePattern, trialCount);
            if(CanMove(nextPosition, addPosition))
            {
                SetPattern(nextPosition, addPosition);
                _nowRotate = nextRotate;
                return;
            }
        }
    }

    private void SetPattern(float[,] nextPosition, int[] addPosition)
    {
        int moveRow = addPosition[MinoData.ROW_NUMBER];
        int moveColumn = addPosition[MinoData.COLUMN_NUMBER];

        _dropingminoPositions = nextPosition;

        MoveMino(moveRow, moveColumn);
        _iDropMinoLooksUpdata.DropMinoLooksUpdata(GetAllDropingMinoPosition());
    }

    private int[] GetPatternValue(E_RotatePattern rotatePattern, int trialCount)
    {
        int[] returnValue = new int[2];
        int row = default;
        int column = default;
        switch (rotatePattern)
        {
            case E_RotatePattern.Other_A:
                switch (trialCount)
                {
                    case 0:
                        row = 0;
                        column = 1;
                        break;

                    case 1:
                        row = 1;
                        column = 1;
                        break;

                    case 2:
                        row = -2;
                        column = 0;
                        break;

                    case 3:
                        row = -2;
                        column = 1;
                        break;

                    default:
                        // 例外処理
                        Debug.LogError("試行回数に異常あり");
                        break;
                }
                break;

            case E_RotatePattern.Other_B:
                switch (trialCount)
                {
                    case 0:
                        row = 0;
                        column = -1;
                        break;

                    case 1:
                        row = 1;
                        column = -1;
                        break;

                    case 2:
                        row = -2;
                        column = 0;
                        break;

                    case 3:
                        row = -2;
                        column = -1;
                        break;

                    default:
                        // 例外処理
                        Debug.LogError("試行回数に異常あり");
                        break;
                }
                break;

            case E_RotatePattern.Other_C:
                switch (trialCount)
                {
                    case 0:
                        row = 0;
                        column = 1;
                        break;

                    case 1:
                        row = -1;
                        column = 1;
                        break;

                    case 2:
                        row = 2;
                        column = 0;
                        break;

                    case 3:
                        row = 2;
                        column = 1;
                        break;

                    default:
                        // 例外処理
                        Debug.LogError("試行回数に異常あり");
                        break;
                }
                break;

            case E_RotatePattern.Other_D:
                switch (trialCount)
                {
                    case 0:
                        row = 0;
                        column = -1;
                        break;

                    case 1:
                        row = -1;
                        column = -1;
                        break;

                    case 2:
                        row = 2;
                        column = 0;
                        break;

                    case 3:
                        row = 2;
                        column = -1;
                        break;

                    default:
                        // 例外処理
                        Debug.LogError("試行回数に異常あり");
                        break;
                }
                break;

            case E_RotatePattern.T_A:
                switch (trialCount)
                {
                    case 0:
                        row = 0;
                        column = 1;
                        break;

                    case 1:
                        row = 1;
                        column = 1;
                        break;

                    case 2:
                        row = -2;
                        column = 0;
                        break;

                    case 3:
                        row = -2;
                        column = 1;
                        break;

                    default:
                        // 例外処理
                        Debug.LogError("試行回数に異常あり");
                        break;
                }
                break;

            case E_RotatePattern.T_B:
                switch (trialCount)
                {
                    case 0:
                        row = 0;
                        column = -1;
                        break;

                    case 1:
                        row = 1;
                        column = -1;
                        break;

                    case 2:
                        row = -2;
                        column = 0;
                        break;

                    case 3:
                        row = -2;
                        column = -1;
                        break;

                    default:
                        // 例外処理
                        Debug.LogError("試行回数に異常あり");
                        break;
                }
                break;

            case E_RotatePattern.T_C:
                switch (trialCount)
                {
                    case 0:
                        row = 0;
                        column = 1;
                        break;

                    case 1:
                        row = -1;
                        column = 1;
                        break;

                    case 2:
                        row = 2;
                        column = 0;
                        break;

                    case 3:
                        row = 2;
                        column = 1;
                        break;

                    default:
                        // 例外処理
                        Debug.LogError("試行回数に異常あり");
                        break;
                }
                break;

            case E_RotatePattern.T_D:
                switch (trialCount)
                {
                    case 0:
                        row = 0;
                        column = -1;
                        break;

                    case 1:
                        row = -1;
                        column = -1;
                        break;

                    case 2:
                        row = 2;
                        column = 0;
                        break;

                    case 3:
                        row = 2;
                        column = -1;
                        break;

                    default:
                        // 例外処理
                        Debug.LogError("試行回数に異常あり");
                        break;
                }
                break;

            case E_RotatePattern.T_E:
                switch (trialCount)
                {
                    case 0:
                        row = 0;
                        column = -2;
                        break;

                    case 1:
                        row = -1;
                        column = -2;
                        break;

                    case 2:
                        row = 2;
                        column = 0;
                        break;

                    case 3:
                        row = 2;
                        column = -1;
                        break;

                    default:
                        // 例外処理
                        Debug.LogError("試行回数に異常あり");
                        break;
                }
                break;

            case E_RotatePattern.I_A:
                switch (trialCount)
                {
                    case 0:
                        row = 0;
                        column = -1;
                        break;

                    case 1:
                        row = 0;
                        column = 2;
                        break;

                    case 2:
                        row = 2;
                        column = -1;
                        break;

                    case 3:
                        row = -1;
                        column = 2;
                        break;

                    default:
                        // 例外処理
                        Debug.LogError("試行回数に異常あり");
                        break;
                }
                break;

            case E_RotatePattern.I_B:
                switch (trialCount)
                {
                    case 0:
                        row = 0;
                        column = -2;
                        break;

                    case 1:
                        row = 0;
                        column = 1;
                        break;

                    case 2:
                        row = -1;
                        column = -2;
                        break;

                    case 3:
                        row = 2;
                        column = 1;
                        break;

                    default:
                        // 例外処理
                        Debug.LogError("試行回数に異常あり");
                        break;
                }
                break;

            case E_RotatePattern.I_C:
                switch (trialCount)
                {
                    case 0:
                        row = 0;
                        column = 2;
                        break;

                    case 1:
                        row = 0;
                        column = -1;
                        break;

                    case 2:
                        row = 1;
                        column = 2;
                        break;

                    case 3:
                        row = -2;
                        column = -1;
                        break;

                    default:
                        // 例外処理
                        Debug.LogError("試行回数に異常あり");
                        break;
                }
                break;

            case E_RotatePattern.I_D:
                switch (trialCount)
                {
                    case 0:
                        row = 0;
                        column = -1;
                        break;

                    case 1:
                        row = 0;
                        column = 2;
                        break;

                    case 2:
                        row = 2;
                        column = -1;
                        break;

                    case 3:
                        row = -1;
                        column = 2;
                        break;

                    default:
                        // 例外処理
                        Debug.LogError("試行回数に異常あり");
                        break;
                }
                break;

            case E_RotatePattern.I_E:
                switch (trialCount)
                {
                    case 0:
                        row = 0;
                        column = 1;
                        break;

                    case 1:
                        row = 0;
                        column = -2;
                        break;

                    case 2:
                        row = -2;
                        column = 1;
                        break;

                    case 3:
                        row = 1;
                        column = -2;
                        break;

                    default:
                        // 例外処理
                        Debug.LogError("試行回数に異常あり");
                        break;
                }
                break;

            case E_RotatePattern.I_F:
                switch (trialCount)
                {
                    case 0:
                        row = 0;
                        column = 2;
                        break;

                    case 1:
                        row = 0;
                        column = -1;
                        break;

                    case 2:
                        row = 1;
                        column = 2;
                        break;

                    case 3:
                        row = -2;
                        column = -1;
                        break;

                    default:
                        // 例外処理
                        Debug.LogError("試行回数に異常あり");
                        break;
                }
                break;

            case E_RotatePattern.I_G:
                switch (trialCount)
                {
                    case 0:
                        row = 0;
                        column = 1;
                        break;

                    case 1:
                        row = 0;
                        column = -2;
                        break;

                    case 2:
                        row = -1;
                        column = -2;
                        break;

                    case 3:
                        row = 2;
                        column = 1;
                        break;

                    default:
                        // 例外処理
                        Debug.LogError("試行回数に異常あり");
                        break;
                }
                break;

            case E_RotatePattern.I_H:
                switch (trialCount)
                {
                    case 0:
                        row = 0;
                        column = -2;
                        break;

                    case 1:
                        row = 0;
                        column = 1;
                        break;

                    case 2:
                        row = -2;
                        column = 1;
                        break;

                    case 3:
                        row = 1;
                        column = -2;
                        break;

                    default:
                        // 例外処理
                        Debug.LogError("試行回数に異常あり");
                        break;
                }
                break;
        }

        returnValue[0] = row;
        returnValue[1] = column;

        return returnValue;
    }


    private int GetDropingMinoColumn(int minoNumber)
    {
        return (int)(_dropingMinoOrigin[MinoData.COLUMN_NUMBER] + _dropingminoPositions[minoNumber, MinoData.COLUMN_NUMBER]);
    }

    private int GetDropingMinoColumn(float[,] positions ,int minoNumber)
    {
        return (int)(_dropingMinoOrigin[MinoData.COLUMN_NUMBER] + positions[minoNumber, MinoData.COLUMN_NUMBER]);
    }

    private int GetDropingMinoRow(int minoNumber)
    {
        return (int)(_dropingMinoOrigin[MinoData.ROW_NUMBER] + _dropingminoPositions[minoNumber, MinoData.ROW_NUMBER]);
    }

    private int GetDropingMinoRow(float[,] positions ,int minoNumber)
    {
        return (int)(_dropingMinoOrigin[MinoData.ROW_NUMBER] + positions[minoNumber, MinoData.ROW_NUMBER]);
    }

    public int[] GetDropingMinoPosition(int minoNumber)
    {
        int[] minoPosition = { GetDropingMinoColumn(minoNumber), GetDropingMinoRow(minoNumber) };
        return minoPosition;
    }

    private int[,] GetAllDropingMinoPosition()
    {
        int[,] minoPosition = new int[4, 2];
        for (int i = 0; i < 4; i++)
        {
            minoPosition[i, MinoData.COLUMN_NUMBER] = GetDropingMinoColumn(i);
            minoPosition[i, MinoData.ROW_NUMBER] = GetDropingMinoRow(i);
        }
        return minoPosition;
    }
}
