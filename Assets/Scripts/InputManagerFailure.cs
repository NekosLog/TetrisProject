using System;
using UnityEngine;
public class InputManagerFailure : MonoBehaviour
{
    public delegate void InputEventHandler(object sender, EventArgs e);

    public InputEventHandler InputDownEvent;

    public InputEventHandler InputHoldEvent;

    InputEventArgs input_Right = new InputEventArgs(InputEventArgs.E_InputKey.Right);

    InputEventArgs input_Left = new InputEventArgs(InputEventArgs.E_InputKey.Left);

    InputEventArgs input_Up = new InputEventArgs(InputEventArgs.E_InputKey.Up);

    InputEventArgs input_Down = new InputEventArgs(InputEventArgs.E_InputKey.Down);

    InputEventArgs input_Decision = new InputEventArgs(InputEventArgs.E_InputKey.Decision);

    InputEventArgs input_Cancel = new InputEventArgs(InputEventArgs.E_InputKey.Cancel);


    private void Update()
    {
        // 右方向の入力
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnInputDownEvent(input_Right);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            OnInputHoldEvent(input_Right);
        }


        // 左方向の入力
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnInputDownEvent(input_Left);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            OnInputHoldEvent(input_Left);
        }


        // 上方向の入力
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnInputDownEvent(input_Up);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            OnInputHoldEvent(input_Up);
        }


        // 下方向の入力
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnInputDownEvent(input_Down);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            OnInputHoldEvent(input_Down);
        }


        // 決定ボタンの入力
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
        {
            OnInputDownEvent(input_Decision);
        }
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Z))
        {
            OnInputHoldEvent(input_Decision);
        }


        // キャンセルボタンの入力
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X))
        {
            OnInputDownEvent(input_Cancel);
        }
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.X))
        {
            OnInputHoldEvent(input_Cancel);
        }
    }


    private void OnInputDownEvent(InputEventArgs inputType)
    {
        InputDownEvent?.Invoke(this, inputType);
    }

    private void OnInputHoldEvent(InputEventArgs inputType)
    {
        InputDownEvent?.Invoke(this, inputType);
    }
}