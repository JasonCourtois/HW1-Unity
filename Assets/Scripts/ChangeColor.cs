using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeColor : MonoBehaviour
{
    // Input variables
    private InputAction _startAction;
    private InputAction _stopAction;

    // Color variables
    private Color[] _colors = new Color[] {Color.red, Color.blue, Color.green};
    private int _newColorIndex = 0;
    private Coroutine _colorCoroutine;  // Reference to coroutine stored to stop it when c is pressed.
    [SerializeField, Range(0f, 2f)] private float _lerpRate = 0.9f;    // Rate colors will be transitioned at.

    // Renderer used to access current material of object.
    private Renderer _renderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startAction = InputSystem.actions.FindAction("Start");
        _stopAction = InputSystem.actions.FindAction("Stop");

        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Only start if coroutine isn't already running.
        if (_startAction.WasPressedThisFrame() && _colorCoroutine == null)
        {
            _colorCoroutine = StartCoroutine(ChangeColors());
        }

        // Similarly, only end coroutine if it is running.
        if (_stopAction.WasPressedThisFrame() && _colorCoroutine != null)
        {
            StopCoroutine(_colorCoroutine);
            _colorCoroutine = null;
        }
    }

    private IEnumerator ChangeColors()
    {
        // Will continue changing colors until it is manually stopped
        while (true)
        {
            float lerpProgress = 0;
            Color oldColor = _renderer.material.color;
            while (lerpProgress < 1)
            {
                Color lerpedColor = Color.Lerp(oldColor, _colors[_newColorIndex], lerpProgress);
                lerpProgress += Time.deltaTime * _lerpRate;
                _renderer.material.color = lerpedColor;
                yield return null;
            }
            // Advance new color index and loop it to start if end was reached.
            _newColorIndex = _newColorIndex == _colors.Length - 1 ? 0 : _newColorIndex + 1;

            // Waits for 2 seconds until changing to next color as written in assignment instructions.
            yield return new WaitForSeconds(2);
        }
    }

}
