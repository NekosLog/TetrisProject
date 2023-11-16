using UnityEngine;
public class StateManager:MonoBehaviour
{
    private InputManager _inputManager = default;

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
        _inputManager = GameObject.FindWithTag("GameManager").GetComponent<InputManager>();
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
                _inputManager.InputHoldRight = null;
                _inputManager.InputHoldLeft = null;
                _inputManager.InputDownUp = null;
                _inputManager.InputHoldDown = null;
                _inputManager.InputDownDecision = null;
                _inputManager.InputDownCancel = null;
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
        _inputManager.InputHoldRight = _iInputMainGame.InputHoldRight;
        _inputManager.InputHoldLeft = _iInputMainGame.InputHoldLeft;
        _inputManager.InputDownUp = _iInputMainGame.InputDownUp;
        _inputManager.InputHoldDown = _iInputMainGame.InputHoldDown;
        _inputManager.InputDownDecision = _iInputMainGame.InputDownDecision;
        _inputManager.InputDownCancel = _iInputMainGame.InputDownCancel;
        _inputManager.InputDownHold = _iInputMainGame.InputDownHold;
    }
}
