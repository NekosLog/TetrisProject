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
        // 右方向の入力
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            InputDownRight?.Invoke();
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            InputHoldRight?.Invoke();
        }


        // 左方向の入力
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            InputDownLeft?.Invoke();
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            InputHoldLeft?.Invoke();
        }


        // 上方向の入力
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            InputDownUp?.Invoke();
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            InputHoldUp?.Invoke();
        }


        // 下方向の入力
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            InputDownDown?.Invoke();
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            InputHoldDown?.Invoke();
        }


        // 決定ボタンの入力
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Z))
        {
            InputDownDecision?.Invoke();
        }
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Z))
        {
            InputHoldDecision?.Invoke();
        }


        // キャンセルボタンの入力
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.X))
        {
            InputDownCancel?.Invoke();
        }
        if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.X))
        {
            InputHoldCancel?.Invoke();
        }

        // ホールドボタンの入力
        if (Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.Space))
        {
            InputDownHold?.Invoke();
        }
    }
}