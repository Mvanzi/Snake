using UnityEngine;
namespace Snake.Model
{
    public class DownDirection : IDirection
    {
        public Coordinate GetNextCoordinate(Coordinate current)
        {
            return new Coordinate(current.X, current.Y - 1);
        }
        public IDirection TurnUp()
        {
            return this;
        }
        public IDirection TurnDown()
        {
            return this;
        }
        public IDirection TurnLeft()
        {
            return new LeftDirection();
        }
        public IDirection TurnRight()
        {
            return new RightDirection();
        }
    }
}