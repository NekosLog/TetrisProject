using UnityEngine;
public class InputManager : MonoBehaviour
{
    public delegate void InputEventHandler();

    public InputEventHandler InputDownRight;
    public InputEventHandler InputDownLeft;
    public InputEventHandler InputDownUp;
    public InputEventHandler InputDownDown;
    public InputEventHandler InputDownDecision;
    public InputEventHandler InputDownCancel;

    public InputEventHandler InputHoldRight;
    public InputEventHandler InputHoldLeft;
    public InputEventHandler InputHoldUp;
    public InputEventHandler InputHoldDown;
    public InputEventHandler InputHoldDecision;
    public InputEventHandler InputHoldCancel;

    private void Update()
    {
        // �E�����̓���
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            InputDownRight();
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            InputHoldRight();
        }


        // �������̓���
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            InputDownLeft();
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            InputHoldLeft();
        }


        // ������̓���
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            InputDownUp();
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            InputHoldUp();
        }


        // �������̓���
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            InputDownDown();
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            InputHoldDown();
        }


        // ����{�^���̓���
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
        {
            InputDownDecision();
        }
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Z))
        {
            InputHoldDecision();
        }


        // �L�����Z���{�^���̓���
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X))
        {
            InputDownCancel();
        }
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.X))
        {
            InputHoldCancel();
        }
    }
}