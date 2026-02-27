using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    // Input Variables
    private InputAction _rotateAction;
    private InputAction _moveAction;
    
    // Speed Variables
    [SerializeField, Range(0f, 100f)] private float _rotateSpeed = 75;
    [SerializeField, Range(0f, 50f)] private float _moveSpeed = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // For this assignment I chose to use the new input system as that is what I am currently using for my final project.
        _rotateAction = InputSystem.actions.FindAction("Rotate");
        _moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        float rotateValue = _rotateAction.ReadValue<float>();
        Vector3 moveValue = _moveAction.ReadValue<Vector3>();

        // I chose to use a vector 3 to also allow up and down movement with space and shift keys. This multiplication applies move speed and delta time to each value.
        moveValue *= Time.deltaTime * _moveSpeed;

        transform.Rotate(Vector3.up * _rotateSpeed * Time.deltaTime * rotateValue);
        transform.Translate(moveValue);
    }
}
