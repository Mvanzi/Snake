using System.Collections;
using System.Collections.Generic;

    public abstract class FruitEntity : Entity
    {
        public override void OnCollide(IGameContext context)
        {
            context.RespawnFruit();
            ApplyFruitEffect(context);
        }

        protected abstract void ApplyFruitEffect(IGameContext context);
    }
