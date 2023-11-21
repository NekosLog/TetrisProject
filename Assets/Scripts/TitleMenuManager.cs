using UnityEngine;

public class TitleMenuManager : MonoBehaviour, IFInputTitleMenu
{
    private IFTitleMenuUI _iTitleMenuUI = default;
    private E_MenuItem _titleMenuIndex = E_MenuItem.GameStart;
    private const int INDEX_LOWER_LIMIT = 0;
    private const int INDEX_UPPER_LIMIT = 2;

    private void Awake()
    {
        _iTitleMenuUI = GameObject.FindWithTag("GameManager").GetComponent<IFTitleMenuUI>();
    }

    public void UpKeyDown()
    {
        // IndexÇàÍÇ¬è„Ç∞ÇÈ
        if ((int)_titleMenuIndex < INDEX_UPPER_LIMIT)
        {
            _titleMenuIndex++;
            _iTitleMenuUI.ChengeUI(_titleMenuIndex);
        }
    }
    public void DownKeyDown()
    {
        // IndexÇàÍÇ¬â∫Ç∞ÇÈ
        if ((int)_titleMenuIndex > INDEX_LOWER_LIMIT)
        {
            _titleMenuIndex--;
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

            case E_MenuItem.OpenOption:
                OpenOptionEvent();
                break;

            case E_MenuItem.Exit:
                ExitEvent();
                break;
        }
    }

    private void GameStartEvent()
    {

    }

    private void OpenOptionEvent()
    {

    }

    private void ExitEvent()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
