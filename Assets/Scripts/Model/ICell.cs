using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Snake.Assets.Scripts.Model { 

    public interface ICell
    {
        Entity CurrentEntity { get; set; }
        bool IsEmpty { get; }
        void Clear();
    }
}