using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyEntity : Entity
{
    public void OnCollide(IGameContext context)
    {
        context.EndGame();
    }
}
