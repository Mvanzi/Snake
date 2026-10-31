
    public class UpDirection : IDirection
    {
        public Coordinate GetNextCoordinate(Coordinate current)
        {
            return new Coordinate(current.X, current.Z + 1);
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
