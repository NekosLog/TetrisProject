using UnityEngine;

public class TitleMenuManager : MonoBehaviour, IFInputTitleMenu
{
    private IFTitleMenuUI iTitleMenuUI = default;
    private E_MenuItem TitleMenuIndex = E_MenuItem.GameStart;
    private const int INDEX_LOWER_LIMIT = 0;
    private const int INDEX_UPPER_LIMIT = 2;

    private void Awake()
    {
        iTitleMenuUI = GameObject.FindWithTag("GameManager").GetComponent<IFTitleMenuUI>();
    }

    public void UpKeyDown()
    {
        // IndexÇàÍÇ¬è„Ç∞ÇÈ
        if ((int)TitleMenuIndex < INDEX_UPPER_LIMIT)
        {
            TitleMenuIndex++;
            iTitleMenuUI.ChengeUI(TitleMenuIndex);
        }
    }
    public void DownKeyDown()
    {
        // IndexÇàÍÇ¬â∫Ç∞ÇÈ
        if ((int)TitleMenuIndex > INDEX_LOWER_LIMIT)
        {
            TitleMenuIndex--;
            iTitleMenuUI.ChengeUI(TitleMenuIndex);
        }
    }
    public void DecisionKeyDown()
    {
        // ëIëçÄñ⁄ÇÃãNìÆ
        EventExecution(TitleMenuIndex);
        iTitleMenuUI.EventExecution(TitleMenuIndex);
    }
    public void CancelKeyDown()
    {

    }

    public void ReSetIndex()
    {
        const E_MenuItem START_INDEX = E_MenuItem.GameStart;
        TitleMenuIndex = START_INDEX;
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
