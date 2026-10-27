using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Snake.Assets.Scripts.Model
{
    public class Cell : ICell
    {
        public Entity CurrentEntity { get; set; }
        public bool IsEmpty => CurrentEntity.IsEmpty();

        public Cell()
        {
            CurrentEntity = new EmptyEntity();
        }

        public void Clear()
        {
            CurrentEntity = new EmptyEntity();
        }
    }
}

