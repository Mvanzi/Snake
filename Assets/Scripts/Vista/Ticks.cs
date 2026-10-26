using System;
using UnityEngine;

public class Ticks : MonoBehaviour, ITicks
{
    public float speed = 0.2f;
    private float timer;
    private bool isRunning = true;

    public event Action OnTick;

    private void Update()
    {
        if (!isRunning) return;

        timer += Time.deltaTime;
        if (timer >= speed)
        {
            timer = 0f;
            OnTick?.Invoke();
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void Pause() => isRunning = false;
    public void Resume() => isRunning = true;
}