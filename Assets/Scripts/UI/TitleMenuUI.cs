using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMenuUI : MonoBehaviour, IFTitleMenuUI
{
    [SerializeField, Tooltip("TitleMenu�S��")]
    private GameObject _titleMenu = default;
    [SerializeField, Tooltip("InGemeMenu�S��")]
    private GameObject _inGameMenu = default;
    [SerializeField, Tooltip("GameStart�̖��")]
    private Image _arrow_GameStart = default;
    //[SerializeField, Tooltip("OpenOption�̖��")]
    //private SpriteRenderer _arrow_OpenOption = default;
    [SerializeField, Tooltip("Exit�̖��")]
    private Image _arrow_Exit = default;
    [SerializeField, Tooltip("���̕��i�̐F")]
    private Color _defaultArrowColor = Color.red;
    [SerializeField, Tooltip("���̌��莞�̐F")]
    private Color _executionArrowColor = Color.blue;

    // SE�f�[�^
    private SEData _sound = default;

    private void Awake()
    {
        // SE�f�[�^�̎擾
        _sound = GameObject.FindWithTag("GameManager").GetComponent<SEData>();
    }

    public void ChengeUI(E_MenuItem index)
    {
        switch (index)
        {
            case E_MenuItem.GameStart:
                _arrow_GameStart.enabled = true;
                //_arrow_OpenOption.enabled = false;
                _arrow_Exit.enabled = false;
                break;
            //case E_MenuItem.OpenOption:
            //    _arrow_GameStart.enabled = false;
            //    _arrow_OpenOption.enabled = true;
            //    _arrow_Exit.enabled = false;
            //    break;
            case E_MenuItem.Exit:
                _arrow_GameStart.enabled = false;
                //_arrow_OpenOption.enabled = false;
                _arrow_Exit.enabled = true;
                break;
        }
    }

    public void EventExecution(E_MenuItem index)
    {
        switch (index)
        {
            case E_MenuItem.GameStart:
                _sound._speaker.PlayOneShot(_sound._menuExecutionSE);
                break;

            //case E_MenuItem.OpenOption:
            //    _sound._speaker.PlayOneShot(_sound._menuExecutionSE);
            //    break;

            case E_MenuItem.Exit:
                _sound._speaker.PlayOneShot(_sound._menuExecutionSE);
                break;
        }
    }

    private void GameStartUIEvent()
    {
        _arrow_GameStart.color = _executionArrowColor;
        Invoke("CloseMenu",1f);
    }

    private void CloseMenu()
    {
        _titleMenu.SetActive(false);
        _inGameMenu.SetActive(true);
    }
}
