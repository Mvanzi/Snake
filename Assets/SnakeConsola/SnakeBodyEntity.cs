using System.Collections;
using System.Collections.Generic;

    public class SnakeBodyEntity : Entity
    {
        public override void OnCollide(IGameContext context)
        {
            context.EndGame();
        }
    }

