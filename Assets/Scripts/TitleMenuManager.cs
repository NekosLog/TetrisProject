using UnityEngine;

public class TitleMenuManager : MonoBehaviour, IFInputTitleMenu
{
    private IFStateEvent _iStateEvent = default;
    private IFTitleMenuUI _iTitleMenuUI = default;
    private IFDropStart _iDropStart = default;
    private E_MenuItem _titleMenuIndex = E_MenuItem.GameStart;
    private const int INDEX_LOWER_LIMIT = 0;
    private const int INDEX_UPPER_LIMIT = 1;

    private void Awake()
    {
        _iStateEvent = GameObject.FindWithTag("GameManager").GetComponent<IFStateEvent>();
        _iTitleMenuUI = GameObject.FindWithTag("GameManager").GetComponent<IFTitleMenuUI>();
        _iDropStart = GameObject.FindWithTag("GameManager").GetComponent<IFDropStart>();
        _iTitleMenuUI.ChengeUI(_titleMenuIndex);
    }

    public void UpKeyDown()
    {
        // IndexÇàÍÇ¬â∫Ç∞ÇÈ
        if ((int)_titleMenuIndex > INDEX_LOWER_LIMIT)
        {
            _titleMenuIndex--;
            print(_titleMenuIndex);
            _iTitleMenuUI.ChengeUI(_titleMenuIndex);
        }
    }

    public void DownKeyDown()
    {
        // IndexÇàÍÇ¬è„Ç∞ÇÈ
        if ((int)_titleMenuIndex < INDEX_UPPER_LIMIT)
        {
            _titleMenuIndex++;
            print(_titleMenuIndex);
            _iTitleMenuUI.ChengeUI(_titleMenuIndex);
        }
    }

    public void DecisionKeyDown()
    {
        // ëIëçÄñ⁄ÇÃãNìÆ
        EventExecution(_titleMenuIndex);
        _iTitleMenuUI.EventExecution(_titleMenuIndex);
    }

    public void CancelKeyDown()
    {

    }

    public void ReSetIndex()
    {
        const E_MenuItem START_INDEX = E_MenuItem.GameStart;
        _titleMenuIndex = START_INDEX;
    }
    

    private void EventExecution(E_MenuItem index)
    {
        switch (index)
        {
            case E_MenuItem.GameStart:
                GameStartEvent();
                break;

            //case E_MenuItem.OpenOption:
            //    OpenOptionEvent();
            //    break;                    åªç›ñ¢é¿ëï

            case E_MenuItem.Exit:
                ExitEvent();
                break;
        }
    }

    private void GameStartEvent()
    {
        _iStateEvent.ChengeInputState(InputState.MainGame);
        Invoke("DropStart", 1f);
    }

    //private void OpenOptionEvent()Å@åªç›ñ¢é¿ëï
    //{

    //}

    private void ExitEvent()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    private void DropStart()
    {
        _iDropStart.DropStart();
    }
}
