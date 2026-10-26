using System;

ususing System;

public enum KeyDirection
{
    Up,
    Down, 
    Left,  
    Right   
}

public interface IInputKey
{
    event Action<KeyDirection> OnDirectionPressed;
    event Action OnPausePressed;
    event Action<bool> OnSpeedBoostChanged;
}