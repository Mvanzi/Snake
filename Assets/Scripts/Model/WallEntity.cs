using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Snake.Assets.Model
{

    public class WallEntity : Entity
    {
        public override void OnCollide(IGameContext context)
        {
            context.EndGame();
        }
    }
}
