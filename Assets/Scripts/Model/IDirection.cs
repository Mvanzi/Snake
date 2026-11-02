public interface IDirection
{
    Coordinate GetNextCoordinate(Coordinate current);
    IDirection TurnUp();
    IDirection TurnDown();
    IDirection TurnLeft();
    IDirection TurnRight();
}