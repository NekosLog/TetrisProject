using UnityEngine;

public class SEData : MonoBehaviour
{
    [SerializeField, Tooltip("SE用のスピーカ")]
    public AudioSource _speaker = default;
    [SerializeField, Tooltip("メニュー移動時のSE")]
    public AudioClip _menuChangeSE = default;
    [SerializeField, Tooltip("メニュー決定時のSE")]
    public AudioClip _menuExecutionSE = default;
    [SerializeField, Tooltip("ゲーム開始時のカウントダウンのSE")]
    public AudioClip _countDown = default;
    [SerializeField, Tooltip("ゲーム開始時のSE")]
    public AudioClip _gameStartSE = default;
    [SerializeField, Tooltip("ミノの移動時のSE")]
    public AudioClip _minoTranslateSE = default;
    [SerializeField, Tooltip("ミノの回転時のSE")]
    public AudioClip _minoRotateSE = default;
    [SerializeField, Tooltip("ミノの着地時のSE")]
    public AudioClip _minoLandingSE = default;
    [SerializeField, Tooltip("１ラインのSE")]
    public AudioClip _oneLineSE = default;
    [SerializeField, Tooltip("２ラインのSE")]
    public AudioClip _twoLineSE = default;
    [SerializeField, Tooltip("3ラインのSE")]
    public AudioClip _threeLineSE = default;
    [SerializeField, Tooltip("テトリス・TスピントリプルのSE")]
    public AudioClip _fourLineSE = default;
    [SerializeField, Tooltip("ゲームオーバー時のSE")]
    public AudioClip _gameOverSE = default;
}
