using UnityEngine;
public class StateManager:MonoBehaviour
{
    private InputManager _inputManager = default;

    private IFInputMainGame _inputMainGame = default;

    private IFInputTitleMenu _inputTitleMenu = default;

    private IFInputInGameMenu _inputInGameMenu = default;

    private IFInputResultMenu _inputResultMenu = default;

    private IFInputOption _inputOption = default;

    public enum InputState 
    {
        MainGame     = 0,
        TitleMenu    = 1,
        InGameMenu   = 2,
        ResultMenu   = 3,
        OptionMenu   = 4
    }

    private InputState _nowInputState = InputState.MainGame;

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
        _inputManager.
    }
}
