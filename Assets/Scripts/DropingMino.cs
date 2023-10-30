using System.Collections.Generic;
using UnityEngine;

public class DropingMino : MonoBehaviour, IFInputMainGame
{
    private IFDropMino _iDropMino;
    private IFGetKeyInterval _iGetKeyInterval;
    private IFGetMinoArray _iGetMinoArray;
    private IFLandingMinos _iLandingMinos;

    private MinoData.E_MinoColor _dropingMinoColor;
    private Vector2 _dropingMinoOrigin;
    private float[,] _dropingminoPositions = new float[4,2];
    private List<MinoData.minoState> _landingMinoList;
    private float _minoFallInterval = 1f;
    private float _minoFallTime = 0f;

    private float _nowRotate = default;
    private float _beforeRotate = default;

    private float _rightInterval = default;
    private float _leftInterval = default;
    private float _downInterval = default;

    private readonly Vector2 MOVE_RIGHT = Vector2.right;
    private readonly Vector2 MOVE_LEFT = Vector2.left;
    private readonly Vector2 MOVE_DOWN = Vector2.down;

    private readonly Vector2 ORIGIN_STRANGE = new Vector2(4.5f, 20.5f);
    private readonly Vector2 ORIGIN_NORMAL = new Vector2(4f, 20f);

    private const float ROTATE_VALUE = 90;


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

        if(_dropingMinoColor != MinoData.E_MinoColor.empty)
        {
            _minoFallTime += Time.deltaTime;
            if(_minoFallTime > _minoFallInterval)
            {
                _minoFallTime = 0f;
                _dropingMinoOrigin += MOVE_DOWN;
                if (CheckStack())
                {
                    _dropingMinoOrigin -= MOVE_DOWN;
                    _iLandingMinos.LandingMinos(LandingMinoList());
                }
            }
        }
    }

    #region InputŠÖŒW
    public void RightKeyHold()
    {
        if(_rightInterval <= 0)
        {
            _dropingMinoOrigin += MOVE_RIGHT;
            _rightInterval += _iGetKeyInterval.MinoMoveInterval();
            if (CheckStack())
            {
                _dropingMinoOrigin -= MOVE_RIGHT;
            }
        }
    }

    public void LeftKeyHold()
    {
        if (_leftInterval <= 0)
        {
            _dropingMinoOrigin += MOVE_LEFT;
            _leftInterval += _iGetKeyInterval.MinoMoveInterval();
            if (CheckStack())
            {
                _dropingMinoOrigin -= MOVE_LEFT;
            }
        }
    }

    public void UpKeyDown()
    {

    }

    public void DownKeyHold()
    {
        if(_downInterval <= 0)
        {
            _minoFallTime = 0f;
            _dropingMinoOrigin += MOVE_DOWN;
            _downInterval += _iGetKeyInterval.MinoMoveInterval();
            if (CheckStack())
            {
                _dropingMinoOrigin -= MOVE_DOWN;
                _iLandingMinos.LandingMinos(LandingMinoList());
            }
        }
    }

    public void DecisionKeyDown()
    {
        _beforeRotate = _nowRotate;
        _nowRotate -= ROTATE_VALUE;
    }

    public void CancelKeyDown()
    {
        _beforeRotate = _nowRotate;
        _nowRotate += ROTATE_VALUE;
    }

    #endregion

    public void DropStart()
    {
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

    private void RotateReSet()
    {
        _nowRotate = 0f;
        _beforeRotate = 0f;
    }

    private bool CheckStack()
    {
        for(int position_x = 0; position_x < 4; position_x++)
        {
            if(_iGetMinoArray.GetMinoArray()[(int)(_dropingMinoOrigin.x + _dropingminoPositions[position_x,0]),(int)(_dropingMinoOrigin.y + _dropingminoPositions[position_x,1])] != MinoData.E_MinoColor.empty)
            {
                return true;
            }
        }
        return false;
    }


    private List<MinoData.minoState> LandingMinoList()
    {
        List<MinoData.minoState> list = default;
        MinoData.minoState mino = default;

        mino.minoColor = _dropingMinoColor;
        for (int i = 0; i < 4; i++)
        {
            mino.Row = (int)(_dropingMinoOrigin.x + _dropingminoPositions[i, 0]);
            mino.Column = (int)(_dropingMinoOrigin.y + _dropingminoPositions[i, 1]);
            list.Add(mino);
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
}
