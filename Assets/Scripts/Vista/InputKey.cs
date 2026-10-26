using System;
using System.Collections.Generic;
using UnityEngine;

public class InputKey : MonoBehaviour, IInputKey
{
    public event Action<KeyDirection> OnDirectionPressed;
    public event Action OnPausePressed;
    public event Action<bool> OnSpeedBoostChanged;

    private readonly Dictionary<KeyCode, KeyDirection> _direcAsignadas = new Dictionary<KeyCode, KeyDirection>()
{
    { KeyCode.W, KeyDirection.Up },
    { KeyCode.S, KeyDirection.Down },
    { KeyCode.A, KeyDirection.Left },
    { KeyCode.D, KeyDirection.Right },
    { KeyCode.UpArrow, KeyDirection.Up },
    { KeyCode.DownArrow, KeyDirection.Down },
    { KeyCode.LeftArrow, KeyDirection.Left },
    { KeyCode.RightArrow, KeyDirection.Right }
};

    private void Update()
    {
        foreach (KeyValuePair<KeyCode, KeyDirection> kvp in _direcAsignadas)
        {
            if (Input.GetKeyDown(kvp.Key))
            {
                OnDirectionPressed?.Invoke(kvp.Value);
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            OnPausePressed?.Invoke();

        if (Input.GetKeyDown(KeyCode.LeftShift))
            OnSpeedBoostChanged?.Invoke(true);
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            OnSpeedBoostChanged?.Invoke(false);
    }
}