using System.Collections;
using System.Collections.Generic;

    public class RegularFruit : FruitEntity
    {
        protected override void ApplyFruitEffect(IGameContext context)
        {
            context.GrowSnake();
        }
    }
