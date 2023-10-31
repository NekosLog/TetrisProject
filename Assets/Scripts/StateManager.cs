using UnityEngine;
public class StateManager:MonoBehaviour
{
    private IFSetInputEvent _iSetInputEvent = default;

    private IFInputMainGame _iInputMainGame = default;

    private IFInputTitleMenu _iInputTitleMenu = default;

    private IFInputInGameMenu _iInputInGameMenu = default;

    private IFInputResultMenu _iInputResultMenu = default;

    private IFInputOption _iInputOption = default;

    public enum InputState 
    {
        MainGame     = 0,
        TitleMenu    = 1,
        InGameMenu   = 2,
        ResultMenu   = 3,
        OptionMenu   = 4
    }

    private InputState _nowInputState = InputState.MainGame;

    private void Awake()
    {
        _iSetInputEvent = GameObject.FindWithTag("GameManager").GetComponent<IFSetInputEvent>();
        _iInputMainGame = GameObject.FindWithTag("GameManager").GetComponent<IFInputMainGame>();
        _iInputTitleMenu = GameObject.FindWithTag("GameManager").GetComponent<IFInputTitleMenu>();
        _iInputInGameMenu = GameObject.FindWithTag("GameManager").GetComponent<IFInputInGameMenu>();
        _iInputResultMenu = GameObject.FindWithTag("GameManager").GetComponent<IFInputResultMenu>();
        _iInputOption = GameObject.FindWithTag("GameManager").GetComponent<IFInputOption>();
    }

    private void Start()
    {
        ChengeInputState(InputState.MainGame);
    }

    public void ChengeInputState(InputState inputState)
    {
        switch (inputState)
        {
            case InputState.MainGame:
                ExitInputState(_nowInputState);
                SetInputMainGame();
                break;

            case InputState.TitleMenu:
                ExitInputState(_nowInputState);

                break;

            case InputState.InGameMenu:
                ExitInputState(_nowInputState);

                break;

            case InputState.ResultMenu:
                ExitInputState(_nowInputState);

                break;

            case InputState.OptionMenu:
                ExitInputState(_nowInputState);

                break;
        }
    }

    private void ExitInputState(InputState nowInputState)
    {
        switch (nowInputState)
        {
            case InputState.MainGame:

                break;

            case InputState.TitleMenu:

                break;

            case InputState.InGameMenu:

                break;

            case InputState.ResultMenu:

                break;

            case InputState.OptionMenu:

                break;
        }
    }


    private void SetInputMainGame()
    {
        _iSetInputEvent.SetHoldRight(_iInputMainGame.RightKeyHold);
        _iSetInputEvent.SetHoldLeft(_iInputMainGame.LeftKeyHold);
        _iSetInputEvent.SetDownUp(_iInputMainGame.UpKeyDown);
        _iSetInputEvent.SetHoldDown(_iInputMainGame.DownKeyHold);
        _iSetInputEvent.SetDownDecision(_iInputMainGame.DecisionKeyDown);
        _iSetInputEvent.SetDownUp(_iInputMainGame.CancelKeyDown);
    }
}
