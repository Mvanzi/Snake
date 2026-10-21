using UnityEngine;
namespace Snake.Model
{

    public interface IDirection
    {
        public Coordinate GetNextCoordinate(Coordinate current);
        public IDirection TurnUp();
        public IDirection TurnDown();
        public IDirection TurnLeft();
        public IDirection TurnRight();
    }
}