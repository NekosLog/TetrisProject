/* 制作日
*　製作者
*　最終更新日
*/

using UnityEngine;
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
}