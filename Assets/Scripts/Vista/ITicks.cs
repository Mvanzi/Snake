using System;

public interface ITicks
{
    event Action OnTick;
    void SetSpeed(float newSpeed);
}