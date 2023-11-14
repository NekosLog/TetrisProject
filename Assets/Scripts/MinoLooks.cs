using System;
using UnityEngine;

/// <summary>
/// ミノの表示を制御するクラス
/// </summary>
public class MinoLooks : MonoBehaviour,IFDropMinoLooksUpdata,IFStayMinoLooksUpdata
{
    #region 変数域

    [SerializeField, Tooltip("ミノの表示用オブジェクト")]
    private GameObject _minoBlockObject = default;

    // インターフェースのインスタンス用変数
    IFGetMinoArray _iGetMinoArray = default;

    // ミノの列数
    private const int ARRAY_ROW = 10;

    // ミノの行数
    private const int ARRAY_COLUMN = 22;

    // 落ちてくるミノの個数
    private const int MINO_MAXVALUE = 4;

    // 落下中のミノが存在しないときに代入する値
    private const int ENPTY_VALUE = -99;

    // ミノの表示用オブジェクト配列　それぞれに_minoBlockObjectを格納する
    private GameObject[,] _minoBlockArray = new GameObject[22, 10];

    // ミノの現在色の保存用配列　各位置ごとの色を保存
    private MinoData.E_MinoColor[,] _nowMinoColor = new MinoData.E_MinoColor[22, 10];

    // 落下中のミノの色　落下中のミノを表示するために使用
    private MinoData.E_MinoColor _dropingMinoColor = MinoData.E_MinoColor.empty;

    // 現在落下中のミノの位置
    private int[,] _nowDropingMinoPosition = new int[4, 2];

    // ミノの各行の位置　高さを指定するために使用
    private float[] _positionHeight = 
        { -11, -10, -9, -8, -7, -6, -5, -4, -3, -2, -1,
            0,   1,  2,  3,  4,  5,  6,  7,  8,  9, 10};

    // ミノの各列の位置　左右の位置を指定するために使用
    private float[] _positionWidth = { -5,-4,-3,-2,-1,0,1,2,3,4 };

    // ミノの色 -------------------------------------------
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
        /* 初期設定 */

        // インターフェースのインスタンス
        _iGetMinoArray = GameObject.FindWithTag("GameManager").GetComponent<IFGetMinoArray>();


        // 生成位置
        Vector3 blockPosition = default;

        // 表示用オブジェクトの生成
        for (int Column = 0; Column < ARRAY_COLUMN;Column++)
        {
            for (int Row = 0; Row < ARRAY_ROW;Row++)
            {
                blockPosition.x = _positionWidth[Row];      // 列の位置設定
                blockPosition.y = _positionHeight[Column];  // 行の位置設定
                _minoBlockArray[Column, Row] = Instantiate(_minoBlockObject,blockPosition,Quaternion.identity);
            }
        }
    }

    /// <summary>
    /// 落下中のミノの表示を更新するメソッド
    /// </summary>
    /// <param name="dropingMinoPosition">落下中のミノの各ブロック毎の位置</param>
    public void DropMinoLooksUpdata(int[,] dropingMinoPosition)
    {
        // 前回の表示を削除
        DeleteLastDrop();

        // 落下中のミノのブロックの個数
        int positionLength = dropingMinoPosition.GetLength(0) * dropingMinoPosition.GetLength(1);

        // 落下中のミノの位置を更新
        Array.Copy(dropingMinoPosition, _nowDropingMinoPosition, positionLength);

        // 作業用の行列変数
        int row = default;
        int column = default;

        // 表示を更新
        for (int i = 0; i < MINO_MAXVALUE; i++)
        {
            row = _nowDropingMinoPosition[i,MinoData.ROW];          // 列設定
            column = _nowDropingMinoPosition[i,MinoData.COLUMN];    // 行設定

            // 表示の更新
            ChengeBlockColor(_minoBlockArray[column, row], _dropingMinoColor);
        }
    }

    /// <summary>
    /// 落下中のミノの色を設定するメソッド
    /// </summary>
    /// <param name="minoColor">設定するミノの色</param>
    public void SetDropingMinoColor(MinoData.E_MinoColor minoColor)
    {
        // 落下中のミノの色を設定
        _dropingMinoColor = minoColor;
    }

    /// <summary>
    /// 前回の落下中のミノの表示を削除するメソッド
    /// </summary>
    public void DeleteLastDrop()
    {
        // 落下中のミノが存在する場合のみ実行
        if (_nowDropingMinoPosition[0,0] != ENPTY_VALUE)
        {
            // ミノが存在する場合

            // 作業用の行列変数
            int row = default;
            int column = default;

            for (int i = 0; i < MINO_MAXVALUE; i++)
            {
                row = _nowDropingMinoPosition[i, MinoData.ROW];         // 列設定
                column = _nowDropingMinoPosition[i, MinoData.COLUMN];   // 行設定

                // 表示の更新
                ChengeBlockColor(_minoBlockArray[column,row], MinoData.E_MinoColor.empty);
            }
        }
        else
        {
            // ミノが存在しない場合
            return;
        }
    }

    /// <summary>
    /// 停止中のミノの表示を更新するメソッド
    /// </summary>
    public void StayMinoLooksUpdata()
    {
        // 停止中のミノの配列を一時保管する変数
        MinoData.E_MinoColor[,] staticMinoArray = _iGetMinoArray.GetMinoArray();

        // 各ブロックを更新
        for (int i = 0; i < ARRAY_COLUMN; i++)
        {
            for (int k = 0; k<ARRAY_ROW; k++)
            {
                // 表示に変更がある場合のみ更新
                if(_nowMinoColor[i,k] != staticMinoArray[i, k])
                {
                    // 色の設定
                    _nowMinoColor[i, k] = staticMinoArray[i, k];
                    // 表示の更新
                    ChengeBlockColor(_minoBlockArray[i, k], staticMinoArray[i, k]);
                }
            }
        }
        // 落下中のミノを削除
        _nowDropingMinoPosition[0, 0] = ENPTY_VALUE;
    }

    /// <summary>
    /// 表示用オブジェクトを更新するメソッド
    /// </summary>
    /// <param name="block">更新する表示用オブジェクト</param>
    /// <param name="minoColor">更新する色</param>
    private void ChengeBlockColor(GameObject block,MinoData.E_MinoColor minoColor)
    {
        // 色ごとに分岐
        switch (minoColor)
        {
            // 水色
            case MinoData.E_MinoColor.cyan:
                block.GetComponent<Renderer>().material.color = _cyan;
                block.SetActive(true);
                break;

            // 紫色
            case MinoData.E_MinoColor.purple:
                block.GetComponent<Renderer>().material.color = _purple;
                block.SetActive(true);
                break;

            // 赤色
            case MinoData.E_MinoColor.red:
                block.GetComponent<Renderer>().material.color = _red;
                block.SetActive(true);
                break;

            // 緑色
            case MinoData.E_MinoColor.green:
                block.GetComponent<Renderer>().material.color = _green;
                block.SetActive(true);
                break;

            // 黄色
            case MinoData.E_MinoColor.yellow:
                block.GetComponent<Renderer>().material.color = _yellow;
                block.SetActive(true);
                break;

            // オレンジ色
            case MinoData.E_MinoColor.orange:
                block.GetComponent<Renderer>().material.color = _orange;
                block.SetActive(true);
                break;

            // 青色
            case MinoData.E_MinoColor.blue:
                block.GetComponent<Renderer>().material.color = _blue;
                block.SetActive(true);
                break;

            // 非表示
            case MinoData.E_MinoColor.empty:
                block.SetActive(false);
                break;
        }
    }
}
