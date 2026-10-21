using UnityEngine;
namespace Snake.Model
{
    public interface ISnake
    {
        public Coordinate GetHead();
        public Coordinate GetTail();
        public Coordinate GetLastTailPosition();
        public Coordinate GetNextHeadPosition();
        public void MoveTo(Coordinate nextHeadPos);
        public void Grow();

    }
}