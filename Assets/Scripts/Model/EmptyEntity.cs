using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Snake.Assets.Model { 

    public class EmptyEntity : Entity
    {
        public override void OnCollide(IGameContext context) { }
        public override bool IsEmpty() => true;
    }
}