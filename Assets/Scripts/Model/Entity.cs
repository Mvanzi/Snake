using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Snake.Assets.Model {
    public abstract class Entity
    {
        public abstract void OnCollide(IGameContext collide);
    } 
}
