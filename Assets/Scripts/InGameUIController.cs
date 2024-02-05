/* 制作日
*　製作者
*　最終更新日
*/

using UnityEngine;
using UnityEngine.UI;
using TMPro;
 
public class InGameUIController : MonoBehaviour, IFUIController {

    [SerializeField, Tooltip("タイマーのTextMeshPro")]
    private TextMeshProUGUI _timerText = default;

    [SerializeField, Tooltip("レベルのTextMeshPro")]
    private TextMeshProUGUI _levelText = default;

    [SerializeField, Tooltip("スコアのTextMeshPro")]
    private TextMeshProUGUI _scoreText = default;

    [SerializeField, Tooltip("OptionData")]
    private OptionData _optionData = default;

    [SerializeField, Tooltip("ホールドしているミノ")]
    private Image _holdingMino = default;

    [SerializeField, Tooltip("待機ミノ１番目")]
    private Image _nextMino1 = default;

    [SerializeField, Tooltip("待機ミノ２番目")]
    private Image _nextMino2 = default;

    [SerializeField, Tooltip("待機ミノ３番目")]
    private Image _nextMino3 = default;

    [SerializeField, Tooltip("Iミノ")]
    private Sprite _I_Mino = default;

    [SerializeField, Tooltip("Oミノ")]
    private Sprite _O_Mino = default;

    [SerializeField, Tooltip("Tミノ")]
    private Sprite _T_Mino = default;

    [SerializeField, Tooltip("Zミノ")]
    private Sprite _Z_Mino = default;

    [SerializeField, Tooltip("逆Zミノ")]
    private Sprite _revZ_Mino = default;

    [SerializeField, Tooltip("Lミノ")]
    private Sprite _L_Mino = default;

    [SerializeField, Tooltip("逆Lミノ")]
    private Sprite _revL_Mino = default;

    private float _timer = default;

    private int _nowSeconds_one = default;
    private int _nowSeconds_ten = default;
    private int _nowMinutes_one = default;
    private int _nowMinutes_ten = default;
    private int _nowLevel = 1;
    private int _nowScore = 0;

    private string _nowTimeText = default;

    private void Update()
    {
        // 時間をカウント
        _timer += Time.deltaTime;

        // 各桁を加算
        if (_timer > 1)
        {
            // １秒加算
            _timer--;
            _nowSeconds_one++;

            // １０秒の桁加算
            if (_nowSeconds_one == 10)
            {
                _nowSeconds_ten++;
                _nowSeconds_one = _nowSeconds_one - 10;
                _nowLevel++;
                _optionData.LevelUp(_nowLevel);
                _levelText.text = _nowLevel.ToString();

                // １分の桁加算
                if (_nowSeconds_ten == 6)
                {
                    _nowMinutes_one++;
                    _nowSeconds_ten = _nowSeconds_ten - 6;

                    // １０分の桁加算
                    if (_nowMinutes_one == 10)
                    {
                        _nowMinutes_ten++;
                        _nowMinutes_one = _nowMinutes_one - 10;
                    }
                }
            }

            _nowTimeText = _nowMinutes_ten.ToString() + _nowMinutes_one.ToString() + ":" + _nowSeconds_ten.ToString() + _nowSeconds_one.ToString();

            _timerText.text = _nowTimeText;
        }
    }

    public void AddScore(int score)
    {
        _nowScore += score;
        _scoreText.text = _nowScore.ToString();
    }

    public void ChengeHold(MinoData.E_MinoColor minoColor)
    {
        switch (minoColor)
        {
            case MinoData.E_MinoColor.cyan:
                _holdingMino.sprite = _I_Mino;
                break;
            case MinoData.E_MinoColor.yellow:
                _holdingMino.sprite = _O_Mino;
                break;
            case MinoData.E_MinoColor.purple:
                _holdingMino.sprite = _T_Mino;
                break;
            case MinoData.E_MinoColor.red:
                _holdingMino.sprite = _Z_Mino;
                break;
            case MinoData.E_MinoColor.green:
                _holdingMino.sprite = _revZ_Mino;
                break;
            case MinoData.E_MinoColor.orange:
                _holdingMino.sprite = _L_Mino;
                break;
            case MinoData.E_MinoColor.blue:
                _holdingMino.sprite = _revL_Mino;
                break;
        }
    }

    public void ChengeNext1(MinoData.E_MinoColor minoColor)
    {
        switch (minoColor)
        {
            case MinoData.E_MinoColor.cyan:
                _nextMino1.sprite = _I_Mino;
                break;
            case MinoData.E_MinoColor.yellow:
                _nextMino1.sprite = _O_Mino;
                break;
            case MinoData.E_MinoColor.purple:
                _nextMino1.sprite = _T_Mino;
                break;
            case MinoData.E_MinoColor.red:
                _nextMino1.sprite = _Z_Mino;
                break;
            case MinoData.E_MinoColor.green:
                _nextMino1.sprite = _revZ_Mino;
                break;
            case MinoData.E_MinoColor.orange:
                _nextMino1.sprite = _L_Mino;
                break;
            case MinoData.E_MinoColor.blue:
                _nextMino1.sprite = _revL_Mino;
                break;
        }
    }

    public void ChengeNext2(MinoData.E_MinoColor minoColor)
    {
        switch (minoColor)
        {
            case MinoData.E_MinoColor.cyan:
                _nextMino2.sprite = _I_Mino;
                break;
            case MinoData.E_MinoColor.yellow:
                _nextMino2.sprite = _O_Mino;
                break;
            case MinoData.E_MinoColor.purple:
                _nextMino2.sprite = _T_Mino;
                break;
            case MinoData.E_MinoColor.red:
                _nextMino2.sprite = _Z_Mino;
                break;
            case MinoData.E_MinoColor.green:
                _nextMino2.sprite = _revZ_Mino;
                break;
            case MinoData.E_MinoColor.orange:
                _nextMino2.sprite = _L_Mino;
                break;
            case MinoData.E_MinoColor.blue:
                _nextMino2.sprite = _revL_Mino;
                break;
        }
    }

    public void ChengeNext3(MinoData.E_MinoColor minoColor)
    {
        switch (minoColor)
        {
            case MinoData.E_MinoColor.cyan:
                _nextMino3.sprite = _I_Mino;
                break;
            case MinoData.E_MinoColor.yellow:
                _nextMino3.sprite = _O_Mino;
                break;
            case MinoData.E_MinoColor.purple:
                _nextMino3.sprite = _T_Mino;
                break;
            case MinoData.E_MinoColor.red:
                _nextMino3.sprite = _Z_Mino;
                break;
            case MinoData.E_MinoColor.green:
                _nextMino3.sprite = _revZ_Mino;
                break;
            case MinoData.E_MinoColor.orange:
                _nextMino3.sprite = _L_Mino;
                break;
            case MinoData.E_MinoColor.blue:
                _nextMino3.sprite = _revL_Mino;
                break;
        }
    }
}