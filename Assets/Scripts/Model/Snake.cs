using System.Collections.Generic;

public class Snake : ISnake
{
    private readonly List<Coordinate> bodyPositions;
    private IDirection currentDirection;
    private Coordinate lastTailPosition;

    public int Length => bodyPositions.Count;
    public IReadOnlyList<Coordinate> BodyPositions => bodyPositions;
    public IDirection CurrentDirection => currentDirection;

    public Snake(Coordinate initialPosition, IDirection initialDirection)
    {
        bodyPositions = new List<Coordinate>();
        bodyPositions.Add(initialPosition);
        lastTailPosition = initialPosition;
        currentDirection = initialDirection;
    }

    public Snake(IEnumerable<Coordinate> initialPositions, IDirection initialDirection)
    {
        bodyPositions = new List<Coordinate>(initialPositions);
        lastTailPosition = bodyPositions.Count > 0 ? bodyPositions[0] : new Coordinate(0, 0);
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
        for (int i = 0; i < bodyPositions.Count - 1; i++)
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