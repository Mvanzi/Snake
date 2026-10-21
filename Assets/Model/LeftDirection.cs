using UnityEngine;
namespace Snake.Model
{
    public class LeftDirection : IDirection
    {
        public Coordinate GetNextCoordinate(Coordinate current)
        {
            return new Coordinate(current.X - 1, current.Y);
        }
        public IDirection TurnUp()
        {
            return new UpDirection();
        }
        public IDirection TurnDown()
        {
            return DownDirection();
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