using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action ClickingMouseDown;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ClickingMouseDown?.Invoke();
    }
}