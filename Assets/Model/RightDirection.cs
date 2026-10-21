using UnityEngine;
namespace Snake.Assets.Model
{
    public class RightDirection : IDirection
    {
        public Coordinate GetNextCoordinate(Coordinate current)
        {
            return new Coordinate(current.X + 1, current.Y);
        }
        public IDirection TurnUp()
        {
            return new UpDirection();
        }
        public IDirection TurnDown()
        {
            return new DownDirection();
        }
        public IDirection TurnLeft()
        {
            return this;
        }
        public IDirection TurnRight()
        {
            return this;
        }
    }
}