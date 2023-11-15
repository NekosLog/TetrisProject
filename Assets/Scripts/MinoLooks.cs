using System;
using UnityEngine;

/// <summary>
/// ミノの表示を制御するクラス
/// </summary>
public class MinoLooks : MonoBehaviour,IFDropMinoLooksUpdata,IFStayMinoLooksUpdata
{
    #region 変数域

    #region クラス型変数
    // インターフェースのインスタンス用変数
    private IFGetMinoArray _iGetMinoArray = default;
    #endregion

    [SerializeField, Tooltip("ミノの表示用オブジェクト")]
    private GameObject _minoBlockObject = default;


    // ミノの行数
    private const int FIELD_ROW = 10;

    // ミノの列数
    private const int FIELD_COLUMN = 22;

    // 落ちてくるミノの個数
    private const int MINO_MAX_VALUE = 4;

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

        // ミノの位置を設定する原点の位置
        Vector2 _positionOrigin = new Vector2(-5f, -11f);

        // ミノの設置間隔
        const float POSITION_INTERVAL = 1f;

        // 表示用オブジェクトの生成
        for (int Row = 0; Row < FIELD_COLUMN; Row++)
        {
            for (int Column = 0; Column < FIELD_ROW; Column++)
            {
                blockPosition.x = _positionOrigin.x + (POSITION_INTERVAL * Column); // 列の位置設定
                blockPosition.y = _positionOrigin.y + (POSITION_INTERVAL * Row);    // 行の位置設定
                _minoBlockArray[Row, Column] = Instantiate(_minoBlockObject,blockPosition,Quaternion.identity);
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

        // 表示を更新
        DropUpdata(_dropingMinoColor);
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

            // 表示を更新
            DropUpdata(MinoData.E_MinoColor.empty);
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
        for (int i = 0; i < FIELD_COLUMN; i++)
        {
            for (int k = 0; k<FIELD_ROW; k++)
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
    /// 落下中のミノの色を設定するメソッド
    /// </summary>
    /// <param name="minoColor">設定する色</param>
    private void DropUpdata(MinoData.E_MinoColor minoColor)
    {
        // 行列指定用の作業変数
        int row = default;
        int column = default;

        // 各ブロックを更新
        for (int i = 0; i < MINO_MAX_VALUE; i++)
        {
            row = _nowDropingMinoPosition[i, MinoData.ROW];         // 行設定
            column = _nowDropingMinoPosition[i, MinoData.COLUMN];   // 列設定

            // 表示の更新
            ChengeBlockColor(_minoBlockArray[row, column], minoColor);
        }
    }

    /// <summary>
    /// 表示用オブジェクトを更新するメソッド
    /// </summary>
    /// <param name="block">更新する表示用オブジェクト</param>
    /// <param name="minoColor">更新する色</param>
    private void ChengeBlockColor(GameObject block,MinoData.E_MinoColor minoColor)
    {
        // 表示か非表示か判定
        if(minoColor != MinoData.E_MinoColor.empty)
        {
            // 色の設定
            block.GetComponent<Renderer>().material.color = ChengeColor(minoColor);
            // 表示する
            block.SetActive(true);
        }
        else
        {
            // 非表示にする
            block.SetActive(false);
        }
    }

    /// <summary>
    /// E_MinoColorをColorに変換する
    /// </summary>
    /// <param name="minoColor">変換するE_MinoColor</param>
    /// <returns>変換されたColor</returns>
    private Color ChengeColor(MinoData.E_MinoColor minoColor)
    {
        // 返り値用変数
        Color returnColor = default;

        // 色ごとに分岐
        switch (minoColor)
        {
            // 水色
            case MinoData.E_MinoColor.cyan:
                returnColor = _cyan;
                break;

            // 紫色
            case MinoData.E_MinoColor.purple:
                returnColor = _purple;
                break;

            // 赤色
            case MinoData.E_MinoColor.red:
                returnColor = _red;
                break;

            // 緑色
            case MinoData.E_MinoColor.green:
                returnColor = _green;
                break;

            // 黄色
            case MinoData.E_MinoColor.yellow:
                returnColor = _yellow;
                break;

            // オレンジ色
            case MinoData.E_MinoColor.orange:
                returnColor = _orange;
                break;

            // 青色
            case MinoData.E_MinoColor.blue:
                returnColor = _blue;
                break;
        }
        //色を返す
        return returnColor;
    }
}
