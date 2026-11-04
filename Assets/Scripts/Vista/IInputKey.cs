using System;

public interface IInputKey
{
    event Action<KeyDirection> OnDirectionPressed;
    event Action OnPausePressed;
    event Action<bool> OnSpeedBoostChanged;
}