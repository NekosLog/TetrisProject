using System.Collections.Generic;
using UnityEngine;

public class DropingMino : MonoBehaviour, IFInputMainGame
{
    #region ïœêî
    private IFDropMino _iDropMino;
    private IFGetKeyInterval _iGetKeyInterval;
    private IFGetMinoArray _iGetMinoArray;
    private IFLandingMinos _iLandingMinos;
    private IFDropMinoLooksUpdata _iDropMinoLooksUpdata;

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

    private const float ORIGIN_STRANGE_COLUMN = 20.5f;
    private const float ORIGIN_STRANGE_ROW = 4.5f;

    private const float ORIGIN_NORMAL_COLUMN = 20f;
    private const float ORIGIN_NORMAL_ROW = 4f;

    private const int ROTATE_VALUE = 90;

    private const int MAX_ROW = 9;
    private const int MIN_ROW = 0;
    private const int MIN_COLUMN = 0;

    private readonly int[] up    = { 1, 0};
    private readonly int[] down  = {-1, 0};
    private readonly int[] right = { 0, 1};
    private readonly int[] left  = { 0,-1};


    private readonly float[,] INITIAL_POSITIONS_CYAN = 
    {
        {-0.5f,-1.5f},
        {-0.5f,-0.5f},
        {-0.5f,0.5f},
        {-0.5f,1.5f}
    };

