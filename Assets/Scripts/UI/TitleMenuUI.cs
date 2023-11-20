using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuUI : MonoBehaviour, IFTitleMenuUI
{
    [SerializeField, Tooltip("GameStart‚Ì–îˆó")]
    private SpriteRenderer arrow_GameStart = default;
    [SerializeField, Tooltip("OpenOption‚Ì–îˆó")]
    private SpriteRenderer arrow_OpenOption = default;
    [SerializeField, Tooltip("Exit‚Ì–îˆó")]
    private SpriteRenderer arrow_Exit = default;
        
    public void ChengeUI(E_MenuItem index)
    {
        switch (index)
        {
            case E_MenuItem.GameStart:
                arrow_GameStart.enabled = true;
                arrow_OpenOption.enabled = false;
                arrow_Exit.enabled = false;
                break;
            case E_MenuItem.OpenOption:
                arrow_GameStart.enabled = false;
                arrow_OpenOption.enabled = true;
                arrow_Exit.enabled = false;
                break;
            case E_MenuItem.Exit:
                arrow_GameStart.enabled = false;
                arrow_OpenOption.enabled = false;
                arrow_Exit.enabled = true;
                break;
        }
    }

    public void EventExecution(E_MenuItem index)
    {
        switch (index)
        {

        }
    }
}
