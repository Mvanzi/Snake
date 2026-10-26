using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Snake.Assets.Model { 

    public class EmptyEntity : Entity
    {
        public void OnCollide(IGameContext collide) { }
    }
}