
    public interface ISnake
    {
        public Coordinate GetHead();
        public Coordinate GetTail();
        public Coordinate GetLastTailPosition();
        public Coordinate GetNextHeadPosition();
        public void MoveTo(Coordinate nextHeadPos);
        public void Grow();
        public void TurnUp();
        public void TurnDown();
        public void TurnLeft();
        public void TurnRight();

        int Length { get; }
}
