using UnityEngine;
public class InputManager : MonoBehaviour
{
    private DropingMino drop;

    public delegate void InputEventHandler();

    public InputEventHandler InputDownRight;
    public InputEventHandler InputDownLeft;
    public InputEventHandler InputDownUp;
    public InputEventHandler InputDownDown;
    public InputEventHandler InputDownDecision;
    public InputEventHandler InputDownCancel;
    public InputEventHandler InputDownHold;

    public InputEventHandler InputHoldRight;
    public InputEventHandler InputHoldLeft;
    public InputEventHandler InputHoldUp;
    public InputEventHandler InputHoldDown;
    public InputEventHandler InputHoldDecision;
    public InputEventHandler InputHoldCancel;

    private void Awake()
    {
        drop = GameObject.FindWithTag("GameManager").GetComponent<DropingMino>();
    }

    private void Update()
    {
        // �E�����̓���
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            InputDownRight?.Invoke();
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            InputHoldRight?.Invoke();
        }


        // �������̓���
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            InputDownLeft?.Invoke();
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            InputHoldLeft?.Invoke();
        }


        // ������̓���
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            InputDownUp?.Invoke();
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            InputHoldUp?.Invoke();
        }


        // �������̓���
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            InputDownDown?.Invoke();
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            InputHoldDown?.Invoke();
        }


        // ����{�^���̓���
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Z))
        {
            InputDownDecision?.Invoke();
        }
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Z))
        {
            InputHoldDecision?.Invoke();
        }


        // �L�����Z���{�^���̓���
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.X))
        {
            InputDownCancel?.Invoke();
        }
        if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.X))
        {
            InputHoldCancel?.Invoke();
        }

        // �z�[���h�{�^���̓���
        if (Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.Space))
        {
            InputDownHold?.Invoke();
        }
    }
}