
    public class LeftDirection : IDirection
    {
        public Coordinate GetNextCoordinate(Coordinate current)
        {
            return new Coordinate(current.X - 1, current.Z);
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
