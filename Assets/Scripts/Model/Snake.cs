using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Snake.Assets.Model
{
    public class Snake : ISnake
    {
        private List<Coordinate> bodyPositions;
        private IDirection currentDirection;
        private Coordinate lastTailPosition;

        public Snake(Coordinate initialPosition, IDirection initialDirection)
        {
            bodyPositions = new List<Coordinate>();
            bodyPositions.Add(initialPosition);
            lastTailPosition = initialPosition;
            currentDirection = initialDirection;
        }

        public Coordinate GetHead()
        {
            return bodyPositions[bodyPositions.Count - 1];
        }

        public Coordinate GetTail()
        {
            return bodyPositions[0];
        }

        public Coordinate GetLastTailPosition()
        {
            return lastTailPosition;
        }

        public Coordinate GetNextHeadPosition()
        {
            return currentDirection.GetNextCoordinate(GetHead());
        }

        public void MoveTo(Coordinate nextHeadPos)
        {
            lastTailPosition = GetTail();
            for (int i = 0; i < bodyPositions.Count -1; i++)
            {
                bodyPositions[i] = bodyPositions[i + 1];
            }
            bodyPositions[bodyPositions.Count - 1] = nextHeadPos;
        }

        public void Grow()
        {
            bodyPositions.Insert(0, lastTailPosition);
        }

        public void TurnUp()
        {
            currentDirection = currentDirection.TurnUp();
        }

        public void TurnDown()
        {
            currentDirection = currentDirection.TurnDown();
        }

        public void TurnLeft()
        {
            currentDirection = currentDirection.TurnLeft();
        }

        public void TurnRight()
        {
            currentDirection = currentDirection.TurnRight();
        }    
    }
}