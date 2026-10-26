using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Snake.Assets.Scripts.Model { 

    public class RegularFruit : FruitEntity
    {
        protected override void ApplyFruitEffect(IGameContext context)
        {
            context.GrowSnake();
        }
    }
}