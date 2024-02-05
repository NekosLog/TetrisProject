using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMenuUI : MonoBehaviour, IFTitleMenuUI
{
    [SerializeField, Tooltip("TitleMenu全部")]
    private GameObject _titleMenu = default;
    [SerializeField, Tooltip("InGemeMenu全部")]
    private GameObject _inGameMenu = default;
    [SerializeField, Tooltip("GameStartの矢印")]
    private GameObject _arrow_GameStart = default;
    //[SerializeField, Tooltip("OpenOptionの矢印")]
    //private SpriteRenderer _arrow_OpenOption = default;
    [SerializeField, Tooltip("Exitの矢印")]
    private GameObject _arrow_Exit = default;

    // SEデータ
    private SoundData _sound = default;

    private void Awake()
    {
        // SEデータの取得
        _sound = GameObject.FindWithTag("GameManager").GetComponent<SoundData>();
    }

    public void ChengeUI(E_MenuItem index)
    {
        switch (index)
        {
            case E_MenuItem.GameStart:
                _arrow_GameStart.SetActive(true);
                //_arrow_OpenOption.enabled = false;
                _arrow_Exit.SetActive(false);
                _sound._SEspeaker.PlayOneShot(_sound._menuChangeSE);
                break;
            //case E_MenuItem.OpenOption:
            //    _arrow_GameStart.enabled = false;
            //    _arrow_OpenOption.enabled = true;
            //    _arrow_Exit.enabled = false;
            //    break;
            case E_MenuItem.Exit:
                _arrow_GameStart.SetActive(false);
                //_arrow_OpenOption.enabled = false;
                _arrow_Exit.SetActive(true);
                _sound._SEspeaker.PlayOneShot(_sound._menuChangeSE);
                break;
        }
    }

    public void EventExecution(E_MenuItem index)
    {
        float moveValue = 10f;

        switch (index)
        {
            case E_MenuItem.GameStart:
                _sound._SEspeaker.PlayOneShot(_sound._menuExecutionSE);
                _arrow_GameStart.transform.position += Vector3.right * moveValue;
                Invoke("CloseMenu", 1f);
                break;

            //case E_MenuItem.OpenOption:
            //    _sound._speaker.PlayOneShot(_sound._menuExecutionSE);
            //    break;

            case E_MenuItem.Exit:
                _sound._SEspeaker.PlayOneShot(_sound._menuExecutionSE);
                _arrow_Exit.transform.position += Vector3.right * moveValue;
                break;
        }
    }

    private void GameStartUIEvent()
    {
        Invoke("CloseMenu",1f);
    }

    private void CloseMenu()
    {
        _titleMenu.SetActive(false);
        _inGameMenu.SetActive(true);
    }
}
