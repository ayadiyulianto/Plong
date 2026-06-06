using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{
    public enum ControlMode { Human, AI }
    public float maxY, minY;
    public float speed;
    public ControlMode mode = ControlMode.Human;

    Plong controls;
    InputAction moveAction;

    void Awake()
    {
        if (mode == ControlMode.Human)
        {
            controls = new Plong();
            moveAction = controls.Player.Move;
        }
    }

    void OnEnable()
    {
        moveAction?.Enable();
    }

    void OnDisable()
    {
        moveAction?.Disable();
    }

    void OnDestroy()
    {
        controls?.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        float input = mode == ControlMode.Human ? moveAction.ReadValue<float>() : 0f; // TODO: AI drives this paddle later
        float move = input * speed * Time.deltaTime;
        float nextPos = transform.position.y + move;
        if (nextPos > maxY)
        {
            move = 0;
        }
        if (nextPos < minY)
        {
            move = 0;
        }
        transform.Translate(0, move, 0);
    }
}
