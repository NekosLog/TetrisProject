using System.Collections.Generic;
using UnityEngine;

public class DropingMino : MonoBehaviour, IFInputMainGame, IFDropMinoLooksData
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

    private float _nowRotate = default;
    private float _beforeRotate = default;

    private float _rightInterval = default;
    private float _leftInterval = default;
    private float _downInterval = default;

    private enum E_RotationVector { right = 0, left = 1 }
    private E_RotationVector _rotationRight = E_RotationVector.right;
    private E_RotationVector _rotationLeft = E_RotationVector.left;

    private readonly float[] ORIGIN_STRANGE = { 4.5f, 20.5f };
    private readonly float[] ORIGIN_NORMAL = {4f, 20f};

    private const float ROTATE_VALUE = 90;

    private const int MAX_ROW = 9;
    private const int MIN_ROW = 0;
    private const int MIN_COLUMN = 0;

    private readonly int[] up    = { 0, 1};
    private readonly int[] down  = { 0,-1};
    private readonly int[] right = { 1, 0};
    private readonly int[] left  = {-1, 0};


    private readonly float[,] INITIAL_POSITIONS_CYAN = 
    {
        {-1.5f,-0.5f},
        {-0.5f,-0.5f},
        {0.5f, -0.5f},
        {1.5f, -0.5f}
    };

    private readonly float[,] INITIAL_POSITIONS_PURPLE = 
    { 
        {-1f,0f},
        { 0f,0f},
        { 1f,0f},
        { 0f,1f} 
    };

    private readonly float[,] INITIAL_POSITIONS_RED =
    {
        {-1f,1f},
        { 0f,1f},
        { 0f,0f},
        { 1f,0f}
    };

    private readonly float[,] INITIAL_POSITIONS_GREEN =
    {
        {-1f,0f},
        { 0f,0f},
        { 0f,1f},
        { 1f,1f}
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
        {-1f,0f},
        { 0f,0f},
        { 1f,0f},
        { 0f,1f}
    };

    private readonly float[,] INITIAL_POSITIONS_BLUE =
    {
        {-1f,0f},
        { 0f,0f},
        { 1f,0f},
        { 0f,1f}
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
                    _iDropMinoLooksUpdata.DropMinoLooksUpdata(_dropingMinoColor);
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
        if(_dropingMinoColor != MinoData.E_MinoColor.empty || _rightInterval <= 0)
        {
            if (CanMove())
            {
                _dropingMinoOrigin = MoveRight(_dropingMinoOrigin);
                _rightInterval += _iGetKeyInterval.MinoMoveInterval();
                _iDropMinoLooksUpdata.DropMinoLooksUpdata(_dropingMinoColor);
            }
        }
    }

    public void InputHoldLeft()
    {
        if (_dropingMinoColor != MinoData.E_MinoColor.empty || _leftInterval <= 0)
        {
            if (CanMove())
            {
                _dropingMinoOrigin = MoveLeft(_dropingMinoOrigin);
                _leftInterval += _iGetKeyInterval.MinoMoveInterval();
                _iDropMinoLooksUpdata.DropMinoLooksUpdata(_dropingMinoColor);
            }
        }
    }

    public void InputDownUp()
    {

    }

    public void InputHoldDown()
    {
        if(_dropingMinoColor != MinoData.E_MinoColor.empty || _downInterval <= 0)
        {
            if (CanMove())
            {
                _minoFallTime = 0f;
                _dropingMinoOrigin = MoveLeft(_dropingMinoOrigin);
                _downInterval += _iGetKeyInterval.MinoMoveInterval();
                _iDropMinoLooksUpdata.DropMinoLooksUpdata(_dropingMinoColor);
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
        _beforeRotate = _nowRotate;
        _nowRotate -= ROTATE_VALUE;
        RotationMino(_rotationLeft);

        while (CheckStack())
        {
            PushUp();
        }
        _iDropMinoLooksUpdata.DropMinoLooksUpdata(_dropingMinoColor);
    }

    public void InputDownCancel()
    {
        _beforeRotate = _nowRotate;
        _nowRotate += ROTATE_VALUE;
        RotationMino(_rotationRight);
        while (CheckStack())
        {
            PushUp();
        }
        _iDropMinoLooksUpdata.DropMinoLooksUpdata(_dropingMinoColor);
    }

    #endregion

    public void DropStart()
    {
        print(_iDropMino);
        _dropingMinoColor = _iDropMino.GetDropMino();

        switch (_dropingMinoColor)
        {
            case MinoData.E_MinoColor.cyan:
                _dropingMinoOrigin = ORIGIN_STRANGE;
                _dropingminoPositions = INITIAL_POSITIONS_CYAN;
                break;


            case MinoData.E_MinoColor.purple:
                _dropingMinoOrigin = ORIGIN_NORMAL;
                _dropingminoPositions = INITIAL_POSITIONS_PURPLE;
                break;


            case MinoData.E_MinoColor.red:
                _dropingMinoOrigin = ORIGIN_NORMAL;
                _dropingminoPositions = INITIAL_POSITIONS_RED;
                break;


            case MinoData.E_MinoColor.green:
                _dropingMinoOrigin = ORIGIN_NORMAL;
                _dropingminoPositions = INITIAL_POSITIONS_GREEN;
                break;


            case MinoData.E_MinoColor.yellow:
                _dropingMinoOrigin = ORIGIN_STRANGE;
                _dropingminoPositions = INITIAL_POSITIONS_YELLOW;
                break;


            case MinoData.E_MinoColor.orange:
                _dropingMinoOrigin = ORIGIN_NORMAL;
                _dropingminoPositions = INITIAL_POSITIONS_ORANGE;
                break;


            case MinoData.E_MinoColor.blue:
                _dropingMinoOrigin = ORIGIN_NORMAL;
                _dropingminoPositions = INITIAL_POSITIONS_BLUE;
                break;
        }
    }

    private float[] MoveUp(float[] positionArray)
    {
        positionArray[1]++;
        return positionArray;
    }

    private float[] MoveDown(float[] positionArray)
    {
        positionArray[1]--;
        return positionArray;
    }

    private float[] MoveRight(float[] positionArray)
    {
        positionArray[0]++;
        return positionArray;
    }

    private float[] MoveLeft(float[] positionArray)
    {
        positionArray[0]--;
        return positionArray;
    }

    private void RotateReSet()
    {
        _nowRotate = 0f;
        _beforeRotate = 0f;
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
            int dropingMinoRow = GetDropingMinoRow(minoNumber) + addValue[1];
            int dropingMinoColumn = GetDropingMinoColumn(minoNumber) + addValue[0];

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
            return true;
        }

        return false;
    }

    private bool CheckOut(int row, int column)
    {
        if (row < MIN_ROW || row > MAX_ROW || column < MIN_COLUMN)
        {
            return true;
        }
        else
        {
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
            mino.Row = GetDropingMinoColumn(i);
            mino.Column = GetDropingMinoRow(i);
            list.Insert(0,mino);
        }

        return list;
    }

    private void ReFresh()
    {
        _dropingMinoColor = MinoData.E_MinoColor.empty;
        _dropingMinoOrigin = default;
        _dropingminoPositions = default;
        _downInterval = default;
        _minoFallTime = default;
        RotateReSet();
    }

    private void RotationMino(E_RotationVector rotationVector)
    {
        for(int i = 0; i < 4; i++)
        {
            float posX = _dropingminoPositions[i, 0];
            float posY = _dropingminoPositions[i, 1];

            switch (rotationVector)
            {
                case E_RotationVector.right:
                        _dropingminoPositions[i, 0] = posY;
                        _dropingminoPositions[i, 1] = -posX;
                    break;

                case E_RotationVector.left:
                        _dropingminoPositions[i, 0] = -posY;
                        _dropingminoPositions[i, 1] = posX;
                    break;
            }
        }
    }

    private void MoveUp(float[] positionArray)
    {
        ;
    }

    private void MoveDown()
    {

    }

    private void MoveRight()
    {

    }

    private void MoveLeft()
    {

    }

    private int GetDropingMinoRow(int minoNumber)
    {
        return (int)(_dropingMinoOrigin[0] + _dropingminoPositions[minoNumber, 0]);
    }

    private int GetDropingMinoColumn(int minoNumber)
    {
        return (int)(_dropingMinoOrigin[1] + _dropingminoPositions[minoNumber, 1]);
    }

    public int[] GetDropingMinoPosition(int minoNumber)
    {
        return new int[]{GetDropingMinoColumn(minoNumber), GetDropingMinoRow(minoNumber) };
    }

    public MinoData.E_MinoColor GetDropingMinoColor()
    {
        return _dropingMinoColor;
    }
}
