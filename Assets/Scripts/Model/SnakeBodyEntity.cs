using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Snake.Assets.Model
{
    public class SnakeBodyEntity : Entity
    {
        public override void OnCollide(IGameContext context)
        {
            context.EndGame();
        }
    }
}
