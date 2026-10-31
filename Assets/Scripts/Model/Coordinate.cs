using UnityEngine;
namespace Snake.Assets.Scripts.Model
{

    public struct Coordinate
    {
        public readonly int X;
        public readonly int Z;

        public Coordinate(int x, int z)
        {
            X = x;
            Z = z;
        }
    }
}