using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Snake.Assets.Scripts.Model {
    public abstract class Entity
    {
        public abstract void OnCollide(IGameContext context);
        public virtual bool IsEmpty() => false;
    } 
}
