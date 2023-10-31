using UnityEngine;

public class MinoLooks : MonoBehaviour,IFDropMinoLooksUpdata,IFStayMinoLooksUpdata
{
    IFDropMinoLooksData _iDropMinoLooksData;
    IFGetMinoArray _iGetMinoArray;

    private const int ARRAY_ROW = 10;
    private const int ARRAY_COLUMN = 22;
    private const int MINO_MAXVALUE = 4;

    [SerializeField]
    GameObject _minoBlockObject;

    GameObject[,] _minoBlockArray = new GameObject[22, 10];

    private MinoData.E_MinoColor[,] _nowMinoColor = new MinoData.E_MinoColor[22, 10];

    private int[,] _lastSetDropPosition = new int[4, 2];

    private float[] _positionHeight = { -11,-10,-9,-8,-7,-6,-5,-4,-3,-2,-1,0,1,2,3,4,5,6,7,8,9,10};

    private float[] _positionWidth = { -5,-4,-3,-2,-1,0,1,2,3,4};

    private readonly Color _cyan = Color.cyan;
    private readonly Color _purple = new Color(1,0,1);
    private readonly Color _red = Color.red;
    private readonly Color _green = Color.green;
    private readonly Color _yellow = Color.yellow;
    private readonly Color _orange = new Color(1,1,0);
    private readonly Color _blue = Color.blue;

    private void Awake()
    {
        _iDropMinoLooksData = GameObject.FindWithTag("GameManager").GetComponent<IFDropMinoLooksData>();
        _iGetMinoArray = GameObject.FindWithTag("GameManager").GetComponent<IFGetMinoArray>();

        Vector3 blockPosition = default;
        for (int i = 0; i < ARRAY_COLUMN;i++)
        {
            for (int k = 0; k < ARRAY_ROW;k++)
            {
                blockPosition.x = _positionWidth[k];
                blockPosition.y = _positionHeight[i];
                _minoBlockArray[i, k] = Instantiate(_minoBlockObject,blockPosition,Quaternion.identity);
            }
        }
    }

    public void DropMinoLooksUpdata(MinoData.E_MinoColor minoColor)
    {
        DeleteLastDrop();
        for (int i = 0; i < MINO_MAXVALUE; i++)
        {
            int[] dropPosition = _iDropMinoLooksData.GetDropingMinoPosition(i);
            _lastSetDropPosition[i,0] = dropPosition[0];
            _lastSetDropPosition[i,1] = dropPosition[1];
            ChengeBlockColor(_minoBlockArray[dropPosition[0], dropPosition[1]], minoColor);
        }
    }

    public void DeleteLastDrop()
    {
        if (_lastSetDropPosition != null)
        {
            for (int i = 0; i < MINO_MAXVALUE; i++)
            {
                ChengeBlockColor(_minoBlockArray[_lastSetDropPosition[i, 0],_lastSetDropPosition[i, 1]], MinoData.E_MinoColor.empty);
            }
        }
    }

    public void StayMinoLooksUpdata()
    {
        for (int i = 0; i < ARRAY_COLUMN; i++)
        {
            for (int k = 0; k<ARRAY_ROW; k++)
            { 
                if(_nowMinoColor[i,k] != _iGetMinoArray.GetMinoArray()[i, k])
                {
                    _nowMinoColor[i, k] = _iGetMinoArray.GetMinoArray()[i, k];
                    ChengeBlockColor(_minoBlockArray[i, k], _iGetMinoArray.GetMinoArray()[i, k]);
                }
            }
        }
        _lastSetDropPosition = null;
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
