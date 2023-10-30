using System;

public class InputEventArgs : EventArgs
{
    public enum E_InputKey
    { 
        Right    = 0,
        Left     = 1,
        Up       = 2,
        Down     = 3,
        Decision = 4,
        Cancel   = 5
    }

    public E_InputKey Event { get; }

    public InputEventArgs(E_InputKey InputKeyType)
    {
        Event = InputKeyType;
    }
}
