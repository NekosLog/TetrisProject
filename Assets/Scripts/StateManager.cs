using UnityEngine;
public class StateManager:MonoBehaviour
{
    private InputManager _inputManager = default;

    private IFInputMainGame _iInputMainGame = default;

    private IFInputTitleMenu _iInputTitleMenu = default;

    private IFInputInGameMenu _iInputInGameMenu = default;

    private IFInputResultMenu _iInputResultMenu = default;

    private IFInputOption _iInputOption = default;

    public delegate void StateDelegate();

    public StateDelegate MainGameEvent;
    public StateDelegate TitleMenuEvent;
    public StateDelegate InGameMenuEvent;
    public StateDelegate ResultMenuEvent;
    public StateDelegate OptionEvent;

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

        // MainGame
        MainGameEvent += ExitInputState;
        MainGameEvent += SetInputMainGame;

        // TitleMenu
        TitleMenuEvent += ExitInputState;
        TitleMenuEvent += SetInputTitleMenu;

        // InGameMenu
        InGameMenuEvent += ExitInputState;
        InGameMenuEvent += SetInputInGameMenu;

        // ResultMenu
        ResultMenuEvent += ExitInputState;
        ResultMenuEvent += SetInputResult;

        // Option
        OptionEvent += ExitInputState;
        OptionEvent += SetInputOption;
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
                MainGameEvent?.Invoke();
                break;

            case InputState.TitleMenu:
                SetInputTitleMenu();
                TitleMenuEvent?.Invoke();
                break;

            case InputState.InGameMenu:
                InGameMenuEvent?.Invoke();
                break;

            case InputState.ResultMenu:
                ResultMenuEvent?.Invoke();
                break;

            case InputState.OptionMenu:
                OptionEvent?.Invoke();
                break;
        }
    }

    private void ExitInputState()
    {
        _inputManager.InputDownRight = null;
        _inputManager.InputHoldRight = null;
        _inputManager.InputDownLeft = null;
        _inputManager.InputHoldLeft = null;
        _inputManager.InputDownUp = null;
        _inputManager.InputHoldUp = null;
        _inputManager.InputDownDown = null;
        _inputManager.InputHoldDown = null;
        _inputManager.InputDownDecision = null;
        _inputManager.InputHoldDecision = null;
        _inputManager.InputDownCancel = null;
        _inputManager.InputHoldCancel = null;
        _inputManager.InputDownHold = null;
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

    private void SetInputTitleMenu()
    {
        _inputManager.InputDownUp = _iInputTitleMenu.UpKeyDown;
        _inputManager.InputDownDown = _iInputTitleMenu.DownKeyDown;
        _inputManager.InputDownDecision = _iInputTitleMenu.DecisionKeyDown;
        _inputManager.InputDownCancel = _iInputTitleMenu.CancelKeyDown;
    }

    private void SetInputInGameMenu()
    {

    }
    private void SetInputResult()
    {

    }
    private void SetInputOption()
    {

    }
}
