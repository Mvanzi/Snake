using UnityEngine;
namespace Snake.Assets.Scripts.Model
{

    public struct Coordinate
    {
        public readonly int X;
        public readonly int Y;

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}