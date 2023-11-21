using UnityEngine;

public class SEData : MonoBehaviour
{
    [SerializeField, Tooltip("SE�p�̃X�s�[�J")]
    public AudioSource _speaker = default;
    [SerializeField, Tooltip("���j���[�ړ�����SE")]
    public AudioClip _menuChangeSE = default;
    [SerializeField, Tooltip("���j���[���莞��SE")]
    public AudioClip _menuExecutionSE = default;
    [SerializeField, Tooltip("�Q�[���J�n���̃J�E���g�_�E����SE")]
    public AudioClip _countDown = default;
    [SerializeField, Tooltip("�Q�[���J�n����SE")]
    public AudioClip _gameStartSE = default;
    [SerializeField, Tooltip("�~�m�̈ړ�����SE")]
    public AudioClip _minoTranslateSE = default;
    [SerializeField, Tooltip("�~�m�̉�]����SE")]
    public AudioClip _minoRotateSE = default;
    [SerializeField, Tooltip("�~�m�̒��n����SE")]
    public AudioClip _minoLandingSE = default;
    [SerializeField, Tooltip("�P���C����SE")]
    public AudioClip _oneLineSE = default;
    [SerializeField, Tooltip("�Q���C����SE")]
    public AudioClip _twoLineSE = default;
    [SerializeField, Tooltip("3���C����SE")]
    public AudioClip _threeLineSE = default;
    [SerializeField, Tooltip("�e�g���X�ET�X�s���g���v����SE")]
    public AudioClip _fourLineSE = default;
    [SerializeField, Tooltip("�Q�[���I�[�o�[����SE")]
    public AudioClip _gameOverSE = default;
}
