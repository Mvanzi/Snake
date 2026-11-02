using System.Collections.Generic;

public interface ISnake
{
    Coordinate GetHead();
    Coordinate GetTail();
    Coordinate GetLastTailPosition();
    Coordinate GetNextHeadPosition();
    void MoveTo(Coordinate nextHeadPos);
    void Grow();
    void TurnUp();
    void TurnDown();
    void TurnLeft();
    void TurnRight();
    int Length { get; }
    IReadOnlyList<Coordinate> BodyPositions { get; }
    IDirection CurrentDirection { get; }
}