    private readonly float[,] INITIAL_POSITIONS_PURPLE = 
    { 
        {0f,-1f},
        {0f, 0f},
        {0f, 1f},
        {0f, 0f} 
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
        _iGetKeyInterval = GameObject.FindWithTag("GameManager").GetComponent<IFGetKeyInterval>();
        _iGetMinoArray = GameObject.FindWithTag("GameManager").GetComponent<IFGetMinoArray>();
        _iLandingMinos = GameObject.FindWithTag("GameManager").GetComponent<IFLandingMinos>();
        _iDropMinoLooksUpdata = GameObject.FindWithTag("GameManager").GetComponent<IFDropMinoLooksUpdata>();
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
            if(_minoFallTime > _minoFallInterval)
            {
                _minoFallTime = 0f;
                if (CanMove(down))
                {
                    _dropingMinoOrigin = MoveDown(_dropingMinoOrigin);
                    _iDropMinoLooksUpdata.DropMinoLooksUpdata(GetAllDropingMinoPosition());
                }
                else
                {
                    _iLandingMinos.LandingMinos(LandingMinoList());
                    ReFresh();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            DropStart();
        }
    }

    #region Inputä÷åW
    public void InputHoldRight()
    {
        if(_dropingMinoColor != MinoData.E_MinoColor.empty && _rightInterval <= 0)
        {
            if (CanMove(right))
            {
                _dropingMinoOrigin = MoveRight(_dropingMinoOrigin);
                _rightInterval = _iGetKeyInterval.MinoMoveInterval();
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
                _dropingMinoOrigin = MoveLeft(_dropingMinoOrigin);
                _leftInterval = _iGetKeyInterval.MinoMoveInterval();
                _iDropMinoLooksUpdata.DropMinoLooksUpdata(GetAllDropingMinoPosition());
            }
        }
    }

    public void InputDownUp()
    {

    }

    public void InputHoldDown()
    {
        if(_dropingMinoColor != MinoData.E_MinoColor.empty && _downInterval <= 0)
        {
            if (CanMove(down))
            {
                _minoFallTime = 0f;
                _dropingMinoOrigin = MoveDown(_dropingMinoOrigin);
                _downInterval = _iGetKeyInterval.MinoMoveInterval();
                _iDropMinoLooksUpdata.DropMinoLooksUpdata(GetAllDropingMinoPosition());
            }
            else
            {
                _iLandingMinos.LandingMinos(LandingMinoList());
                ReFresh();
            }
        }
    }

    public void InputDownDecision()
    {
        int nextRotate = _nowRotate - ROTATE_VALUE;
        float[,] nextPositions = RotationMino(_dropingminoPositions,_rotationLeft);

        if (CanMove(nextPositions))
        {
            _nowRotate = nextRotate;
            _dropingminoPositions = nextPositions;
            _iDropMinoLooksUpdata.DropMinoLooksUpdata(GetAllDropingMinoPosition());
        }
        else
        {

        }
    }

    public void InputDownCancel()
    {
        int nextRotate = _nowRotate + ROTATE_VALUE;
        float[,] nextPositions = RotationMino(_dropingminoPositions, _rotationLeft);

        if (CanMove(nextPositions))
        {
            _nowRotate = nextRotate;
            _dropingminoPositions = nextPositions;
            _iDropMinoLooksUpdata.DropMinoLooksUpdata(GetAllDropingMinoPosition());
        }
        else
        {

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
        _dropingMinoColor = _iDropMino.GetDropMino();
        print("Now  " + _dropingMinoColor) ;

        switch (_dropingMinoColor)
        {
            case MinoData.E_MinoColor.cyan:
                _dropingMinoOrigin[MinoData.COLUMN] = ORIGIN_STRANGE_COLUMN;
                _dropingMinoOrigin[MinoData.ROW] = ORIGIN_STRANGE_ROW;
                _dropingminoPositions = INITIAL_POSITIONS_CYAN;
                _iDropMinoLooksUpdata.DropMinoLooksUpdata(GetAllDropingMinoPosition());
                print("column = " + _dropingMinoOrigin[0] + "      row = " + _dropingMinoOrigin[1]);
                break;


            case MinoData.E_MinoColor.purple:
                _dropingMinoOrigin[MinoData.COLUMN] = ORIGIN_NORMAL_COLUMN;
                _dropingMinoOrigin[MinoData.ROW] = ORIGIN_NORMAL_ROW;
                _dropingminoPositions = INITIAL_POSITIONS_PURPLE;
                _iDropMinoLooksUpdata.DropMinoLooksUpdata(GetAllDropingMinoPosition());
                print("column = " + _dropingMinoOrigin[0] + "      row = " + _dropingMinoOrigin[1]);
                break;


            case MinoData.E_MinoColor.red:
                _dropingMinoOrigin[MinoData.COLUMN] = ORIGIN_NORMAL_COLUMN;
                _dropingMinoOrigin[MinoData.ROW] = ORIGIN_NORMAL_ROW;
                _dropingminoPositions = INITIAL_POSITIONS_RED;
                _iDropMinoLooksUpdata.DropMinoLooksUpdata(GetAllDropingMinoPosition());
                print("column = " + _dropingMinoOrigin[0] + "      row = " + _dropingMinoOrigin[1]);
                break;


            case MinoData.E_MinoColor.green:
                _dropingMinoOrigin[MinoData.COLUMN] = ORIGIN_NORMAL_COLUMN;
                _dropingMinoOrigin[MinoData.ROW] = ORIGIN_NORMAL_ROW;
                _dropingminoPositions = INITIAL_POSITIONS_GREEN;
                _iDropMinoLooksUpdata.DropMinoLooksUpdata(GetAllDropingMinoPosition());
                print("column = " + _dropingMinoOrigin[0] + "      row = " + _dropingMinoOrigin[1]);
                break;


            case MinoData.E_MinoColor.yellow:
                _dropingMinoOrigin[MinoData.COLUMN] = ORIGIN_STRANGE_COLUMN;
                _dropingMinoOrigin[MinoData.ROW] = ORIGIN_STRANGE_ROW;
                _dropingminoPositions = INITIAL_POSITIONS_YELLOW;
                _iDropMinoLooksUpdata.DropMinoLooksUpdata(GetAllDropingMinoPosition());
                print("column = " + _dropingMinoOrigin[0] + "      row = " + _dropingMinoOrigin[1]);
                break;


            case MinoData.E_MinoColor.orange:
                _dropingMinoOrigin[MinoData.COLUMN] = ORIGIN_NORMAL_COLUMN;
                _dropingMinoOrigin[MinoData.ROW] = ORIGIN_NORMAL_ROW;
                _dropingminoPositions = INITIAL_POSITIONS_ORANGE;
                _iDropMinoLooksUpdata.DropMinoLooksUpdata(GetAllDropingMinoPosition());
                print("column = " + _dropingMinoOrigin[0] + "      row = " + _dropingMinoOrigin[1]);
                break;


            case MinoData.E_MinoColor.blue:
                _dropingMinoOrigin[MinoData.COLUMN] = ORIGIN_NORMAL_COLUMN;
                _dropingMinoOrigin[MinoData.ROW] = ORIGIN_NORMAL_ROW;
                _dropingminoPositions = INITIAL_POSITIONS_BLUE;
                _iDropMinoLooksUpdata.DropMinoLooksUpdata(GetAllDropingMinoPosition());
                print("column = " + _dropingMinoOrigin[0] + "      row = " + _dropingMinoOrigin[1]);
                break;
        }
    }

    private float[] MoveUp(float[] positionArray)
    {
        positionArray[MinoData.COLUMN]++;
        return positionArray;
    }

    private float[] MoveDown(float[] positionArray)
    {
        positionArray[MinoData.COLUMN]--;
        return positionArray;
    }

    private float[] MoveRight(float[] positionArray)
    {
        positionArray[MinoData.ROW]++;
        return positionArray;
    }

    private float[] MoveLeft(float[] positionArray)
    {
        positionArray[MinoData.ROW]--;
        return positionArray;
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
            int dropingMinoRow = GetDropingMinoRow(minoNumber) + addValue[MinoData.ROW];
            int dropingMinoColumn = GetDropingMinoColumn(minoNumber) + addValue[MinoData.COLUMN];

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
            int dropingMinoRow = GetDropingMinoRow(minoNumber) + addValue[MinoData.ROW];
            int dropingMinoColumn = GetDropingMinoColumn(minoNumber) + addValue[MinoData.COLUMN];

            if (CheckOut(dropingMinoRow, dropingMinoColumn) || CheckStack(dropingMinoRow, dropingMinoColumn))
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckStack(int row, int column)
    {
        if(_iGetMinoArray.GetMinoArray()[column ,row]!= MinoData.E_MinoColor.empty)
        {
            print("stack");
            return true;
        }
        print("noStack");
        return false;
    }

    private bool CheckOut(int row, int column)
    {
        if (row < MIN_ROW || row > MAX_ROW || column < MIN_COLUMN)
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

    private void ReFresh()
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
            float posX = positions[i, MinoData.COLUMN];
            float posY = positions[i, MinoData.ROW];

            switch (rotationVector)
            {
                case E_RotationVector.right:
                    positions[i, MinoData.COLUMN] = posY;
                    positions[i, MinoData.ROW] = -posX;
                    break;

                case E_RotationVector.left:
                    positions[i, MinoData.COLUMN] = -posY;
                    positions[i, MinoData.ROW] = posX;
                    break;
            }
        }
        return positions;
    }

    private int GetDropingMinoColumn(int minoNumber)
    {
        return (int)(_dropingMinoOrigin[MinoData.COLUMN] + _dropingminoPositions[minoNumber, MinoData.COLUMN]);
    }

    private int GetDropingMinoColumn(float[,] positions ,int minoNumber)
    {
        return (int)(_dropingMinoOrigin[MinoData.COLUMN] + positions[minoNumber, MinoData.COLUMN]);
    }

    private int GetDropingMinoRow(int minoNumber)
    {
        return (int)(_dropingMinoOrigin[MinoData.ROW] + _dropingminoPositions[minoNumber, MinoData.ROW]);
    }

    private int GetDropingMinoRow(float[,] positions ,int minoNumber)
    {
        return (int)(_dropingMinoOrigin[MinoData.ROW] + positions[minoNumber, MinoData.ROW]);
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
            minoPosition[i, MinoData.COLUMN] = GetDropingMinoColumn(i);
            minoPosition[i, MinoData.ROW] = GetDropingMinoRow(i);
        }
        return minoPosition;
    }

}
