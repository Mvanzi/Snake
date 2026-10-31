using System.Collections;
using System.Collections.Generic;
    public class EmptyEntity : Entity
    {
        public override void OnCollide(IGameContext context) { }
        public override bool IsEmpty() => true;
    }
